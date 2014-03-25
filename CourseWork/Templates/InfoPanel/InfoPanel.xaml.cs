using System.Windows.Controls;
using SeMOEditor.Manager;

namespace SeMOEditor.Templates.InfoPanel
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
