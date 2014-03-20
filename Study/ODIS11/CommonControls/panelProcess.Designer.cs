namespace ODIS.Controls
{
    partial class panelProcess
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
            this.comboProcessType = new System.Windows.Forms.ComboBox();
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
            this.groupBox1.Size = new System.Drawing.Size(216, 272);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Случайный процесс";
            // 
            // ParamsPanel
            // 
            this.ParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParamsPanel.Location = new System.Drawing.Point(3, 43);
            this.ParamsPanel.Name = "ParamsPanel";
            this.ParamsPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ParamsPanel.Size = new System.Drawing.Size(210, 226);
            this.ParamsPanel.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboProcessType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 27);
            this.panel1.TabIndex = 4;
            // 
            // comboProcessType
            // 
            this.comboProcessType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProcessType.DropDownWidth = 280;
            this.comboProcessType.FormattingEnabled = true;
            this.comboProcessType.Location = new System.Drawing.Point(26, 0);
            this.comboProcessType.Name = "comboProcessType";
            this.comboProcessType.Size = new System.Drawing.Size(184, 21);
            this.comboProcessType.TabIndex = 1;
            this.comboProcessType.SelectedIndexChanged += new System.EventHandler(this.comboProcessType_SelectedIndexChanged);
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
            // panelProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "panelProcess";
            this.Size = new System.Drawing.Size(216, 272);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboProcessType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ParamsPanel;
    }
}
