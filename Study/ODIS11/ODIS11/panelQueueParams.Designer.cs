namespace ODIS.WinApp
{
    partial class panelQueueParams
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
            this.checkInfQueueLength = new System.Windows.Forms.CheckBox();
            this.editQueueLength = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.editQueueLength)).BeginInit();
            this.SuspendLayout();
            // 
            // checkInfQueueLength
            // 
            this.checkInfQueueLength.AutoSize = true;
            this.checkInfQueueLength.Checked = true;
            this.checkInfQueueLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkInfQueueLength.Location = new System.Drawing.Point(94, 26);
            this.checkInfQueueLength.Name = "checkInfQueueLength";
            this.checkInfQueueLength.Size = new System.Drawing.Size(99, 17);
            this.checkInfQueueLength.TabIndex = 8;
            this.checkInfQueueLength.Text = "не ограничена";
            this.checkInfQueueLength.UseVisualStyleBackColor = true;
            this.checkInfQueueLength.CheckedChanged += new System.EventHandler(this.checkInfQueueLength_CheckedChanged);
            // 
            // editQueueLength
            // 
            this.editQueueLength.Enabled = false;
            this.editQueueLength.Location = new System.Drawing.Point(6, 25);
            this.editQueueLength.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.editQueueLength.Name = "editQueueLength";
            this.editQueueLength.Size = new System.Drawing.Size(82, 20);
            this.editQueueLength.TabIndex = 7;
            this.editQueueLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Длина очереди";
            // 
            // panelQueueParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkInfQueueLength);
            this.Controls.Add(this.editQueueLength);
            this.Controls.Add(this.label3);
            this.Name = "panelQueueParams";
            this.Size = new System.Drawing.Size(213, 59);
            ((System.ComponentModel.ISupportInitialize)(this.editQueueLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkInfQueueLength;
        private System.Windows.Forms.NumericUpDown editQueueLength;
        private System.Windows.Forms.Label label3;
    }
}
