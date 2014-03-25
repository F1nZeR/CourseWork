using System.Windows;

namespace SeMOEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewGroupWindow.xaml
    /// </summary>
    public partial class NewGroupWindow : Window
    {
        public string Value { get; set; }

        public NewGroupWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbGroupName.Text.Trim())) return;
            Value = tbGroupName.Text;
            Close();
        }


    }
}
