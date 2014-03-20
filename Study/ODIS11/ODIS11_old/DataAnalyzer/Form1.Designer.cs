namespace ODIS.DataAnalyzer
{
    partial class frmMain
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
            this.panelParams = new System.Windows.Forms.Panel();
            this.btStart = new System.Windows.Forms.Button();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.labInfo = new System.Windows.Forms.Label();
            this.btAbort = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panelParams.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelParams
            // 
            this.panelParams.Controls.Add(this.btStart);
            this.panelParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelParams.Location = new System.Drawing.Point(0, 0);
            this.panelParams.Name = "panelParams";
            this.panelParams.Size = new System.Drawing.Size(507, 75);
            this.panelParams.TabIndex = 0;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(19, 13);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.labInfo);
            this.panelProgress.Controls.Add(this.btAbort);
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgress.Location = new System.Drawing.Point(0, 75);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(507, 98);
            this.panelProgress.TabIndex = 1;
            this.panelProgress.Visible = false;
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.Location = new System.Drawing.Point(9, 12);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(75, 13);
            this.labInfo.TabIndex = 2;
            this.labInfo.Text = "Выполняется";
            // 
            // btAbort
            // 
            this.btAbort.Location = new System.Drawing.Point(199, 66);
            this.btAbort.Name = "btAbort";
            this.btAbort.Size = new System.Drawing.Size(75, 23);
            this.btAbort.TabIndex = 1;
            this.btAbort.Text = "Abort";
            this.btAbort.UseVisualStyleBackColor = true;
            this.btAbort.Click += new System.EventHandler(this.btAbort_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 37);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(483, 23);
            this.progressBar.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 233);
            this.panel1.TabIndex = 2;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(507, 233);
            this.textBox.TabIndex = 0;
            this.textBox.WordWrap = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 406);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelParams);
            this.MinimumSize = new System.Drawing.Size(500, 170);
            this.Name = "frmMain";
            this.Text = "Data Analyzer";
            this.panelParams.ResumeLayout(false);
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelParams;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Button btAbort;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labInfo;
    }
}

