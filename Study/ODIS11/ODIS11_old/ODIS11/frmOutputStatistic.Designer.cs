using ODIS.Controls;
namespace ODIS.WinApp
{
    partial class frmOutputStatistic
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelStatistic = new ODIS.WinApp.PanelStatistic();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btRestore = new System.Windows.Forms.Button();
            this.btBuild = new System.Windows.Forms.Button();
            this.editLength = new ODIS.Controls.MyNumericUpDown();
            this.labText = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLength)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStatistic
            // 
            this.panelStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatistic.Location = new System.Drawing.Point(0, 0);
            this.panelStatistic.Name = "panelStatistic";
            this.panelStatistic.Size = new System.Drawing.Size(632, 374);
            this.panelStatistic.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.editCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btRestore);
            this.panel1.Controls.Add(this.btBuild);
            this.panel1.Controls.Add(this.editLength);
            this.panel1.Controls.Add(this.labText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 374);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 72);
            this.panel1.TabIndex = 1;
            // 
            // editCount
            // 
            this.editCount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editCount.Location = new System.Drawing.Point(130, 43);
            this.editCount.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.editCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editCount.Name = "editCount";
            this.editCount.Size = new System.Drawing.Size(98, 20);
            this.editCount.TabIndex = 11;
            this.editCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "Объем выборки";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btRestore
            // 
            this.btRestore.Location = new System.Drawing.Point(259, 40);
            this.btRestore.Name = "btRestore";
            this.btRestore.Size = new System.Drawing.Size(75, 23);
            this.btRestore.TabIndex = 9;
            this.btRestore.Text = "Вернуть";
            this.btRestore.UseVisualStyleBackColor = true;
            this.btRestore.Click += new System.EventHandler(this.btRestore_Click);
            // 
            // btBuild
            // 
            this.btBuild.Location = new System.Drawing.Point(259, 7);
            this.btBuild.Name = "btBuild";
            this.btBuild.Size = new System.Drawing.Size(75, 23);
            this.btBuild.TabIndex = 8;
            this.btBuild.Text = "Построить";
            this.btBuild.UseVisualStyleBackColor = true;
            this.btBuild.Click += new System.EventHandler(this.btBuild_Click);
            // 
            // editLength
            // 
            this.editLength.ChangeSignDecPlaces = 6;
            this.editLength.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editLength.Location = new System.Drawing.Point(130, 10);
            this.editLength.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.editLength.Name = "editLength";
            this.editLength.Size = new System.Drawing.Size(98, 20);
            this.editLength.TabIndex = 7;
            this.editLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(3, 9);
            this.labText.Name = "labText";
            this.labText.Padding = new System.Windows.Forms.Padding(3);
            this.labText.Size = new System.Drawing.Size(121, 19);
            this.labText.TabIndex = 6;
            this.labText.Text = "Длина интервала (T):";
            this.labText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmOutputStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.panelStatistic);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmOutputStatistic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выходящий поток";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PanelStatistic panelStatistic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown editCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRestore;
        private System.Windows.Forms.Button btBuild;
        private MyNumericUpDown editLength;
        private System.Windows.Forms.Label labText;
    }
}