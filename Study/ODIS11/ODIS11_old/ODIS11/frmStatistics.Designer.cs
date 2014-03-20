namespace ODIS.WinApp
{
    partial class frmStatistics
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
            this.panelStatistic = new ODIS.WinApp.PanelStatistic();
            this.SuspendLayout();
            // 
            // panelStatistic
            // 
            this.panelStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatistic.Location = new System.Drawing.Point(0, 0);
            this.panelStatistic.Name = "panelStatistic";
            this.panelStatistic.Size = new System.Drawing.Size(632, 426);
            this.panelStatistic.TabIndex = 0;
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 426);
            this.Controls.Add(this.panelStatistic);
            this.MinimumSize = new System.Drawing.Size(640, 460);
            this.Name = "frmStatistics";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Статистика";
            this.ResumeLayout(false);

        }

        #endregion

        private PanelStatistic panelStatistic;

    }
}