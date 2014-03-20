namespace ODIS.Controls
{
    partial class panelUniformDistributionParams
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
            this.label1 = new System.Windows.Forms.Label();
            this.editMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editMax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.editMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMax)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Интервал:";
            // 
            // editMin
            // 
            this.editMin.DecimalPlaces = 3;
            this.editMin.Location = new System.Drawing.Point(30, 25);
            this.editMin.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.editMin.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.editMin.Name = "editMin";
            this.editMin.Size = new System.Drawing.Size(118, 20);
            this.editMin.TabIndex = 1;
            this.editMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editMin.ValueChanged += new System.EventHandler(this.editMin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "до";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "от";
            // 
            // editMax
            // 
            this.editMax.DecimalPlaces = 3;
            this.editMax.Location = new System.Drawing.Point(30, 51);
            this.editMax.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.editMax.Minimum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            -2147483648});
            this.editMax.Name = "editMax";
            this.editMax.Size = new System.Drawing.Size(118, 20);
            this.editMax.TabIndex = 4;
            this.editMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editMax.ValueChanged += new System.EventHandler(this.editMax_ValueChanged);
            // 
            // panelUniformDistributionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editMax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editMin);
            this.Controls.Add(this.label1);
            this.Name = "panelUniformDistributionParams";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(201, 80);
            ((System.ComponentModel.ISupportInitialize)(this.editMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown editMin;
        private System.Windows.Forms.NumericUpDown editMax;
    }
}
