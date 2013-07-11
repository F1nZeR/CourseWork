﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWork.Manager;
using CourseWork.Properties;
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
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Settings.Default.Save();
            DiagramItemManager.Instance.SaveAll();
            Application.Current.Shutdown();
        }
    }
}
