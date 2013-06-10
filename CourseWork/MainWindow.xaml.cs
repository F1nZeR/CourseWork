﻿using System;
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
            Closed += OnClosed;
            StateChanged += OnStateChanged;
            SizeChanged += OnSizeChanged;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            DiagramItemManager.Instance.SaveAll();
            Application.Current.Shutdown();
        }

        private void OnStateChanged(object sender, EventArgs eventArgs)
        {
            // не сразу обновляет
            drawControl.ReDrawElements();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            drawControl.ReDrawElements();
        }
    }
}
