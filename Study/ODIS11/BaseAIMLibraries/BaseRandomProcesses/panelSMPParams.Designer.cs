namespace BaseRandomProcesses
{
    partial class panelSMPParams
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
            this.btnInputGMatrix = new System.Windows.Forms.Button();
            this.editK = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInputPMatrix = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInputRMatrix = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.editK)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInputGMatrix
            // 
            this.btnInputGMatrix.Location = new System.Drawing.Point(161, 56);
            this.btnInputGMatrix.Name = "btnInputGMatrix";
            this.btnInputGMatrix.Size = new System.Drawing.Size(38, 23);
            this.btnInputGMatrix.TabIndex = 12;
            this.btnInputGMatrix.Text = "G(x)";
            this.btnInputGMatrix.UseVisualStyleBackColor = true;
            // 
            // editK
            // 
            this.editK.Location = new System.Drawing.Point(133, 10);
            this.editK.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.editK.Name = "editK";
            this.editK.Size = new System.Drawing.Size(66, 20);
            this.editK.TabIndex = 11;
            this.editK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editK.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Число состояний:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "Матрица распределений\r\nвременной составляющей";
            // 
            // btnInputPMatrix
            // 
            this.btnInputPMatrix.Location = new System.Drawing.Point(161, 88);
            this.btnInputPMatrix.Name = "btnInputPMatrix";
            this.btnInputPMatrix.Size = new System.Drawing.Size(38, 23);
            this.btnInputPMatrix.TabIndex = 14;
            this.btnInputPMatrix.Text = "P";
            this.btnInputPMatrix.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 32);
            this.label3.TabIndex = 15;
            this.label3.Text = "Матрица вероятностей\r\nпереходов";
            // 
            // btnInputRMatrix
            // 
            this.btnInputRMatrix.Location = new System.Drawing.Point(161, 120);
            this.btnInputRMatrix.Name = "btnInputRMatrix";
            this.btnInputRMatrix.Size = new System.Drawing.Size(38, 23);
            this.btnInputRMatrix.TabIndex = 16;
            this.btnInputRMatrix.Text = "R";
            this.btnInputRMatrix.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 32);
            this.label4.TabIndex = 17;
            this.label4.Text = "Финальные вероятности\r\n(необязательно)";
            // 
            // panelSMPParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInputRMatrix);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnInputPMatrix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInputGMatrix);
            this.Controls.Add(this.editK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "panelSMPParams";
            this.Size = new System.Drawing.Size(210, 190);
            ((System.ComponentModel.ISupportInitialize)(this.editK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputGMatrix;
        internal System.Windows.Forms.NumericUpDown editK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInputPMatrix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInputRMatrix;
        private System.Windows.Forms.Label label4;
    }
}
