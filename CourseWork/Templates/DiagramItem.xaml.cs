using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using GMap.NET;
using SeMOEditor.Manager;
using SeMOEditor.Maps;
using SeMOEditor.Properties;
using SeMOEditor.Windows;
using GroupItem = SeMOEditor.Templates.Elements.GroupItem;

namespace SeMOEditor.Templates
{
    public abstract partial class DiagramItem : UserControl
    {
        public virtual PointLatLng PositionLatLng { get; set; }
        protected abstract void UpdateImage();

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
            get { return ConnectionArrowsOut.Sum(x => x.Chance); }
        }

        private static int _currentId;
        public int Id { get; private set; }
        public string LabelName
        {
            get { return labelName.Content.ToString(); }
            set { labelName.Content = value; }
        }

        protected DiagramItem(string name)
        {
            InitializeComponent();
            Id = _currentId++;
            DataContext = this;
            ConnectionArrows = new List<ConnectionArrow>();
            LabelName = name;
            UpdateImage();

            PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            IsSelected = true;
            MiAddToNewGroup.IsEnabled = MiAddToExistingGroup.IsEnabled = !Settings.Default.AutoGrouping;

            var curGroups = DiagramItemManager.Instance.GroupDeviceses;
            if (GetType() == typeof (GroupItem))
            {
                // убираем эту группу
                curGroups = curGroups.Where(x => !x.Equals(this)).ToList();
            }

            // есть, в какую группу добавить элемент
            if (curGroups.Count > 0)
            {
                MiAddToExistingGroup.IsEnabled = true;
                MiAddToExistingGroup.Items.Clear();

                foreach (var groupDevicese in curGroups)
                {
                    var menuItem = new MenuItem
                    {
                        Header = groupDevicese.LabelName
                    };
                    menuItem.Click += MenuItemAddToGroupOnClick;

                    MiAddToExistingGroup.Items.Add(menuItem);
                }
            }
            else
            {
                MiAddToExistingGroup.IsEnabled = false;
                MiAddToExistingGroup.Items.Clear();
            }
        }

        private void MenuItemAddToGroupOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var index = MiAddToExistingGroup.Items.IndexOf(sender);
            var group = DiagramItemManager.Instance.GroupDeviceses[index];
            AddSelectedItemsToGroup(group);
        }

        protected DiagramItem(string name, double x = 0, double y = 0) : this(name)
        {
            Move(x, y);
        }

        public void UpdateConnectionArrows()
        {
            foreach (var connectionArrow in ConnectionArrows)
            {
                connectionArrow.UpdateLink();
            }
        }

        /// <summary>
        /// Переместить элемент с привязкой к центру
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void Move(double x, double y)
        {
            CenterPoint = new Point(x, y);
            UpdateConnectionArrows();
        }

        public void AddDragEffect()
        {
            Effect = new DropShadowEffect();
            Panel.SetZIndex(this, 100);
        }

        public void RemoveDragEffect()
        {
            Effect = null;
            Panel.SetZIndex(this, 0);
        }

        private void MiAddToNewGroupClick(object sender, RoutedEventArgs e)
        {
            var window = new NewGroupWindow {Owner = Application.Current.MainWindow};
            window.ShowDialog();
            var group = DiagramItemManager.Instance.AddNewItemGroupItem(new Point());
            group.LabelName = window.Value;
            group.ComposeSize = MapHelper.Instance.MapZoom;
            AddSelectedItemsToGroup(group);
        }

        private void AddSelectedItemsToGroup(GroupItem group)
        {
            foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
            {
                var inExistingGroup =
                    DiagramItemManager.Instance.GroupDeviceses.FirstOrDefault(x => x.GetItems().Contains(selectedItem));
                if (inExistingGroup != null) inExistingGroup.Remove(selectedItem);
                group.Add(selectedItem);
            }

            DiagramItemManager.Instance.SelectedItems.ForEach(x => x.IsSelected = false);
            MapHelper.Instance.GroupElements();
        }

        private void MiRenameOnClick(object sender, RoutedEventArgs e)
        {
            var rnmWindow = new RenameItemWindow(LabelName) {Owner = Application.Current.MainWindow};
            rnmWindow.ShowDialog();
            if (!string.IsNullOrEmpty(rnmWindow.Result))
            {
                LabelName = rnmWindow.Result;
            }
        }
    }
}