namespace ODIS.Controls
{
    partial class panelTwoParameters
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
            this.labParam2Text = new System.Windows.Forms.Label();
            this.labParam1Text = new System.Windows.Forms.Label();
            this.editParam2 = new MyNumericUpDown();
            this.editParam1 = new MyNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.editParam2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editParam1)).BeginInit();
            this.SuspendLayout();
            // 
            // labParam2Text
            // 
            this.labParam2Text.AutoSize = true;
            this.labParam2Text.Location = new System.Drawing.Point(3, 39);
            this.labParam2Text.Name = "labParam2Text";
            this.labParam2Text.Size = new System.Drawing.Size(67, 13);
            this.labParam2Text.TabIndex = 8;
            this.labParam2Text.Text = "Параметр 2";
            // 
            // labParam1Text
            // 
            this.labParam1Text.AutoSize = true;
            this.labParam1Text.Location = new System.Drawing.Point(3, 9);
            this.labParam1Text.Name = "labParam1Text";
            this.labParam1Text.Size = new System.Drawing.Size(67, 13);
            this.labParam1Text.TabIndex = 6;
            this.labParam1Text.Text = "Параметр 1";
            // 
            // editParam2
            // 
            this.editParam2.ChangeSignDecPlaces = 6;
            this.editParam2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editParam2.Location = new System.Drawing.Point(101, 37);
            this.editParam2.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editParam2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.editParam2.Name = "editParam2";
            this.editParam2.Size = new System.Drawing.Size(83, 20);
            this.editParam2.TabIndex = 9;
            this.editParam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editParam2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // editParam1
            // 
            this.editParam1.ChangeSignDecPlaces = 6;
            this.editParam1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editParam1.Location = new System.Drawing.Point(101, 7);
            this.editParam1.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editParam1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.editParam1.Name = "editParam1";
            this.editParam1.Size = new System.Drawing.Size(83, 20);
            this.editParam1.TabIndex = 7;
            this.editParam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editParam1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panelTwoParametersDistributionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editParam2);
            this.Controls.Add(this.labParam2Text);
            this.Controls.Add(this.editParam1);
            this.Controls.Add(this.labParam1Text);
            this.Name = "panelTwoParametersDistributionParams";
            this.Size = new System.Drawing.Size(197, 69);
            ((System.ComponentModel.ISupportInitialize)(this.editParam2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editParam1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyNumericUpDown editParam2;
        private System.Windows.Forms.Label labParam2Text;
        private MyNumericUpDown editParam1;
        private System.Windows.Forms.Label labParam1Text;
    }
}
