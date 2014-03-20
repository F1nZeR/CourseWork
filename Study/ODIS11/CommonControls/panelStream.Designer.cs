namespace ODIS.Controls
{
    partial class panelStream
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ParamsPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboStreamType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ParamsPanel);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(209, 301);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поток событий";
            // 
            // ParamsPanel
            // 
            this.ParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParamsPanel.Location = new System.Drawing.Point(5, 45);
            this.ParamsPanel.Name = "ParamsPanel";
            this.ParamsPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ParamsPanel.Size = new System.Drawing.Size(199, 251);
            this.ParamsPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboStreamType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 27);
            this.panel1.TabIndex = 3;
            // 
            // comboStreamType
            // 
            this.comboStreamType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboStreamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStreamType.FormattingEnabled = true;
            this.comboStreamType.Location = new System.Drawing.Point(26, 0);
            this.comboStreamType.Name = "comboStreamType";
            this.comboStreamType.Size = new System.Drawing.Size(173, 21);
            this.comboStreamType.TabIndex = 1;
            this.comboStreamType.SelectedIndexChanged += new System.EventHandler(this.comboStreamType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вид";
            // 
            // panelStream
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "panelStream";
            this.Size = new System.Drawing.Size(209, 301);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboStreamType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ParamsPanel;
        private System.Windows.Forms.Panel panel1;

    }
}
