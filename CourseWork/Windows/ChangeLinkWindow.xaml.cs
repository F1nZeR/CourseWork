﻿using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWork.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeLinkWindow.xaml
    /// </summary>
    public partial class ChangeLinkWindow : Window
    {
        public double Chance { get; set; }

        public ChangeLinkWindow()
        {
            InitializeComponent();
            tbChance.PreviewTextInput += TbChanceOnPreviewTextInput;
        }

        private void TbChanceOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text.Trim());
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            if (IsTextAllowed(tbChance.Text) && !string.IsNullOrWhiteSpace(tbChance.Text))
            {
                var chance = double.Parse(tbChance.Text.Trim())/100;
                if (chance > 0 && chance <= 1)
                {
                    Chance = chance;
                    this.Close();
                }
            }
        }
    }
}
