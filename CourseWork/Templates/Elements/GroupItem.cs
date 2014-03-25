using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using GMap.NET;
using SeMOEditor.Maps;

namespace SeMOEditor.Templates.Elements
{
    public class GroupItem : DiagramItem
    {
        private PointLatLng _pointLatLng;
        public override PointLatLng PositionLatLng
        {
            get { return _pointLatLng; }
            set
            {
                _pointLatLng = value;
                _items.ForEach(x => MapHelper.Instance.UpdateLatLngPoses(x));
            }
        }

        protected override void UpdateImage()
        {
            imgNavigate.Source = new BitmapImage(new Uri("../Images/Group.png", UriKind.RelativeOrAbsolute));
        }
        
        public double ComposeSize { get; set; }
        private readonly List<DiagramItem> _items;

        public GroupItem(string name, double x, double y) : base(name)
        {
            ComposeSize = 10;
            _items = new List<DiagramItem>();
            Move(x, y);
        }

        public void Compose()
        {
            Visibility = Visibility.Visible;

            foreach (var diagramItem in _items)
            {
                // если спрятан в настройках - пропускаем
                if (diagramItem.Visibility == Visibility.Collapsed) continue;
                
                diagramItem.Visibility = Visibility.Hidden;

                foreach (var connectionArrow in diagramItem.ConnectionArrows.Where(x => x.Visibility == Visibility.Visible))
                {
                    connectionArrow.Visibility = Visibility.Hidden;
                }
            }
        }

        public void Decompose()
        {
            Visibility = Visibility.Hidden;

            foreach (var diagramItem in _items)
            {
                // если спрятан в настройках - пропускаем
                if (diagramItem.Visibility == Visibility.Collapsed) continue;

                diagramItem.Visibility = Visibility.Visible;
                foreach (var connectionArrow in diagramItem.ConnectionArrows.Where(x => x.Visibility == Visibility.Hidden))
                {
                    connectionArrow.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Обновить текущее представление в соответствии с приближением карты
        /// </summary>
        /// <param name="zoom">размер приближения</param>
        public void UpdateCurrentView(double zoom)
        {
            // пустая группа - не показывать и ничего не делать
            if (_items.Count == 0)
            {
                Visibility = Visibility.Hidden;
                return;
            }

            if (zoom <= ComposeSize)
            {
                Compose();
            }
            else
            {
                Decompose();
                foreach (var groupDevices in _items.OfType<GroupItem>())
                {
                    groupDevices.UpdateCurrentView(zoom);
                }
            }
        }

        public override void Move(double x, double y)
        {
            var diffX = x - CenterPoint.X;
            var diffY = y - CenterPoint.Y;
            CenterPoint = new Point(x, y);

            foreach (var diagramItem in _items)
            {
                diagramItem.Move(diagramItem.CenterPoint.X + diffX, diagramItem.CenterPoint.Y + diffY);
                diagramItem.UpdateConnectionArrows();
            }

            UpdateConnectionArrows();
        }

        public void Add(DiagramItem item)
        {
            if (_items.Contains(item) == false) _items.Add(item);
        }

        public void Remove(DiagramItem item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Получить элементы группы
        /// </summary>
        /// <returns></returns>
        public List<DiagramItem> GetItems()
        {
            return _items;
        }

        /// <summary>
        /// Разместить элемент группы по центру (относительно сгруппированных элементов)
        /// </summary>
        public void MakeCentered()
        {
            var centerPoint = new Point(_items.Sum(x => x.PositionLatLng.Lat)/_items.Count,
                                        _items.Sum(x => x.PositionLatLng.Lng)/_items.Count);
            var items = _items.ToList();
            _items.Clear();
            PositionLatLng = new PointLatLng(centerPoint.X, centerPoint.Y);
            MapHelper.Instance.UpdateScreenCoords(this);
            _items.AddRange(items);
        }
    }
}
