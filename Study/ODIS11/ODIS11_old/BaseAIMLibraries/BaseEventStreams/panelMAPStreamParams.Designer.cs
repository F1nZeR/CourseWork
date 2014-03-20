namespace ODIS.Controls
{
    partial class panelMAPStreamParams
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
            this.btnInputLambdaMatrix = new System.Windows.Forms.Button();
            this.btnInputDMatrix = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelProcess = new ODIS.Controls.panelProcess();
            this.SuspendLayout();
            // 
            // btnInputLambdaMatrix
            // 
            this.btnInputLambdaMatrix.Font = new System.Drawing.Font("Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnInputLambdaMatrix.Location = new System.Drawing.Point(140, 155);
            this.btnInputLambdaMatrix.Name = "btnInputLambdaMatrix";
            this.btnInputLambdaMatrix.Size = new System.Drawing.Size(30, 23);
            this.btnInputLambdaMatrix.TabIndex = 3;
            this.btnInputLambdaMatrix.Text = "L";
            this.btnInputLambdaMatrix.UseVisualStyleBackColor = true;
            this.btnInputLambdaMatrix.Click += new System.EventHandler(this.btnInputLambdaMatrix_Click);
            // 
            // btnInputDMatrix
            // 
            this.btnInputDMatrix.Location = new System.Drawing.Point(140, 201);
            this.btnInputDMatrix.Name = "btnInputDMatrix";
            this.btnInputDMatrix.Size = new System.Drawing.Size(30, 23);
            this.btnInputDMatrix.TabIndex = 4;
            this.btnInputDMatrix.Text = "D";
            this.btnInputDMatrix.UseVisualStyleBackColor = true;
            this.btnInputDMatrix.Click += new System.EventHandler(this.btnInputDMatrix_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Интенсивности потока";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 41);
            this.label4.TabIndex = 7;
            this.label4.Text = "Вероятности событий в моменты смены состояния";
            // 
            // panelProcess
            // 
            this.panelProcess.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProcess.Location = new System.Drawing.Point(0, 0);
            this.panelProcess.Name = "panelProcess";
            this.panelProcess.Size = new System.Drawing.Size(215, 149);
            this.panelProcess.TabIndex = 8;
            // 
            // panelMAPStreamParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelProcess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnInputDMatrix);
            this.Controls.Add(this.btnInputLambdaMatrix);
            this.Controls.Add(this.label3);
            this.Name = "panelMAPStreamParams";
            this.Size = new System.Drawing.Size(215, 237);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInputLambdaMatrix;
        private System.Windows.Forms.Button btnInputDMatrix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private panelProcess panelProcess;
    }
}
