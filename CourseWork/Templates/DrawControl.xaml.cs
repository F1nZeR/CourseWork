﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CourseWork.Manager;
using CourseWork.Maps;
using CourseWork.Properties;
using GMap.NET;
using GMap.NET.MapProviders;

namespace CourseWork.Templates
{
    /// <summary>
    /// Логика взаимодействия для DrawControl.xaml
    /// </summary>
    public partial class DrawControl : UserControl
    {
        private bool _isRightBtnPressed;

        public DrawControl()
        {
            InitializeComponent();

            MainMap.MapProvider = GMapProviders.GoogleMap;
            MainMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            MainMap.Position = new PointLatLng(58, -37);

            PreviewKeyDown += OnPreviewKeyDown;
            Loaded += OnLoaded;

            PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
            PreviewMouseRightButtonUp += OnPreviewMouseRightButtonUp;
            PreviewMouseWheel += OnPreviewMouseWheel;
            PreviewMouseMove += OnPreviewMouseMove;

            btnAutoSize.Click += BtnAutoSizeOnClick;
        }

        private void BtnAutoSizeOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            MapHelper.Instance.FitMapToScreen();
            MapHelper.Instance.GroupElements();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            MapHelper.SetInstance(MainMap);
            DiagramItemManager.Instance.LoadDefaultElements();
            MapHelper.Instance.FitMapToScreen();

            drawCanvas.DragSelectionBorder = dragSelectionBorder;
            drawCanvas.DragSelectionCanvas = dragSelectionCanvas;

            if (DiagramItemManager.Instance.Items.Any(x => Math.Abs(x.PositionLatLng.Lat) < 0.01))
            {
                DiagramItemManager.Instance.Items.ForEach(item => MapHelper.Instance.UpdateLatLngPoses(item));
            }
            MapHelper.Instance.ReDrawElements();

            // заполнить комбобокс провайдеров
            comboBoxMapType.ItemsSource = GMapProviders.List;
            comboBoxMapType.DisplayMemberPath = "Name";
            comboBoxMapType.SelectedItem = MainMap.MapProvider;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isRightBtnPressed) MapHelper.Instance.ReDrawElements();
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs mouseWheelEventArgs)
        {
            var delta = mouseWheelEventArgs.Delta > 0 ? 1 : -1;

            MapHelper.Instance.Zoom(delta);
            MapHelper.Instance.GroupElements();
            MapHelper.Instance.ReDrawElements();
        }

        private void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            drawCanvas.Background = Brushes.Transparent;
            _isRightBtnPressed = false;
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            // обработка события MouseRightDown на элементах
            if (DiagramItemManager.Instance.Items.Any(x => x.IsMouseOver))
            {
                if (!DiagramItemManager.Instance.SelectedItems.Contains(
                    DiagramItemManager.Instance.Items.Single(x => x.IsMouseOver)))
                {
                    drawCanvas.ClearSelection();
                }
                return;
            }

            // иначе передвижение карты
            drawCanvas.Background = null;
            drawCanvas.ClearSelection();
            MainMap.CaptureMouse();
            _isRightBtnPressed = true;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                drawCanvas.ResetAddNew();
            }
        }

        private void ButtonClickAddDevice(object sender, RoutedEventArgs e)
        {
            drawCanvas.StartAddNewElement(0);
        }

        private void ButtonClickAddInBuffer(object sender, RoutedEventArgs e)
        {
            drawCanvas.StartAddNewElement(1);
        }

        private void ButtonClickAddLink(object sender, RoutedEventArgs e)
        {
            drawCanvas.StartAddNewElement(2);
        }

        private void CheckBoxClickShowInBuffer(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            if (cb.IsChecked == false)
            {
                foreach (DiagramItem item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferIn))
                {
                    item.Visibility = Visibility.Collapsed;
                    foreach (ConnectionArrow connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                foreach (DiagramItem item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferIn))
                {
                    item.Visibility = Visibility.Visible;
                    foreach (ConnectionArrow connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CheckBoxClickShowOutBuffer(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            if (cb.IsChecked == false)
            {
                foreach (DiagramItem item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferOut))
                {
                    item.Visibility = Visibility.Collapsed;
                    foreach (ConnectionArrow connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                foreach (DiagramItem item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferOut))
                {
                    item.Visibility = Visibility.Visible;
                    foreach (ConnectionArrow connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CheckBoxClickShowRouting(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            if (cb.IsChecked == false)
            {
                foreach (ConnectionArrow item in DiagramItemManager.Instance.ConnectionArrows)
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                var items = new List<ConnectionArrow>();
                if (cbShowInBuffer.IsChecked == false)
                {
                    items.AddRange(DiagramItemManager.Instance.ConnectionArrows.Where(
                        x => x.FromItem.DiagramItemType != DiagramItemType.BufferIn));
                }
                if (cbShowOutBuffer.IsChecked == false)
                {
                    if (cbShowInBuffer.IsChecked == false)
                    {
                        items.RemoveAll(x => x.TargetItem.DiagramItemType == DiagramItemType.BufferOut);
                    }
                    else
                    {
                        items.AddRange(DiagramItemManager.Instance.ConnectionArrows.Where(
                            x => x.TargetItem.DiagramItemType != DiagramItemType.BufferOut));
                    }
                }

                if (cbShowInBuffer.IsChecked == true && cbShowOutBuffer.IsChecked == true)
                {
                    items = DiagramItemManager.Instance.ConnectionArrows;
                }

                foreach (ConnectionArrow connectionArrow in items)
                {
                    connectionArrow.Visibility = Visibility.Visible;
                }
            }
        }

        private void CheckBoxClickShowLoopback(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            if (cb.IsChecked == false)
            {
                foreach (ConnectionArrow item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.ConnectionArrowType == ConnectionArrowType.Loopback))
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                foreach (ConnectionArrow item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.ConnectionArrowType == ConnectionArrowType.Loopback))
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }

        private void ComboBoxLoopbackViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.OutArrowType = ((ComboBox) sender).SelectedIndex;
            Settings.Default.Save();
            foreach (ConnectionArrow item in DiagramItemManager.Instance.ConnectionArrows.Where(
                x => x.TargetItem.DiagramItemType == DiagramItemType.BufferOut))
            {
                item.ConnectionArrowType = Settings.Default.OutArrowType == 0
                                               ? ConnectionArrowType.Normal
                                               : ConnectionArrowType.OutArrowType;
            }
        }

        private void ComboBoxNormalArrowViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.NormalArrowType = ((ComboBox) sender).SelectedIndex;
            Settings.Default.Save();
            foreach (ConnectionArrow item in DiagramItemManager.Instance.ConnectionArrows.Where(
                x => x.ConnectionArrowType == ConnectionArrowType.Normal))
            {
                item.ViewType = Settings.Default.NormalArrowType;
            }
        }

        private void CbAutoGroupingClicked(object sender, RoutedEventArgs e)
        {
            MapHelper.Instance.RemovePreviousGroups();
            MapHelper.Instance.GroupElements();
        }
    }
}