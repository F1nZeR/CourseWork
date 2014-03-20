namespace ODIS.Controls
{
    partial class panelMDCCTParams
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
            this.btnInputQMatrix = new System.Windows.Forms.Button();
            this.editK = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.editK)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInputQMatrix
            // 
            this.btnInputQMatrix.Location = new System.Drawing.Point(169, 58);
            this.btnInputQMatrix.Name = "btnInputQMatrix";
            this.btnInputQMatrix.Size = new System.Drawing.Size(30, 23);
            this.btnInputQMatrix.TabIndex = 8;
            this.btnInputQMatrix.Text = "Q";
            this.btnInputQMatrix.UseVisualStyleBackColor = true;
            this.btnInputQMatrix.Click += new System.EventHandler(this.btnInputQMatrix_Click);
            // 
            // editK
            // 
            this.editK.Location = new System.Drawing.Point(140, 12);
            this.editK.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.editK.Name = "editK";
            this.editK.Size = new System.Drawing.Size(59, 20);
            this.editK.TabIndex = 7;
            this.editK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editK.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.editK.ValueChanged += new System.EventHandler(this.editK_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Число состояний цепи:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "Матрица инфинитезимальных коэффициентов";
            // 
            // panelMDCCTParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInputQMatrix);
            this.Controls.Add(this.editK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "panelMDCCTParams";
            this.Size = new System.Drawing.Size(213, 215);
            ((System.ComponentModel.ISupportInitialize)(this.editK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputQMatrix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown editK;
    }
}
