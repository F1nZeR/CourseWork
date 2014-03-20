using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AMM;

namespace ODIS.Controls
{
    public partial class MatrixEditor : DataGridView
    {
        //??public NetTaskPanel Owner = null;
        private string[] RowHeaders = null;
        private string MatrixHeader = "";

        public EventHandler AfterAutoFill = null;

        public MatrixEditor()
        {
            InitializeComponent();
        }

        Matrix TheMatrix = null;

        public void SetMatrix(Matrix A, string[] RowHeaders = null, string MatrixHeader = "")
        {
            this.RowHeaders = RowHeaders;
            this.MatrixHeader = MatrixHeader;
            TheMatrix = A;
            Build();
        }

        public void GetMatrix()
        {
            TheMatrix.Resize(Rows.Count, Columns.Count);
            double x;
            for (int i = 1; i <= Rows.Count; i++)
                for (int j = 1; j <= Columns.Count; j++)
                {
                    /*string separatorMustBe = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    string separatorToChange = ".";
                    string s = this[j - 1, i - 1].Value.ToString();
                    if (separatorMustBe == separatorToChange) separatorToChange = ",";
                    s = s.Replace(separatorToChange, separatorMustBe);
                    double.TryParse(s, out x);*/
                    double.TryParse(this[j - 1, i - 1].Value.ToString(), out x);
                    TheMatrix[i, j] = x;
                }
        }

        private void Build()
        {
            SuspendLayout();
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Rows.Clear();
            Columns.Clear();
            for (int i = 1; i <= TheMatrix.Cols; i++)
            {
                Columns.Add("C" + i.ToString(), i.ToString());
                //Columns[i - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Columns[i - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Rows.Add(TheMatrix.Rows);
            for (int i = 1; i <= TheMatrix.Rows; i++)
            {
                if ((TheMatrix.Rows > 1) || (RowHeaders != null))
                {
                    if (RowHeaders == null) Rows[i - 1].HeaderCell.Value = i.ToString();
                    else Rows[i - 1].HeaderCell.Value = RowHeaders[i - 1];
                }
                for (int j = 1; j <= TheMatrix.Cols; j++)
                {
                    this[j - 1, i - 1].Value = TheMatrix[i, j];
                }
            }
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            ShowEditingIcon = false;
            if (MatrixHeader.Length == 0)
            {
                TopLeftHeaderCell.Value = "A";
                TopLeftHeaderCell.ToolTipText = "Заполнить автоматически";
            }
            else TopLeftHeaderCell.Value = MatrixHeader;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if ((TheMatrix.Rows > 1) || (RowHeaders != null)) RowHeadersWidth = 50;
            ResumeLayout();
            if (AfterAutoFill != null) AfterAutoFill(this, null);
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
            if ((e.ColumnIndex < 0) && (e.RowIndex < 0) && (MatrixHeader.Length == 0)) AutoFill();
        }

        private void AutoFill()
        {
            /*??if (Owner != null) Owner.AutoFill(this);*/
            Build();
        }

    }
}
