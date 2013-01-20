using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseWork.Manager;

namespace CourseWork.Templates
{
    /// <summary>
    /// Логика взаимодействия для DrawControl.xaml
    /// </summary>
    public partial class DrawControl : UserControl
    {
        public DrawControl()
        {
            InitializeComponent();
            this.PreviewKeyDown += OnPreviewKeyDown;
            this.Loaded += (sender, args) =>
                               {
                                   drawCanvas.DragSelectionBorder = dragSelectionBorder;
                                   drawCanvas.DragSelectionCanvas = dragSelectionCanvas;
                               };
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

        private void ButtonClickAddLink(object sender, RoutedEventArgs e)
        {
            drawCanvas.StartAddNewElement(1);
        }

        private void CheckBoxClickShowInBuffer(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                foreach (var item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferIn))
                {
                    item.Visibility = Visibility.Collapsed;
                    foreach (var connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                foreach (var item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferIn))
                {
                    item.Visibility = Visibility.Visible;
                    foreach (var connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CheckBoxClickShowOutBuffer(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                foreach (var item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferOut))
                {
                    item.Visibility = Visibility.Collapsed;
                    foreach (var connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                foreach (var item in DiagramItemManager.Instance.Items.Where(
                    x => x.DiagramItemType == DiagramItemType.BufferOut))
                {
                    item.Visibility = Visibility.Visible;
                    foreach (var connectionArrow in item.ConnectionArrows)
                    {
                        connectionArrow.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CheckBoxClickShowRouting(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                foreach (var item in DiagramItemManager.Instance.ConnectionArrows)
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

                foreach (var connectionArrow in items)
                {
                    connectionArrow.Visibility = Visibility.Visible;
                }
            }
        }

        private void CheckBoxClickShowLoopback(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                foreach (var item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.ConnectionArrowType == ConnectionArrowType.Loopback))
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                foreach (var item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.ConnectionArrowType == ConnectionArrowType.Loopback))
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }

        private void ComboBoxLoopbackViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.OutArrowType = ((ComboBox) sender).SelectedIndex;
            Properties.Settings.Default.Save();
            foreach (var item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.TargetItem.DiagramItemType == DiagramItemType.BufferOut))
            {
                item.ConnectionArrowType = Properties.Settings.Default.OutArrowType == 0
                                               ? ConnectionArrowType.Normal
                                               : ConnectionArrowType.OutArrowType;
            }
        }

        private void ComboBoxNormalArrowViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.NormalArrowType = ((ComboBox)sender).SelectedIndex;
            Properties.Settings.Default.Save();
            foreach (var item in DiagramItemManager.Instance.ConnectionArrows.Where(
                    x => x.ConnectionArrowType == ConnectionArrowType.Normal))
            {
                item.ViewType = Properties.Settings.Default.NormalArrowType;
            }
        }
    }
}