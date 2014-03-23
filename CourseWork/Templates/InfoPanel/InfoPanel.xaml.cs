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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataGrid2DLibrary;

namespace CourseWork.Templates.InfoPanel
{
    /// <summary>
    /// Interaction logic for InfoPanel.xaml
    /// </summary>
    public partial class InfoPanel : UserControl
    {
        public InfoPanel()
        {
            InitializeComponent();
            Float2DArray = new float[5, 10];
            Dg2D.DataContext = this;
        }

        public float[,] Float2DArray { get; set; }

        public void SyncTable()
        {
        }
    }
}
