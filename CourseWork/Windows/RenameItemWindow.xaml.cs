using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SeMOEditor.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeLinkWindow.xaml
    /// </summary>
    public partial class RenameItemWindow : Window
    {
        public string Result { get; set; }

        public RenameItemWindow(string curName)
        {
            InitializeComponent();
            TbName.Text = curName;
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            Result = TbName.Text.Trim();
            Close();
        }
    }
}
