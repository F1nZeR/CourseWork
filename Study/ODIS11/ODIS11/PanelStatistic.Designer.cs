using ODIS.Controls;
namespace ODIS.WinApp
{
    partial class PanelStatistic
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btShowDistribution = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btCalcCrit = new System.Windows.Forms.Button();
            this.labCrit = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labStat = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSaveStateProbs = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveStateImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelDistribution = new ODIS.Controls.panelDistribution();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // btShowDistribution
            // 
            this.btShowDistribution.Location = new System.Drawing.Point(2, 3);
            this.btShowDistribution.Name = "btShowDistribution";
            this.btShowDistribution.Size = new System.Drawing.Size(75, 23);
            this.btShowDistribution.TabIndex = 0;
            this.btShowDistribution.Text = "Показать";
            this.btShowDistribution.UseVisualStyleBackColor = true;
            this.btShowDistribution.Click += new System.EventHandler(this.btShowDistribution_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btCalcCrit);
            this.panel2.Controls.Add(this.labCrit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 296);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 161);
            this.panel2.TabIndex = 2;
            // 
            // btCalcCrit
            // 
            this.btCalcCrit.Location = new System.Drawing.Point(5, 3);
            this.btCalcCrit.Name = "btCalcCrit";
            this.btCalcCrit.Size = new System.Drawing.Size(75, 23);
            this.btCalcCrit.TabIndex = 3;
            this.btCalcCrit.Text = "Критерии";
            this.btCalcCrit.UseVisualStyleBackColor = true;
            this.btCalcCrit.Click += new System.EventHandler(this.btCalcCrit_Click);
            // 
            // labCrit
            // 
            this.labCrit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labCrit.Location = new System.Drawing.Point(0, 0);
            this.labCrit.Name = "labCrit";
            this.labCrit.Padding = new System.Windows.Forms.Padding(3);
            this.labCrit.Size = new System.Drawing.Size(218, 161);
            this.labCrit.TabIndex = 2;
            this.labCrit.Text = "Критерии";
            this.labCrit.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 32);
            this.panel1.TabIndex = 6;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(5, 5);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 3;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btShowDistribution);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 193);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(212, 31);
            this.panel3.TabIndex = 7;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ContextMenuStrip = this.contextMenu;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(224, 0);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            series1.XValueMember = "Min";
            series1.YValueMembers = "Count";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(416, 460);
            this.chart.TabIndex = 3;
            this.chart.Text = "chart1";
            // 
            // labStat
            // 
            this.labStat.AutoSize = true;
            this.labStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labStat.Dock = System.Windows.Forms.DockStyle.Top;
            this.labStat.Location = new System.Drawing.Point(3, 16);
            this.labStat.Name = "labStat";
            this.labStat.Padding = new System.Windows.Forms.Padding(3);
            this.labStat.Size = new System.Drawing.Size(73, 21);
            this.labStat.TabIndex = 1;
            this.labStat.Text = "Статистика";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelDistribution);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 227);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сравнить с распределением:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.labStat);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 460);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выборка";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowLabels,
            this.toolStripMenuItem1,
            this.btnSaveStateProbs,
            this.btnSaveStateImage});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(203, 76);
            // 
            // btnSaveStateProbs
            // 
            this.btnSaveStateProbs.Name = "btnSaveStateProbs";
            this.btnSaveStateProbs.Size = new System.Drawing.Size(202, 22);
            this.btnSaveStateProbs.Text = "Сохранить значения...";
            this.btnSaveStateProbs.Click += new System.EventHandler(this.btnSaveStateProbs_Click);
            // 
            // btnSaveStateImage
            // 
            this.btnSaveStateImage.Name = "btnSaveStateImage";
            this.btnSaveStateImage.Size = new System.Drawing.Size(202, 22);
            this.btnSaveStateImage.Text = "Сохранить рисунок...";
            this.btnSaveStateImage.Click += new System.EventHandler(this.btnSaveStateImage_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // btnShowLabels
            // 
            this.btnShowLabels.CheckOnClick = true;
            this.btnShowLabels.Name = "btnShowLabels";
            this.btnShowLabels.Size = new System.Drawing.Size(202, 22);
            this.btnShowLabels.Text = "Показать значения";
            this.btnShowLabels.Click += new System.EventHandler(this.btnShowLabels_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "jpg";
            this.saveImageDialog.Filter = "Рисунки (*.bmp;*.jpg;*.gif;*.emf) |*.bmp;*.jpg;*.gif;*.emf";
            // 
            // panelDistribution
            // 
            this.panelDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDistribution.Location = new System.Drawing.Point(3, 16);
            this.panelDistribution.Name = "panelDistribution";
            this.panelDistribution.Size = new System.Drawing.Size(212, 177);
            this.panelDistribution.TabIndex = 4;
            // 
            // PanelStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart);
            this.Controls.Add(this.groupBox1);
            this.Name = "PanelStatistic";
            this.Size = new System.Drawing.Size(640, 460);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btShowDistribution;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btCalcCrit;
        private System.Windows.Forms.Label labCrit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Label labStat;
        private panelDistribution panelDistribution;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnSaveStateProbs;
        private System.Windows.Forms.ToolStripMenuItem btnSaveStateImage;
        private System.Windows.Forms.ToolStripMenuItem btnShowLabels;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
    }
}
