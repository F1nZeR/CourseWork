using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using CourseWork.Manager;
using CourseWork.Utilities.Helpers;

namespace CourseWork.Templates
{
    public class DrawCanvas : Canvas
    {
        public DrawCanvas()
        {
            this.PreviewMouseLeftButtonDown += CanvasPreviewMouseLeftButtonDown;
            this.PreviewMouseMove += CanvasPreviewMouseMove;
            this.PreviewMouseLeftButtonUp += CanvasPreviewMouseLeftButtonUp;
            DiagramItemManager.SetInstance(this);
        }

        private Point _startPoint, origMouseDownPoint;
        private bool _isDragging, _addNewElement, isLeftMouseButtonDownOnWindow, isDraggingSelectionRect;
        private DiagramItem _selectedElement, _fromElement;
        private int _idOfNewElement, DragThreshold = 5;
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
                DiagramItemManager.Instance.AddNewLink(_fromElement, e.Source as DiagramItem, 0);
            }
            if (this.IsMouseCaptured)
            {
                if (_isDragging)
                {
                    _isDragging = false;
                    foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                    {
                        selectedItem.RemoveDragEffect();
                    }
                }

                if (isDraggingSelectionRect)
                {
                    isDraggingSelectionRect = false;
                    ApplyDragSelectionRect();

                    e.Handled = true;
                }

                if (isLeftMouseButtonDownOnWindow)
                {
                    isLeftMouseButtonDownOnWindow = false;
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
            if (this.IsMouseCaptured)
            {
                Point currentPosition = e.GetPosition(this);
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
                    }
                }

                if (isDraggingSelectionRect)
                {
                    Point curMouseDownPoint = e.GetPosition(this);
                    UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);

                    e.Handled = true;
                }
                else if (isLeftMouseButtonDownOnWindow)
                {
                    Point curMouseDownPoint = e.GetPosition(this);
                    var dragDelta = curMouseDownPoint - origMouseDownPoint;
                    double dragDistance = Math.Abs(dragDelta.Length);
                    if (dragDistance > DragThreshold)
                    {
                        isDraggingSelectionRect = true;
                        ClearSelection();

                        InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
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

        private void ClearSelection()
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

            foreach (var diagramItem in DiagramItemManager.Instance.Items)
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
            if (_addNewElement && _idOfNewElement == 0)
            {
                DiagramItemManager.Instance.AddNewItem(DiagramItemType.Device, e.GetPosition(this));
                ResetAddNew();
                return;
            }

            if (Equals(e.Source, this) || !(e.Source is DiagramItem))
            {
                ClearSelection();

                isLeftMouseButtonDownOnWindow = true;
                origMouseDownPoint = e.GetPosition(this);

                this.CaptureMouse();
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

                this.CaptureMouse();

                //_isDragging = true;

                _selectedItems.Clear();
                foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                {
                    //selectedItem.AddDragEffect();
                    _selectedItems.Add(selectedItem, selectedItem.Position);
                }
            }
            e.Handled = true;
        }

        public void StartAddNewElement(int id)
        {
            foreach (var result in this.Children.OfType<ConnectionArrow>())
            {
                result.IsHitTestVisible = false;
            }
            _addNewElement = true;
            Mouse.SetCursor(Cursors.Cross);
            _idOfNewElement = id;

            _line = new Line();
            this.Children.Add(_line);
        }

        public void ResetAddNew()
        {
            foreach (var result in this.Children.OfType<ConnectionArrow>())
            {
                result.IsHitTestVisible = true;
            }
            _addNewElement = false;
            this.Children.Remove(_line);
            Mouse.SetCursor(Cursors.Arrow);
        }
    }
}
