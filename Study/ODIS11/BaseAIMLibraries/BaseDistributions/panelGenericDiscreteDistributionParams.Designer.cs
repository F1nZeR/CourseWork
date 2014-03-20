namespace ODIS.Controls
{
    partial class panelGenericDiscreteDistributionParams
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
            this.btnInputMatrix = new System.Windows.Forms.Button();
            this.editN = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btLoad = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.editN)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInputMatrix
            // 
            this.btnInputMatrix.Location = new System.Drawing.Point(116, 35);
            this.btnInputMatrix.Name = "btnInputMatrix";
            this.btnInputMatrix.Size = new System.Drawing.Size(71, 23);
            this.btnInputMatrix.TabIndex = 12;
            this.btnInputMatrix.Text = "Задать";
            this.btnInputMatrix.UseVisualStyleBackColor = true;
            this.btnInputMatrix.Click += new System.EventHandler(this.btnInputMatrix_Click);
            // 
            // editN
            // 
            this.editN.Location = new System.Drawing.Point(128, 9);
            this.editN.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.editN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editN.Name = "editN";
            this.editN.Size = new System.Drawing.Size(59, 20);
            this.editN.TabIndex = 11;
            this.editN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editN.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.editN.ValueChanged += new System.EventHandler(this.editN_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Количество значений:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ряд распределения";
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(116, 64);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(71, 23);
            this.btLoad.TabIndex = 14;
            this.btLoad.Text = "Из файла";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // panelGenericDiscreteDistributionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btnInputMatrix);
            this.Controls.Add(this.editN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "panelGenericDiscreteDistributionParams";
            this.Size = new System.Drawing.Size(203, 222);
            ((System.ComponentModel.ISupportInitialize)(this.editN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputMatrix;
        private System.Windows.Forms.NumericUpDown editN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
