using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using SeMOEditor.Templates;

namespace SeMOEditor.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeLinkWindow.xaml
    /// </summary>
    public partial class ChangeLinkWindow : Window
    {
        public double Chance { get; set; }

        public ChangeLinkWindow(DiagramItem fromItem, DiagramItem targetItem, double chance = 0)
        {
            Chance = chance;
            InitializeComponent();
            tbChance.PreviewTextInput += TbChanceOnPreviewTextInput;
            Title = string.Format("{0} -> {1} ({2}%)", fromItem.LabelName, targetItem.LabelName,
                Chance*100);
            KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Enter) OkHandle();
            };
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
            OkHandle();
        }

        private void OkHandle()
        {
            if (IsTextAllowed(tbChance.Text) && !string.IsNullOrWhiteSpace(tbChance.Text))
            {
                var chance = double.Parse(tbChance.Text.Trim()) / 100;
                if (chance > 0 && chance <= 1)
                {
                    Chance = chance;
                    Close();
                }
            }
        }
    }
}
