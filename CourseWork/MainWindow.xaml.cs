using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWork.Manager;
using CourseWork.Templates;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Closed += (sender, args) => Application.Current.Shutdown();
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            drawControl.ReDrawElements();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DiagramItemManager.Instance.LoadDefaultElements();
        }
    }
}
