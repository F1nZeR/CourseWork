using System;
using System.Windows;
using System.Windows.Controls;
using CourseWork.Maps.ImageProvider;
using GMap.NET.MapProviders;
using SeMOEditor.Manager;
using SeMOEditor.Properties;

namespace SeMOEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += OnClosed;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MapTypeMenuItem.ItemsSource = GMapProviders.List;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Settings.Default.Save();
            DiagramItemManager.Instance.SaveAll();
            Application.Current.Shutdown();
        }

        private void MenuItemOpenClick(object sender, RoutedEventArgs e)
        {
            DiagramItemManager.Instance.LoadDefaultElements();
        }

        private void MenuItemNewClick(object sender, RoutedEventArgs e)
        {
            DiagramItemManager.Instance.ResetManager();
        }

        private void MenuItemBackOpenImage(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpeg",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            
            var result = dlg.ShowDialog();
            if (result != true) return;
            var filename = dlg.FileName;
            DrawControl.MainMap.MapProvider = new GMapImageProvider(filename);
            DrawControl.MainMap.Zoom = 0;
        }

        private void MapTypeDataBoundItemClick(object sender, RoutedEventArgs e)
        {
            var obMenuItem = (MenuItem) e.OriginalSource;
            DrawControl.MainMap.MapProvider = (GMapProvider) obMenuItem.Header;
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabTable.IsSelected)
                {
                    InfoPanelBlock.SyncTable();
                }
                else if (IsLoaded)
                {
                    var matrix = InfoPanelBlock.Float2DArray;
                    DiagramItemManager.Instance.SyncVisualState(matrix);
                }
            }
        }
    }
}
