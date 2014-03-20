using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.Controls;
using ODIS.AIM.Queue;

namespace ODIS.WinApp
{
    public partial class panelSource : panelStream
    {
        public panelSource()
        {
            InitializeComponent();
        }

        public bool ConfigureSource(Source source)
        {
            source.InputStream = GetStream();
            return source.InputStream != null;
        }
    }
}
