using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CourseWork.Templates;
using CourseWork.Utilities.Helpers;

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
                    _canvas.Children.OfType<DiagramItem>().Where(x => x.DiagramItemType == DiagramItemType.Device).
                        ToList();
            }
        }
         
        public List<ConnectionArrow> ConnectionArrows
        {
            get { return _canvas.Children.OfType<ConnectionArrow>().ToList(); }
        }

        public List<DiagramItem> Items
        {
            get { return _canvas.Children.OfType<DiagramItem>().ToList(); }
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

        /// <summary>
        /// Загрузить элементы из matrix.txt | matrixRow.txt
        /// </summary>
        public void LoadDefaultElements()
        {
            var matrix = Services.ReadFromFile.ReadMatrix("./matrix.txt");
            var vector = Services.ReadFromFile.ReadMatrixRow("./matrixRow.txt");
            var positions = Services.ReadFromFile.ReadLatLngPositions("./matrixLatLng.txt");

            var outBuffer = new DiagramItem("inBuf", DiagramItemType.BufferIn, 0, 200);
            _canvas.Children.Add(outBuffer);

            var inBuffer = new DiagramItem("outBuf", DiagramItemType.BufferOut, _canvas.ActualWidth - 60, 200);
            _canvas.Children.Add(inBuffer);


            double curX = 100, curY = 200;
            for (int i = 1; i <= matrix.Rows; i++)
            {
                var dItem = new DiagramItem(i.ToString(), DiagramItemType.Device, curX, curY);
                _canvas.Children.Add(dItem);
                if (vector[i] > 0) AddNewLink(outBuffer, dItem, vector[i]);
                var chance = MatrixHelper.CalculateRowChance(matrix, i);
                if (chance <= 1.0f)
                {
                    AddNewLink(dItem, inBuffer, 1.0f - chance);
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

        public void AddNewItem(DiagramItemType type, Point pos)
        {
            switch (type)
            {
                case DiagramItemType.Device:
                    var dItem = new DiagramItem((Instance.DiagramItemsDevices.Count + 1).ToString(), type,
                                                pos.X-30, pos.Y-40);
                    _canvas.Children.Add(dItem);
                    break;
                case DiagramItemType.BufferIn:
                    var dItemBuf = new DiagramItem("inBuf", type, pos.X, pos.Y);
                    _canvas.Children.Add(dItemBuf);
                    break;
            }
        }

        public void AddNewLink(DiagramItem from, DiagramItem to, double chance)
        {
            if (_canvas.Children.OfType<ConnectionArrow>().Count(
                x => Equals(x.FromItem, @from) && Equals(x.TargetItem, @to)) == 0)
            {
                _canvas.Children.Add(new ConnectionArrow(from, to, chance));
            }
            else MessageBox.Show("Такая связь уже имеется", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
