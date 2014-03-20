using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;

namespace ODIS.WinApp
{
    public partial class frmPreferences : Form
    {
        public frmPreferences()
        {
            InitializeComponent();
        }

        public static void Execute()
        {
            frmPreferences frm = new frmPreferences();
            frm.editDigitsCount.Value = AIMCore.DigitCounts;
            frm.comboBaseGeneratorType.Items.AddRange(AIMCore.BaseGeneratorFactories.ToArray());
            frm.comboBaseGeneratorType.SelectedItem = AIMCore.CurrentBaseGeneratorFactory;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AIMCore.DigitCounts = (int)(frm.editDigitsCount.Value);
                AIMCore.CurrentBaseGeneratorFactory = (frm.comboBaseGeneratorType.SelectedItem as IBaseGeneratorFactory);
                AIMCore.Save();
            }
        }

    }
}
