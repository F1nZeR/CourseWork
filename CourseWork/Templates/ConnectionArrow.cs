using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CourseWork.Windows;

namespace CourseWork.Templates
{
    public enum ConnectionArrowType
    {
        Normal,
        Loopback,
        OutArrowType
    }

    public class ConnectionArrow : Shape
    {
        #region Свойства

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register("HeadWidth", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty HeadHeightProperty = DependencyProperty.Register("HeadHeight", typeof(double), typeof(ConnectionArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ViewTypeProperty = DependencyProperty.Register(
            "ViewType", typeof(int), typeof(ConnectionArrow), new FrameworkPropertyMetadata(
                0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ConnectionArrowTypeProperty =
            DependencyProperty.Register("ArrowType", typeof(ConnectionArrowType), typeof(ConnectionArrow),
                                        new FrameworkPropertyMetadata(
                                            default(ConnectionArrowType),
                                            FrameworkPropertyMetadataOptions.AffectsRender |
                                            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public ConnectionArrowType ConnectionArrowType
        {
            get { return (ConnectionArrowType)GetValue(ConnectionArrowTypeProperty); }
            set { SetValue(ConnectionArrowTypeProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X1
        {
            get { return (double)base.GetValue(X1Property); }
            set { base.SetValue(X1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y1
        {
            get { return (double)base.GetValue(Y1Property); }
            set { base.SetValue(Y1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X2
        {
            get { return (double)base.GetValue(X2Property); }
            set { base.SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y2
        {
            get { return (double)base.GetValue(Y2Property); }
            set { base.SetValue(Y2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double HeadWidth
        {
            get { return (double)base.GetValue(HeadWidthProperty); }
            set { base.SetValue(HeadWidthProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double HeadHeight
        {
            get { return (double)base.GetValue(HeadHeightProperty); }
            set { base.SetValue(HeadHeightProperty, value); }
        }

        public int ViewType
        {
            get { return (int)GetValue(ViewTypeProperty); }
            set { SetValue(ViewTypeProperty, value); }
        }

        #endregion

        protected override Geometry DefiningGeometry
        {
            get
            {
                var geometry = new StreamGeometry { FillRule = FillRule.EvenOdd };

                using (StreamGeometryContext context = geometry.Open())
                {
                    InternalDrawArrowGeometry(context);
                }

                geometry.Freeze();

                return geometry;
            }
        }

        public DiagramItem FromItem { get; set; }
        public DiagramItem TargetItem { get; set; }
        public double Chance { get; set; }

        public ConnectionArrow(DiagramItem fromItem, DiagramItem toItem, double chance)
        {
            FromItem = fromItem;
            TargetItem = toItem;
            ConnectionArrowType = Equals(FromItem, TargetItem)
                                      ? ConnectionArrowType.Loopback
                                      : ConnectionArrowType.Normal;

            ViewType = ConnectionArrowType == ConnectionArrowType.Normal
                           ? Properties.Settings.Default.NormalArrowType
                           : Properties.Settings.Default.OutArrowType;

            if (toItem.DiagramItemType == DiagramItemType.BufferOut)
            {
                ConnectionArrowType = Properties.Settings.Default.OutArrowType == 0
                                          ? ConnectionArrowType.Normal
                                          : ConnectionArrowType.OutArrowType;
            }

            Chance = chance;

            this.UpdateLink();
            this.SetDeafultStyle();

            fromItem.ConnectionArrows.Add(this);
            toItem.ConnectionArrows.Add(this);

            this.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += (sender, args) => SetDeafultStyle();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetColoredStyle();
            this.ToolTip = string.Format("{0}%", Chance * 100);
        }

        /// <summary>
        /// Применить цветной стиль для линии
        /// </summary>
        private void SetColoredStyle()
        {
            Stroke = Brushes.Red;
            StrokeThickness = 3;
            Canvas.SetZIndex(this, int.MaxValue);
        }

        /// <summary>
        /// Вернуть стиль по-умолчанию для линии
        /// </summary>
        private void SetDeafultStyle()
        {
            Stroke = Brushes.Black;
            StrokeThickness = 1.5;
            Canvas.SetZIndex(this, 1);
            HeadHeight = 4;
            HeadWidth = 10;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var wndChange = new ChangeLinkWindow
                                    {
                                        Top = e.GetPosition(this).Y,
                                        Left = e.GetPosition(this).X,
                                        Title = (this.Chance * 100) + "%"
                                    };

                SetColoredStyle();
                wndChange.ShowDialog();
                SetDeafultStyle();

                var chance = wndChange.Chance;
                if (chance == 0) return;
                var tempChance = Chance;
                Chance = chance;
                if (FromItem.SumChanceOut > 1)
                {
                    MessageBox.Show("Сумма вероятностей > 100%", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Chance = tempChance;
                }
            }
        }

        public void UpdateLink()
        {
            var fromPoint = FromItem.CenterPoint;
            var toPoint = TargetItem.CenterPoint;

            X1 = fromPoint.X;
            X2 = toPoint.X;
            Y1 = fromPoint.Y;
            Y2 = toPoint.Y;
        }

        private void InternalDrawArrowGeometry(StreamGeometryContext context)
        {
            Point pt1, pt2, pt3, pt4;
            if (ConnectionArrowType == ConnectionArrowType.Normal)
            {
                var theta = Math.Atan2(Y1 - Y2, X1 - X2);
                var sint = Math.Sin(theta);
                var cost = Math.Cos(theta);

                switch (ViewType)
                {
                    case 0: // прямая в центр
                        pt1 = new Point(X1, Y1);
                        pt2 = new Point(X2, Y2);

                        pt3 = new Point(
                            X2 + (HeadWidth * cost - HeadHeight * sint),
                            Y2 + (HeadWidth * sint + HeadHeight * cost));

                        pt4 = new Point(
                            X2 + (HeadWidth * cost + HeadHeight * sint),
                            Y2 - (HeadHeight * cost - HeadWidth * sint));

                        context.BeginFigure(pt1, true, false);
                        context.LineTo(pt2, true, true);
                        context.LineTo(pt3, true, true);
                        context.LineTo(pt2, true, true);
                        context.LineTo(pt4, true, true);
                        break;

                    case 1: // безье (слабое) в центр
                        pt1 = new Point(X1, Y1);
                        pt2 = new Point(X2, Y2);

                        var ptTemp1 = new Point(
                            X1 - (X1 - X2) / 2,
                            Y1 - (Y1 - Y2) / 3);

                        var ptTemp2 = new Point(
                            X1 - (X1 - X2) / 4 * 3,
                            Y1 - (Y1 - Y2) / 3 * 2);

                        pt3 = new Point(
                            X2 + (HeadWidth * cost - HeadHeight * sint),
                            Y2 + (HeadWidth * sint + HeadHeight * cost));

                        pt4 = new Point(
                            X2 + (HeadWidth * cost + HeadHeight * sint),
                            Y2 - (HeadHeight * cost - HeadWidth * sint));

                        context.BeginFigure(pt1, true, false);
                        context.BezierTo(ptTemp1, ptTemp2, pt2, true, true);
                        context.LineTo(pt3, true, true);
                        context.LineTo(pt2, true, true);
                        context.LineTo(pt4, true, true);
                        break;
                }
            }
            else if (ConnectionArrowType == ConnectionArrowType.Loopback)
            {
                pt1 = new Point(FromItem.Position.X - 5, FromItem.Position.Y + 83);
                pt2 = new Point(pt1.X + 10, pt1.Y - 10);

                pt3 = new Point(pt2.X - 4, pt2.Y + 9);
                pt4 = new Point(pt2.X - 6, pt2.Y + 16);

                context.BeginFigure(pt1, true, false);
                context.ArcTo(pt2, new Size(6, 6), 125, true, SweepDirection.Clockwise, true, true);
                context.ArcTo(pt1, new Size(6, 6), 125, true, SweepDirection.Clockwise, true, true);
                context.LineTo(pt3, true, true);
                context.LineTo(pt1, true, true);
                context.LineTo(pt4, true, true);
            }
            else
            {
                pt1 = new Point(X1 + 20, Y1 - 25);
                pt2 = new Point(pt1.X + 10, pt1.Y - 10);

                pt3 = new Point(pt2.X - 6, pt2.Y + 2);
                pt4 = new Point(pt2.X - 2, pt2.Y + 6);

                context.BeginFigure(pt1, true, false);
                context.LineTo(pt2, true, true);
                context.LineTo(pt3, true, true);
                context.LineTo(pt2, true, true);
                context.LineTo(pt4, true, true);
            }

        }
    }
}