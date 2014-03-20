namespace ODIS.WinApp
{
    partial class frmWait
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
            this.labInfo = new System.Windows.Forms.Label();
            this.btAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labInfo
            // 
            this.labInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labInfo.Location = new System.Drawing.Point(0, 0);
            this.labInfo.Name = "labInfo";
            this.labInfo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.labInfo.Size = new System.Drawing.Size(317, 61);
            this.labInfo.TabIndex = 0;
            this.labInfo.Text = "label1";
            this.labInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btAbort
            // 
            this.btAbort.Location = new System.Drawing.Point(121, 64);
            this.btAbort.Name = "btAbort";
            this.btAbort.Size = new System.Drawing.Size(75, 23);
            this.btAbort.TabIndex = 1;
            this.btAbort.Text = "Отмена";
            this.btAbort.UseVisualStyleBackColor = true;
            this.btAbort.Click += new System.EventHandler(this.btAbort_Click);
            // 
            // frmWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 92);
            this.ControlBox = false;
            this.Controls.Add(this.btAbort);
            this.Controls.Add(this.labInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWait";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подождите...";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.Button btAbort;
    }
}