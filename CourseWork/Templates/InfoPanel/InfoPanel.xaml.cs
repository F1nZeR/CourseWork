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
using CourseWork.Manager;
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
            Dg2D.DataContext = this;
            Float2DArray = new double[1, 1];
        }

        public double[,] Float2DArray { get; set; }

        public void SyncTable()
        {
            Dg2D.ItemsSource2D = null;
            var chanceArray = DiagramItemManager.Instance.GetDeviceChanceMatrix();
            Float2DArray = chanceArray.ConvertToDoubleArray();
            Dg2D.ItemsSource2D = Float2DArray;
        }
    }
}
