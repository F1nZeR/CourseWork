using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using GMap.NET;

namespace CourseWork.Templates
{
    public enum DiagramItemType
    {
        BufferIn,
        BufferOut,
        Device
    }

    public partial class DiagramItem : UserControl
    {
        public PointLatLng PositionLatLng { get; set; }
        private int _oldZIndex;

        public DiagramItemType DiagramItemType { get; private set; }

        public List<ConnectionArrow> ConnectionArrowsOut
        {
            get { return ConnectionArrows.Where(x => x.FromItem.Equals(this)).ToList(); }
        }

        public List<ConnectionArrow> ConnectionArrowsIn
        {
            get { return ConnectionArrows.Where(x => x.TargetItem.Equals(this)).ToList(); }
        }

        public List<ConnectionArrow> ConnectionArrows { get; set; }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof (bool), typeof (DiagramItem), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public Point CenterPoint
        {
            get
            {
                return new Point(
                    (double)GetValue(Canvas.LeftProperty) + (double)GetValue(WidthProperty) / 2.0,
                    (double)GetValue(Canvas.TopProperty) + (double)GetValue(HeightProperty) / 2.0 + 7);
            }

            set
            {
                SetValue(Canvas.LeftProperty, value.X - (double)GetValue(ActualWidthProperty) / 2.0);
                SetValue(Canvas.TopProperty, value.Y - (double)GetValue(ActualHeightProperty) / 2.0);
            }
        }

        public Point Position
        {
            get
            {
                return new Point(
                    (double)GetValue(Canvas.LeftProperty),
                    (double)GetValue(Canvas.TopProperty));
            }

            set
            {
                SetValue(Canvas.LeftProperty, value.X);
                SetValue(Canvas.TopProperty, value.Y);
            }
        }

        public double SumChanceOut
        {
            get
            {
                return ConnectionArrowsOut.Sum(x => x.Chance);
            }
        }

        private static readonly Random _rnd = new Random();

        public DiagramItem(string name, DiagramItemType type, double x = 0, double y = 0)
        {
            InitializeComponent();
            this.DataContext = this;
            ConnectionArrows = new List<ConnectionArrow>();

            this.PositionLatLng = new PointLatLng(_rnd.Next(150) - 75, _rnd.Next(150) - 75);

            labelName.Content = name;
            DiagramItemType = type;
            switch (type)
            {
                case DiagramItemType.Device:
                    imgNavigate.Source = new BitmapImage(new Uri("../Images/Persist.png", UriKind.RelativeOrAbsolute));
                    break;
                case DiagramItemType.BufferIn:
                    imgNavigate.Source = new BitmapImage(new Uri("../Images/Setting.png", UriKind.RelativeOrAbsolute));
                    break;
                case DiagramItemType.BufferOut:
                    imgNavigate.Source = new BitmapImage(new Uri("../Images/Setting.png", UriKind.RelativeOrAbsolute));
                    break;
            }

            this.Move(x, y);
        }

        private void UpdateConnectionArrows()
        {
            foreach (var connectionArrow in ConnectionArrows)
            {
                connectionArrow.UpdateLink();
            }
        }

        public void Move(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            UpdateConnectionArrows();
        }

        public void AddDragEffect()
        {
            this.Effect = new DropShadowEffect();
            _oldZIndex = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, 100);
        }

        public void RemoveDragEffect()
        {
            this.Effect = null;
            //Canvas.SetZIndex(this, _oldZIndex);
            Canvas.SetZIndex(this, 0);
        }
    }
}