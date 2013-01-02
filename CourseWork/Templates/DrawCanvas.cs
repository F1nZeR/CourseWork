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

        private Point _startPoint;
        private bool _isDragging, _addNewElement;
        private DiagramItem _selectedElement, _fromElement;
        private int _idOfNewElement;
        private Line _line;
        private readonly Dictionary<DiagramItem, Point> _selectedItems = new Dictionary<DiagramItem, Point>();

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
                if (_isDragging)
                {
                    Point currentPosition = e.GetPosition(this);
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
                foreach (var item in DiagramItemManager.Instance.SelectedItems)
                {
                    item.IsSelected = false;
                }
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
                    foreach (var item in DiagramItemManager.Instance.SelectedItems)
                    {
                        item.IsSelected = false;
                    }
                }
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    _selectedElement.IsSelected = !_selectedElement.IsSelected;
                } else if (!_selectedElement.IsSelected)
                {
                    _selectedElement.IsSelected = true;
                }

                this.CaptureMouse();

                _isDragging = true;

                _selectedItems.Clear();
                foreach (var selectedItem in DiagramItemManager.Instance.SelectedItems)
                {
                    selectedItem.AddDragEffect();
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
