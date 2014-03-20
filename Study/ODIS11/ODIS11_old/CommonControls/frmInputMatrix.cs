using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AMM;

namespace ODIS.Controls
{
    public partial class frmInputMatrix : Form
    {
        public delegate void MatrixAutocalculateProcedure(Matrix M, int rowNumber = 0, int colNumber = 0); // используется для передачи клиентом процедуры пересчета вычисляемых элементов матрицы

        private Matrix M;
        private MatrixInputDisableOptions disableOptions = MatrixInputDisableOptions.None;
        private MatrixInputConstraintOptions constraintOptions = MatrixInputConstraintOptions.None;
        private MatrixAutocalculateProcedure AutocalculateProcedure = null;

        string Title = "";
        string[] RowTitles = null;
        string[] ColTitles = null;

        public frmInputMatrix(Matrix M, MatrixInputDisableOptions disableOptions = MatrixInputDisableOptions.None,
            MatrixInputConstraintOptions constraintOptions = MatrixInputConstraintOptions.None,
            MatrixAutocalculateProcedure RowAutocalculationProcedure = null,
            string Title = "", string[] RowTitles = null, string[] ColTitles = null)
        {
            InitializeComponent();
            this.disableOptions = disableOptions;
            this.constraintOptions = constraintOptions;
            this.AutocalculateProcedure = RowAutocalculationProcedure;
            this.Title = Title;
            this.RowTitles = RowTitles;
            if (RowTitles != null) RowsHeaderWidth = RowsHeaderWidthLarge;
            this.ColTitles = ColTitles;
            SetMatrix(M);
        }

        public static void InputMatrix(Matrix M, MatrixInputDisableOptions disableOptions = MatrixInputDisableOptions.None,
            MatrixInputConstraintOptions constraintOptions = MatrixInputConstraintOptions.None,
            MatrixAutocalculateProcedure RowAutocalculationProcedure = null,
            string Title = "", string[] RowTitles = null, string[] ColTitles = null)
        {
            frmInputMatrix frm = new frmInputMatrix(M, disableOptions, constraintOptions, RowAutocalculationProcedure, Title, RowTitles, ColTitles);
            if (frm.ShowDialog() == DialogResult.OK) frm.ReturnMatrix();
        }

        private void SetMatrix(Matrix M)
        {
            this.M = M;
            for (int i = M.Rows; i >= 1; i--)
                CreateMatrixRowPanel(M, i);
            InitHeader(M.Cols);
            MainPanel.Height = RowHeight * (M.Rows + 1);
            ActiveControl = MainPanel.Controls[M.Rows - 1].Controls[M.Cols - 1].Controls[0];
        }

        int RowsHeaderWidth = RowsHeaderWidthSmall;
        const int RowsHeaderWidthSmall = 26;
        const int RowsHeaderWidthLarge = 80;
        const int ColumnWidth = 64;
        const int RowHeight = 28;
        private void InitHeader(int cols)
        {
            Panel ColumnHeader = CreateRowPanel();
            for (int i = cols; i >= 1; i--)
            {
                Label lab = CreateLabel(0, i);
                Panel p = CreateCellPanel(i);
                p.Controls.Add(lab);
                ColumnHeader.Controls.Add(p);
            }
            Panel p1 = CreateCellPanel();
            Label lab1 = CreateLabel(0, 0);
            p1.Controls.Add(lab1);
            ColumnHeader.Controls.Add(p1);
            MainPanel.Width = RowsHeaderWidth + ColumnWidth * cols;
            ColumnHeader.Dock = DockStyle.Top; ColumnHeader.Height = RowHeight;
            MainPanel.Controls.Add(ColumnHeader);
        }

        private void CreateMatrixRowPanel(Matrix M, int rowNumber)
        {
            Panel RowPanel = CreateRowPanel(rowNumber); 
            for (int i = M.Cols; i >= 1; i--)
            {
                NumericUpDown edit = CreateEdit(rowNumber, i);
                Panel p = CreateCellPanel(i, edit);
                RowPanel.Controls.Add(p);
            }
            Panel p1 = CreateCellPanel();
            Label lab = CreateLabel(rowNumber, 0);
            p1.Controls.Add(lab);
            RowPanel.Controls.Add(p1);
            MainPanel.Controls.Add(RowPanel);
        }

        private Label CreateLabel(int rowNumber, int colNumber)
        {
            string text = "";
            if ((rowNumber == 0) && (colNumber == 0)) text = Title;
            else if (rowNumber == 0) // заголовок столбца
            {
                if (ColTitles == null) text = colNumber.ToString();
                else text = ColTitles[colNumber - 1];
            }
            else // заголовок строки
            {
                if (RowTitles == null) text = rowNumber.ToString();
                else text = RowTitles[rowNumber - 1];
            }
            Label lab = new Label();
            lab.Text = text;
            lab.TextAlign = ContentAlignment.MiddleCenter; 
            lab.AutoSize = false; 
            lab.Dock = DockStyle.Fill;
            return lab;
        }

        private Panel CreateCellPanel(int tabIndex = 0, NumericUpDown edit = null)
        {
            Panel result = new Panel(); 
            result.Dock = DockStyle.Left; 
            result.BorderStyle = BorderStyle.Fixed3D;
            result.Padding = new System.Windows.Forms.Padding(2); 
            result.TabIndex = tabIndex;
            if (edit != null) result.Controls.Add(edit);
            if (tabIndex == 0) result.Width = RowsHeaderWidth;
            else result.Width = ColumnWidth;
            return result;
        }

        private NumericUpDown CreateEdit(int row, int col)
        {
            MyNumericUpDown result = new MyNumericUpDown();
            result.TextAlign = HorizontalAlignment.Right;
            result.Dock = DockStyle.Fill;

            result.Enabled = ((row == col) && ((disableOptions & MatrixInputDisableOptions.DisableDiagonal) == 0)) ||
                             ((row != col) && ((disableOptions & MatrixInputDisableOptions.DisableNonDiagonal) == 0));
            
            result.Minimum = -result.Maximum;
            if (result.Enabled && ((constraintOptions & MatrixInputConstraintOptions.NonNegative) != 0)) result.Minimum = 0;
            if (result.Enabled && ((constraintOptions & MatrixInputConstraintOptions.Normalized) != 0))
            {
                result.Maximum = 1;
                result.Minimum = 0;
            }

            result.ChangeSignDecPlaces = 6;
            result.Value = (decimal)M[row, col];

            if (result.Enabled)
            {
                result.Tag = row; // используется для организации автоматического пересчета
                                  // пока нужна только строка
                result.ValueChanged += new EventHandler(ValueChanged);
            }

            return result;
        }

        void ValueChanged(object sender, EventArgs e)
        {
            int row = (int)((NumericUpDown)sender).Tag;
            int col = 0;
            if (AutocalculateProcedure != null)
            {
                Matrix A = new Matrix(M.Cols, M.Rows);
                ReturnMatrix(A);
                AutocalculateProcedure(A, row, col);
                RefreshValues(A, row, row);
            }
        }

        private void RefreshValues(Matrix source, int row, int col)
        {
            if (source == null) source = M;
            Panel rowPanel = (Panel)MainPanel.Controls[source.Rows - row];
            NumericUpDown edit = (NumericUpDown)(rowPanel.Controls[source.Cols - col].Controls[0]);
            edit.Value = (decimal)source[row, col];
        }

        private Panel CreateRowPanel(int rowNumber = 0)
        {
            Panel result = new Panel();
            result.Dock = DockStyle.Top;
            result.Height = RowHeight;
            result.TabIndex = rowNumber;
            return result;
        }

        public void ReturnMatrix(Matrix dest = null)
        {
            if (dest == null) dest = M;
            for (int i = 1; i <= dest.Rows; i++)
            {
                Control RowPanel = MainPanel.Controls[dest.Rows - i];
                for (int j = 1; j <= dest.Cols; j++)
                {
                    Control CellPanel = RowPanel.Controls[dest.Cols - j];
                    dest[i, j] = (double)(CellPanel.Controls[0] as NumericUpDown).Value;
                }
            }
        }
    }

    public enum MatrixInputDisableOptions: byte
    {
        None = 0,
        DisableDiagonal = 1,
        DisableNonDiagonal = 2
    }

    public enum MatrixInputConstraintOptions : byte
    {
        None = 0,
        NonNegative = 1,
        Normalized = 2 // числа в интервале [0,1]
    }

}
