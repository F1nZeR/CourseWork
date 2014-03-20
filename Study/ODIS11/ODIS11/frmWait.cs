using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ODIS.WinApp
{
    public partial class frmWait : Form
    {
        public frmWait()
        {
            InitializeComponent();
        }

        private static frmWait frm = new frmWait();
        internal static void ShowInfo(string text)
        {
            frm.aborted = false;
            frm.labInfo.Text = text;
            frm.Show();
            Application.DoEvents();
        }

        internal static void HideInfo()
        {
            frm.Hide();
        }

        private bool aborted = false;

        public static bool Aborted
        {
            get 
            {
                Application.DoEvents();
                return frm.aborted; 
            }
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            aborted = true;
            Application.DoEvents();
        }
    }
}
