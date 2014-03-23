using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CourseWork.Templates;
using CourseWork.Templates.Elements;
using CourseWork.Utilities.Helpers;
using GMap.NET;
using GroupItem = CourseWork.Templates.Elements.GroupItem;

namespace CourseWork.Manager
{
    public class DiagramItemManager
    {
        private static DiagramItemManager _instance;

        public static DiagramItemManager SetInstance(DrawCanvas canvas)
        {
            return _instance ?? (_instance = new DiagramItemManager(canvas));
        }

        public static DiagramItemManager Instance
        {
            get
            {
                if (_instance == null) throw new Exception("Используй инициализацию с параметром");
                return _instance;
            }
        }

        public List<DiagramItem> DiagramItemsDevices
        {
            get
            {
                return
                    new List<DiagramItem>(_canvas.Children.OfType<NodeItem>().
                        OrderBy(x => x.Id).ToList());
            }
        }

        public List<GroupItem> GroupDeviceses
        {
            get { return _canvas.Children.OfType<GroupItem>().OrderBy(x => x.Id).ToList(); }
        }
         
        public List<ConnectionArrow> ConnectionArrows
        {
            get { return _canvas.Children.OfType<ConnectionArrow>().ToList(); }
        }

        public List<DiagramItem> Items
        {
            get { return _canvas.Children.OfType<DiagramItem>().OrderBy(x => x.Id).ToList(); }
        }

        public List<DiagramItem> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList();  }
        }

        private static DrawCanvas _canvas;

        private DiagramItemManager(DrawCanvas canvas)
        {
            _canvas = canvas;
        }

        public void ResetManager()
        {
            for (int i = 0; i < _canvas.Children.Count; i++)
            {
                var curItem = _canvas.Children[0];
                var isDiagramItem = curItem is DiagramItem;
                if (isDiagramItem)
                {
                    Remove((DiagramItem) curItem);
                }
                else
                {
                    Remove((ConnectionArrow) curItem);
                }
            }
        }

        /// <summary>
        /// Загрузить элементы на форму
        /// </summary>
        public void LoadDefaultElements()
        {
            var matrix = Services.ReadFromFile.ReadMatrix();
            var vector = Services.ReadFromFile.ReadMatrixRow();
            var positions = Services.ReadFromFile.ReadLatLngPositions();

            var fromFileCoords = vector.Rows + vector.Cols + 1 == positions.Rows;

            var inBuffers = new DiagramItem[vector.Rows];
            for (int i = 0; i < vector.Rows; i++)
            {
                inBuffers[i] = AddNewItemBufferIn(new Point(30, 240 + 100*i));
                if (fromFileCoords)
                {
                    inBuffers[i].PositionLatLng = new PointLatLng(positions[i + 1, 1], positions[i + 1, 2]);
                    Maps.MapHelper.Instance.UpdateScreenCoords(inBuffers[i]);
                }
            }

            var outBuffer = new OutBuffItem("outBuf", _canvas.ActualWidth - 60, 200);

            if (fromFileCoords)
            {
                outBuffer.PositionLatLng = new PointLatLng(positions[vector.Rows + 1, 1], positions[vector.Rows + 1, 2]);
                Maps.MapHelper.Instance.UpdateScreenCoords(outBuffer);
            }
            _canvas.Children.Add(outBuffer);


            double curX = 140, curY = 200;
            for (int i = 1; i <= matrix.Rows; i++)
            {
                var dItem = AddNewItemDevice(new Point(curX + 30, curY + 40));
                if (fromFileCoords)
                {
                    dItem.PositionLatLng = new PointLatLng(positions[vector.Rows + 1 + i, 1],
                                                           positions[vector.Rows + 1 + i, 2]);
                    Maps.MapHelper.Instance.UpdateScreenCoords(dItem);
                }

                for (int vectIndex = 1; vectIndex <= vector.Rows; vectIndex++)
                {
                    if (vector[vectIndex, i] > 0) AddNewLink(inBuffers[vectIndex - 1], dItem, vector[vectIndex, i]);
                }
                var chance = MatrixHelper.CalculateRowChance(matrix, i);
                if (chance < 1.0f)
                {
                    AddNewLink(dItem, outBuffer, 1.0f - chance);
                }

                curX += 90;
                curY += (i%2 == 1) ? -80 : 80;
                if (curX > _canvas.ActualWidth - 100)
                {
                    curX = 100;
                    curY += 140;
                }
            }

            for (int i = 1; i <= matrix.Rows; i++)
            {
                for (int j = 1; j <= matrix.Cols; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        AddNewLink(DiagramItemsDevices[i-1], DiagramItemsDevices[j-1], matrix[i, j]);
                    }
                }
            }
        }

        public NodeItem AddNewItemDevice(Point pos)
        {
            var dItem = new NodeItem((Instance.DiagramItemsDevices.Count + 1).ToString(), pos.X - 30, pos.Y - 40);
            AddItemToVisual(dItem);
            return dItem;
        }

        public InBuffItem AddNewItemBufferIn(Point pos)
        {
            var dItem = new InBuffItem("inBuf", pos.X - 30, pos.Y - 40);
            AddItemToVisual(dItem);
            return dItem;
        }
        
        public GroupItem AddNewItemGroupItem(Point pos)
        {
            var dItem = new GroupItem("GROUP", pos.X - 30, pos.Y - 40);
            AddItemToVisual(dItem);
            return dItem;
        }

        private void AddItemToVisual(DiagramItem dItem)
        {
            _canvas.Children.Add(dItem);
            Maps.MapHelper.Instance.UpdateLatLngPoses(dItem);
            dItem.UpdateLayout();
        }

        /// <summary>
        /// Добавление новой связи
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="chance"></param>
        /// <param name="isGroups">связь между группами</param>
        public void AddNewLink(DiagramItem from, DiagramItem to, double chance, bool isGroups = false)
        {
            if (isGroups)
            {
                _canvas.Children.Add(new ConnectionArrow(from, to, chance));
                return;
            }

            if ((to.GetType() == typeof(InBuffItem)) ||
                (from.GetType() == to.GetType() && to.GetType() != typeof(NodeItem)))
            {
                MessageBox.Show("Невозможно добавить связь", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_canvas.Children.OfType<ConnectionArrow>().Count(
                x => Equals(x.FromItem, from) && Equals(x.TargetItem, to)) == 0)
            {
                _canvas.Children.Add(new ConnectionArrow(from, to, chance));
            }
            else MessageBox.Show("Такая связь уже имеется", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Сохранить текущие данные в файлы
        /// </summary>
        public void SaveAll()
        {
            Services.SaveToFile.Save();
        }

        public void Remove(DiagramItem item)
        {
            Items.Remove(item);
            _canvas.Children.Remove(item);
        }
        public void Remove(ConnectionArrow arrow)
        {
            ConnectionArrows.Remove(arrow);
            _canvas.Children.Remove(arrow);
        }
    }
}
