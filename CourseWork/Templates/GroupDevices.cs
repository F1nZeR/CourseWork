using System.Collections.Generic;
using System.Windows;
using GMap.NET;

namespace CourseWork.Templates
{
    public class GroupDevices : DiagramItem
    {
        private PointLatLng _pointLatLng;
        public override PointLatLng PositionLatLng
        {
            get { return _pointLatLng; }
            set
            {
                _pointLatLng = value;
                _items.ForEach(x => Maps.MapHelper.Instance.UpdateLatLngPoses(x));
            }
        }


        public double ComposeSize { get; set; }
        private readonly List<DiagramItem> _items;

        public GroupDevices(string name, DiagramItemType type, double x, double y) : base(name, type)
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
                diagramItem.Visibility = Visibility.Hidden;
                foreach (var connectionArrow in diagramItem.ConnectionArrows)
                {
                    connectionArrow.Visibility = Visibility.Hidden;
                }
            }
        }

        public void Decompose()
        {
            foreach (var diagramItem in _items)
            {
                diagramItem.Visibility = Visibility.Visible;
                foreach (var connectionArrow in diagramItem.ConnectionArrows)
                {
                    connectionArrow.Visibility = Visibility.Visible;
                }
            }

            //_items.Clear();
            //ConnectionArrows.Clear();
        }

        /// <summary>
        /// Обновить текущее представление в соответствии с приближением карты
        /// </summary>
        /// <param name="zoom">размер приближения</param>
        public void UpdateCurrentView(double zoom)
        {
            if (_items.Count == 0) return;

            if (zoom <= ComposeSize)
            {
                Compose();
            }
            else
            {
                Decompose();
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

        public List<DiagramItem> GetItems()
        {
            return _items;
        }
    }
}
