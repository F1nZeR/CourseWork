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
using System.Windows.Shapes;

namespace CourseWork.Windows
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
