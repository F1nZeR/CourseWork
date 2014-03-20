namespace ODIS.Controls
{
    partial class panelOneParameter
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
            this.labText = new System.Windows.Forms.Label();
            this.editParameter = new MyNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.editParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(3, 9);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(61, 13);
            this.labText.TabIndex = 0;
            this.labText.Text = "Параметр:";
            // 
            // editParameter
            // 
            this.editParameter.ChangeSignDecPlaces = 6;
            this.editParameter.DecimalPlaces = 1;
            this.editParameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editParameter.Location = new System.Drawing.Point(70, 7);
            this.editParameter.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editParameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.editParameter.Name = "editParameter";
            this.editParameter.Size = new System.Drawing.Size(85, 20);
            this.editParameter.TabIndex = 1;
            this.editParameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editParameter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panelOneParameterDistributionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editParameter);
            this.Controls.Add(this.labText);
            this.Name = "panelOneParameterDistributionParams";
            this.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Size = new System.Drawing.Size(174, 32);
            ((System.ComponentModel.ISupportInitialize)(this.editParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labText;
        private MyNumericUpDown editParameter;
    }
}
