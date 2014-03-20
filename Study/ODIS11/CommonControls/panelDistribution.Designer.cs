namespace ODIS.Controls
{
    partial class panelDistribution
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
            this.ParamsPanel = new System.Windows.Forms.Panel();
            this.comboDistributionType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ParamsPanel
            // 
            this.ParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParamsPanel.Location = new System.Drawing.Point(0, 21);
            this.ParamsPanel.Name = "ParamsPanel";
            this.ParamsPanel.Padding = new System.Windows.Forms.Padding(3);
            this.ParamsPanel.Size = new System.Drawing.Size(184, 189);
            this.ParamsPanel.TabIndex = 4;
            // 
            // comboDistributionType
            // 
            this.comboDistributionType.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboDistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDistributionType.FormattingEnabled = true;
            this.comboDistributionType.Location = new System.Drawing.Point(0, 0);
            this.comboDistributionType.Name = "comboDistributionType";
            this.comboDistributionType.Size = new System.Drawing.Size(184, 21);
            this.comboDistributionType.Sorted = true;
            this.comboDistributionType.TabIndex = 3;
            this.comboDistributionType.SelectedIndexChanged += new System.EventHandler(this.comboDistributionType_SelectedIndexChanged);
            // 
            // panelDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParamsPanel);
            this.Controls.Add(this.comboDistributionType);
            this.Name = "panelDistribution";
            this.Size = new System.Drawing.Size(184, 210);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ParamsPanel;
        private System.Windows.Forms.ComboBox comboDistributionType;
    }
}
