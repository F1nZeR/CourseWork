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
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DiagramItemManager.Instance.LoadDefaultElements();
        }
    }
}
