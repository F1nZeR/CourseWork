using ODIS.Controls;
namespace ODIS.WinApp
{
    partial class PanelServerBlock
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelBuffer = new System.Windows.Forms.Panel();
            this.comboBufferType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelDistribution = new ODIS.Controls.panelDistribution();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkInfCount = new System.Windows.Forms.CheckBox();
            this.editCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboConflictType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCount)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 438);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Блок обслуживания";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelBuffer);
            this.groupBox3.Controls.Add(this.comboBufferType);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 281);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 154);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Буфер";
            // 
            // panelBuffer
            // 
            this.panelBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBuffer.Location = new System.Drawing.Point(0, 46);
            this.panelBuffer.Name = "panelBuffer";
            this.panelBuffer.Size = new System.Drawing.Size(246, 105);
            this.panelBuffer.TabIndex = 2;
            // 
            // comboBufferType
            // 
            this.comboBufferType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBufferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBufferType.FormattingEnabled = true;
            this.comboBufferType.Items.AddRange(new object[] {
            "Нет",
            "Очередь",
            "ИПВ"});
            this.comboBufferType.Location = new System.Drawing.Point(38, 19);
            this.comboBufferType.Name = "comboBufferType";
            this.comboBufferType.Size = new System.Drawing.Size(205, 21);
            this.comboBufferType.TabIndex = 1;
            this.comboBufferType.SelectedIndexChanged += new System.EventHandler(this.comboBufferType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Тип";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 173);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelDistribution);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(243, 154);
            this.panel2.TabIndex = 1;
            // 
            // panelDistribution
            // 
            this.panelDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDistribution.Location = new System.Drawing.Point(3, 19);
            this.panelDistribution.Name = "panelDistribution";
            this.panelDistribution.Size = new System.Drawing.Size(237, 132);
            this.panelDistribution.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Закон обслуживания";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkInfCount);
            this.panel1.Controls.Add(this.editCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 45);
            this.panel1.TabIndex = 0;
            // 
            // checkInfCount
            // 
            this.checkInfCount.AutoSize = true;
            this.checkInfCount.Location = new System.Drawing.Point(109, 20);
            this.checkInfCount.Name = "checkInfCount";
            this.checkInfCount.Size = new System.Drawing.Size(91, 17);
            this.checkInfCount.TabIndex = 2;
            this.checkInfCount.Text = "бесконечное";
            this.checkInfCount.UseVisualStyleBackColor = true;
            this.checkInfCount.CheckedChanged += new System.EventHandler(this.checkInfCount_CheckedChanged);
            // 
            // editCount
            // 
            this.editCount.Location = new System.Drawing.Point(18, 19);
            this.editCount.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editCount.Name = "editCount";
            this.editCount.Size = new System.Drawing.Size(78, 20);
            this.editCount.TabIndex = 1;
            this.editCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Число приборов:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboConflictType);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 47);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Конфликты";
            // 
            // comboConflictType
            // 
            this.comboConflictType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboConflictType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConflictType.FormattingEnabled = true;
            this.comboConflictType.Items.AddRange(new object[] {
            "Нет",
            "Конфликт",
            "Конфликт с оповещением"});
            this.comboConflictType.Location = new System.Drawing.Point(38, 19);
            this.comboConflictType.Name = "comboConflictType";
            this.comboConflictType.Size = new System.Drawing.Size(205, 21);
            this.comboConflictType.TabIndex = 2;
            // 
            // PanelServerBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PanelServerBlock";
            this.Size = new System.Drawing.Size(255, 438);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCount)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkInfCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown editCount;
        private panelDistribution panelDistribution;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBufferType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelBuffer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboConflictType;
    }
}
