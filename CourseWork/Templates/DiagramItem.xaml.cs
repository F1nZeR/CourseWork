﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using CourseWork.Manager;
using CourseWork.Properties;
using GMap.NET;
using GroupItem = CourseWork.Templates.Elements.GroupItem;

namespace CourseWork.Templates
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
        public String LabelName
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
            miAddToNewGroup.IsEnabled = miAddToExistingGroup.IsEnabled = !Settings.Default.AutoGrouping;

            // есть, в какую группу добавить элемент
            if (DiagramItemManager.Instance.GroupDeviceses.Count > 0)
            {
                miAddToExistingGroup.IsEnabled = true;
                miAddToExistingGroup.Items.Clear();

                foreach (var groupDevicese in DiagramItemManager.Instance.GroupDeviceses)
                {
                    var menuItem = new MenuItem
                    {
                        Header = groupDevicese.LabelName
                    };
                    menuItem.Click += MenuItemAddToGroupOnClick;

                    miAddToExistingGroup.Items.Add(menuItem);
                }
            }
            else
            {
                miAddToExistingGroup.IsEnabled = false;
                miAddToExistingGroup.Items.Clear();
            }
        }

        private void MenuItemAddToGroupOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var index = miAddToExistingGroup.Items.IndexOf(sender);
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
            Panel.GetZIndex(this);
            Panel.SetZIndex(this, 100);
        }

        public void RemoveDragEffect()
        {
            Effect = null;
            Panel.SetZIndex(this, 0);
        }

        private void MiAddToNewGroupClick(object sender, RoutedEventArgs e)
        {
            var window = new Windows.NewGroupWindow {Owner = Application.Current.MainWindow};
            window.ShowDialog();
            var group = DiagramItemManager.Instance.AddNewItemGroupItem(new Point());
            group.LabelName = window.Value;
            group.ComposeSize = Maps.MapHelper.Instance.MapZoom;
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
            Maps.MapHelper.Instance.GroupElements();
        }
    }
}