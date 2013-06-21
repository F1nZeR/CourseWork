using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.Templates
{
    public class GroupDevices : DiagramItem
    {
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
                foreach (var connectionArrow in ConnectionArrows)
                {
                    if (connectionArrow.TargetItem.Visibility == Visibility.Hidden &&
                        connectionArrow.FromItem.Visibility == Visibility.Hidden)
                    {
                        connectionArrow.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        public void Decompose()
        {
            Visibility = Visibility.Hidden;
            foreach (var diagramItem in _items)
            {
                diagramItem.Visibility = Visibility.Visible;
                foreach (var connectionArrow in ConnectionArrows)
                {
                    if (connectionArrow.TargetItem.Visibility == Visibility.Visible &&
                        connectionArrow.FromItem.Visibility == Visibility.Visible)
                    {
                        connectionArrow.Visibility = Visibility.Visible;
                    }
                }
            }

            _items.Clear();
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
    }
}
