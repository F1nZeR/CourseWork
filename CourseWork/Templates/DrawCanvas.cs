using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SeMOEditor.Manager;
using SeMOEditor.Maps;

namespace SeMOEditor.Templates
{
    public class DrawCanvas : Canvas
    {
        public DrawCanvas()
        {
            PreviewMouseLeftButtonDown += CanvasPreviewMouseLeftButtonDown;
            PreviewMouseMove += CanvasPreviewMouseMove;
            PreviewMouseLeftButtonUp += CanvasPreviewMouseLeftButtonUp;
            DiagramItemManager.SetInstance(this);
        }

        private Point _startPoint, _origMouseDownPoint;
        private bool _isDragging, _addNewElement, _isLeftMouseButtonDownOnWindow, _isDraggingSelectionRect;
        private DiagramItem _selectedElement, _fromElement;
        private int _idOfNewElement;
        private const int DragThreshold = 5;
        private Line _line;
        private readonly Dictionary<DiagramItem, Point> _selectedItems = new Dictionary<DiagramItem, Point>();
        public Canvas DragSelectionCanvas { get; set; }
        public Border DragSelectionBorder { get; set; }

        private void CanvasPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_addNewElement)
            {
                ResetAddNew();
                var target = e.Source as DiagramItem;
                if (target == null) return;
                DiagramItemManager.Instance.AddNewLink(_fromElement, (DiagramItem) e.Source, 0);
            }
            if (IsMouseCaptured)
            {
                if (_isDragging)
                {
                    _isDragging = false;
                    foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                    {
                        selectedItem.RemoveDragEffect();
                    }
                }

                if (_isDraggingSelectionRect)
                {
                    _isDraggingSelectionRect = false;
                    ApplyDragSelectionRect();

                    e.Handled = true;
                }

                if (_isLeftMouseButtonDownOnWindow)
                {
                    _isLeftMouseButtonDownOnWindow = false;
                    this.ReleaseMouseCapture();

                    e.Handled = true;
                }
                this.ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        private void CanvasPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_addNewElement)
            {
                Mouse.SetCursor(Cursors.Cross);
                _line.X2 = e.GetPosition(this).X;
                _line.Y2 = e.GetPosition(this).Y;
            }
            if (IsMouseCaptured)
            {
                var currentPosition = e.GetPosition(this);
                if (Math.Abs((_startPoint - currentPosition).Length) > DragThreshold)
                {
                    _isDragging = true;
                    foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                    {
                        selectedItem.AddDragEffect();
                    }
                }
                if (_isDragging)
                {
                    foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                    {
                        var item = _selectedItems.Single(x => x.Key.Equals(selectedItem));
                        double elementLeft = (currentPosition.X - _startPoint.X) +
                            item.Value.X;
                        double elementTop = (currentPosition.Y - _startPoint.Y) +
                            item.Value.Y;

                        selectedItem.Move(elementLeft, elementTop);
                        MapHelper.Instance.UpdateLatLngPoses(selectedItem);
                    }
                }

                if (_isDraggingSelectionRect)
                {
                    Point curMouseDownPoint = e.GetPosition(this);
                    UpdateDragSelectionRect(_origMouseDownPoint, curMouseDownPoint);

                    e.Handled = true;
                }
                else if (_isLeftMouseButtonDownOnWindow)
                {
                    Point curMouseDownPoint = e.GetPosition(this);
                    var dragDelta = curMouseDownPoint - _origMouseDownPoint;
                    double dragDistance = Math.Abs(dragDelta.Length);
                    if (dragDistance > DragThreshold)
                    {
                        _isDraggingSelectionRect = true;
                        ClearSelection();

                        InitDragSelectionRect(_origMouseDownPoint, curMouseDownPoint);
                    }

                    e.Handled = true;
                }
            }
        }

        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);

            DragSelectionCanvas.Visibility = Visibility.Visible;
        }

        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            SetLeft(DragSelectionBorder, x);
            SetTop(DragSelectionBorder, y);
            DragSelectionBorder.Width = width;
            DragSelectionBorder.Height = height;
        }

        public void ClearSelection()
        {
            foreach (var item in DiagramItemManager.Instance.SelectedItems)
            {
                item.IsSelected = false;
            }
        }

        private void ApplyDragSelectionRect()
        {
            DragSelectionCanvas.Visibility = Visibility.Collapsed;

            double x = GetLeft(DragSelectionBorder);
            double y = GetTop(DragSelectionBorder);
            double width = DragSelectionBorder.Width;
            double height = DragSelectionBorder.Height;
            var dragRect = new Rect(x, y, width, height);

            dragRect.Inflate(width / 10, height / 10);

            ClearSelection();

            foreach (var diagramItem in DiagramItemManager.Instance.Items.Where(item => item.Visibility == Visibility.Visible))
            {
                var itemRect = new Rect(diagramItem.Position.X, diagramItem.Position.Y, diagramItem.Width, diagramItem.Height);
                if (dragRect.Contains(itemRect))
                {
                    diagramItem.IsSelected = true;
                }
            }
        }

        private void CanvasPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_addNewElement)
            {
                switch (_idOfNewElement)
                {
                    case 0:
                        DiagramItemManager.Instance.AddNewItemDevice(e.GetPosition(this));
                        ResetAddNew();
                        return;

                    case 1:
                        DiagramItemManager.Instance.AddNewItemBufferIn(e.GetPosition(this));
                        ResetAddNew();
                        return;
                }
            }

            if (Equals(e.Source, this) || !(e.Source is DiagramItem))
            {
                ClearSelection();

                _isLeftMouseButtonDownOnWindow = true;
                _origMouseDownPoint = e.GetPosition(this);

                CaptureMouse();
                return;
            }

            if (_addNewElement)
            {
                _line.X1 = e.GetPosition(this).X;
                _line.Y1 = e.GetPosition(this).Y;
                _line.Stroke = Brushes.Black;
                _line.StrokeThickness = 3;
                _line.IsHitTestVisible = false;
                _fromElement = e.Source as DiagramItem;
                return;
            }

            if (!_isDragging)
            {
                _startPoint = e.GetPosition(this);
                _selectedElement = (DiagramItem) e.Source;
                if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !_selectedElement.IsSelected)
                {
                    ClearSelection();
                }
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    _selectedElement.IsSelected = !_selectedElement.IsSelected;
                } else if (!_selectedElement.IsSelected)
                {
                    _selectedElement.IsSelected = true;
                }

                CaptureMouse();

                //_isDragging = true;

                _selectedItems.Clear();
                foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                {
                    //selectedItem.AddDragEffect();
                    _selectedItems.Add(selectedItem, selectedItem.CenterPoint);
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// Добавление элемента на Canvas
        /// </summary>
        /// <param name="id">0 устр-во; 1 - вх. буффер; 2 - связь; 3 - группа</param>
        public void StartAddNewElement(int id)
        {
            foreach (var result in Children.OfType<ConnectionArrow>())
            {
                result.IsHitTestVisible = false;
            }
            _addNewElement = true;
            Mouse.SetCursor(Cursors.Cross);
            _idOfNewElement = id;

            _line = new Line();
            Children.Add(_line);
        }

        /// <summary>
        /// Выключить режим добавления
        /// </summary>
        public void ResetAddNew()
        {
            foreach (var result in this.Children.OfType<ConnectionArrow>())
            {
                result.IsHitTestVisible = true;
            }
            _addNewElement = false;
            Children.Remove(_line);
            Mouse.SetCursor(Cursors.Arrow);
        }
    }
}
