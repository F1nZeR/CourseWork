using ODIS.Controls;
namespace ODIS.WinApp
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editStopTime = new ODIS.Controls.MyNumericUpDown();
            this.editSMOCount = new ODIS.Controls.MyNumericUpDown();
            this.btRunModel = new System.Windows.Forms.Button();
            this.checkStopTime = new System.Windows.Forms.CheckBox();
            this.checkStopEventCount = new System.Windows.Forms.CheckBox();
            this.labelStopTime = new System.Windows.Forms.Label();
            this.labelStopEventCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serverBlockPanel = new ODIS.WinApp.PanelServerBlock();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panelStream = new ODIS.Controls.panelStream();
            this.editStreamsTime = new ODIS.Controls.MyNumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btRunStreams = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelProcess = new ODIS.Controls.panelProcess();
            this.editProcessesTime = new ODIS.Controls.MyNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btRunProcesses = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btPreferences = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelDistribution = new ODIS.Controls.panelDistribution();
            this.editGenerationCount = new ODIS.Controls.MyNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btRandomGenerate = new System.Windows.Forms.Button();
            this.sourcePanel = new ODIS.WinApp.panelSource();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editStopTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSMOCount)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editStreamsTime)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProcessesTime)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editGenerationCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editStopTime);
            this.groupBox1.Controls.Add(this.editSMOCount);
            this.groupBox1.Controls.Add(this.btRunModel);
            this.groupBox1.Controls.Add(this.checkStopTime);
            this.groupBox1.Controls.Add(this.checkStopEventCount);
            this.groupBox1.Controls.Add(this.labelStopTime);
            this.groupBox1.Controls.Add(this.labelStopEventCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(778, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры моделирования";
            // 
            // editStopTime
            // 
            this.editStopTime.ChangeSignDecPlaces = 0;
            this.editStopTime.Enabled = false;
            this.editStopTime.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editStopTime.Location = new System.Drawing.Point(459, 31);
            this.editStopTime.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editStopTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editStopTime.Name = "editStopTime";
            this.editStopTime.Size = new System.Drawing.Size(82, 20);
            this.editStopTime.TabIndex = 9;
            this.editStopTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editStopTime.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.editStopTime.Visible = false;
            // 
            // editSMOCount
            // 
            this.editSMOCount.ChangeSignDecPlaces = 0;
            this.editSMOCount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editSMOCount.Location = new System.Drawing.Point(27, 31);
            this.editSMOCount.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editSMOCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editSMOCount.Name = "editSMOCount";
            this.editSMOCount.Size = new System.Drawing.Size(82, 20);
            this.editSMOCount.TabIndex = 8;
            this.editSMOCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editSMOCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btRunModel
            // 
            this.btRunModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRunModel.Location = new System.Drawing.Point(259, 29);
            this.btRunModel.Name = "btRunModel";
            this.btRunModel.Size = new System.Drawing.Size(75, 23);
            this.btRunModel.TabIndex = 7;
            this.btRunModel.Text = "Run!";
            this.btRunModel.UseVisualStyleBackColor = true;
            this.btRunModel.Click += new System.EventHandler(this.btRun_Click);
            // 
            // checkStopTime
            // 
            this.checkStopTime.AutoSize = true;
            this.checkStopTime.Location = new System.Drawing.Point(438, 33);
            this.checkStopTime.Name = "checkStopTime";
            this.checkStopTime.Size = new System.Drawing.Size(15, 14);
            this.checkStopTime.TabIndex = 6;
            this.checkStopTime.UseVisualStyleBackColor = true;
            this.checkStopTime.Visible = false;
            this.checkStopTime.CheckedChanged += new System.EventHandler(this.checkStopEventCount_CheckedChanged);
            // 
            // checkStopEventCount
            // 
            this.checkStopEventCount.AutoSize = true;
            this.checkStopEventCount.Checked = true;
            this.checkStopEventCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkStopEventCount.Location = new System.Drawing.Point(6, 35);
            this.checkStopEventCount.Name = "checkStopEventCount";
            this.checkStopEventCount.Size = new System.Drawing.Size(15, 14);
            this.checkStopEventCount.TabIndex = 5;
            this.checkStopEventCount.UseVisualStyleBackColor = true;
            this.checkStopEventCount.Visible = false;
            this.checkStopEventCount.CheckedChanged += new System.EventHandler(this.checkStopEventCount_CheckedChanged);
            // 
            // labelStopTime
            // 
            this.labelStopTime.AutoSize = true;
            this.labelStopTime.Enabled = false;
            this.labelStopTime.Location = new System.Drawing.Point(547, 34);
            this.labelStopTime.Name = "labelStopTime";
            this.labelStopTime.Size = new System.Drawing.Size(153, 13);
            this.labelStopTime.TabIndex = 4;
            this.labelStopTime.Text = "усл.ед. модельного времени";
            this.labelStopTime.Visible = false;
            // 
            // labelStopEventCount
            // 
            this.labelStopEventCount.AutoSize = true;
            this.labelStopEventCount.Location = new System.Drawing.Point(115, 35);
            this.labelStopEventCount.Name = "labelStopEventCount";
            this.labelStopEventCount.Size = new System.Drawing.Size(95, 13);
            this.labelStopEventCount.TabIndex = 2;
            this.labelStopEventCount.Text = "входящих заявок";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Условие окончания:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(792, 526);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.sourcePanel);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(784, 500);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "СМО";
            this.tabPage2.ToolTipText = "Системы массового обслуживания";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.serverBlockPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(238, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 431);
            this.panel1.TabIndex = 3;
            // 
            // serverBlockPanel
            // 
            this.serverBlockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverBlockPanel.Location = new System.Drawing.Point(0, 0);
            this.serverBlockPanel.Name = "serverBlockPanel";
            this.serverBlockPanel.Size = new System.Drawing.Size(408, 431);
            this.serverBlockPanel.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(784, 500);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Потоки";
            this.tabPage4.ToolTipText = "Потоки событий";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panelStream);
            this.groupBox4.Controls.Add(this.editStreamsTime);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btRunStreams);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(784, 360);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // panelStream
            // 
            this.panelStream.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStream.Location = new System.Drawing.Point(3, 16);
            this.panelStream.Name = "panelStream";
            this.panelStream.Size = new System.Drawing.Size(270, 341);
            this.panelStream.TabIndex = 12;
            this.panelStream.Title = "Поток событий";
            // 
            // editStreamsTime
            // 
            this.editStreamsTime.ChangeSignDecPlaces = 0;
            this.editStreamsTime.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editStreamsTime.Location = new System.Drawing.Point(408, 20);
            this.editStreamsTime.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editStreamsTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editStreamsTime.Name = "editStreamsTime";
            this.editStreamsTime.Size = new System.Drawing.Size(82, 20);
            this.editStreamsTime.TabIndex = 11;
            this.editStreamsTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editStreamsTime.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Время моделирования";
            // 
            // btRunStreams
            // 
            this.btRunStreams.Location = new System.Drawing.Point(415, 102);
            this.btRunStreams.Name = "btRunStreams";
            this.btRunStreams.Size = new System.Drawing.Size(75, 23);
            this.btRunStreams.TabIndex = 2;
            this.btRunStreams.Text = "Run";
            this.btRunStreams.UseVisualStyleBackColor = true;
            this.btRunStreams.Click += new System.EventHandler(this.btRunStreams_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(784, 500);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Процессы";
            this.tabPage3.ToolTipText = "Случайные процессы";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelProcess);
            this.groupBox3.Controls.Add(this.editProcessesTime);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btRunProcesses);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(784, 237);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // panelProcess
            // 
            this.panelProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProcess.Location = new System.Drawing.Point(3, 16);
            this.panelProcess.Name = "panelProcess";
            this.panelProcess.Size = new System.Drawing.Size(270, 218);
            this.panelProcess.TabIndex = 12;
            this.panelProcess.Title = "Случайный процесс";
            // 
            // editProcessesTime
            // 
            this.editProcessesTime.ChangeSignDecPlaces = 0;
            this.editProcessesTime.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editProcessesTime.Location = new System.Drawing.Point(408, 20);
            this.editProcessesTime.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editProcessesTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editProcessesTime.Name = "editProcessesTime";
            this.editProcessesTime.Size = new System.Drawing.Size(82, 20);
            this.editProcessesTime.TabIndex = 11;
            this.editProcessesTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editProcessesTime.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Время моделирования";
            // 
            // btRunProcesses
            // 
            this.btRunProcesses.Location = new System.Drawing.Point(415, 102);
            this.btRunProcesses.Name = "btRunProcesses";
            this.btRunProcesses.Size = new System.Drawing.Size(75, 23);
            this.btRunProcesses.TabIndex = 2;
            this.btRunProcesses.Text = "Run";
            this.btRunProcesses.UseVisualStyleBackColor = true;
            this.btRunProcesses.Click += new System.EventHandler(this.btRunProcesses_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btPreferences);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(784, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Величины";
            this.tabPage1.ToolTipText = "Случайные величины";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btPreferences
            // 
            this.btPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPreferences.Location = new System.Drawing.Point(700, 469);
            this.btPreferences.Name = "btPreferences";
            this.btPreferences.Size = new System.Drawing.Size(75, 23);
            this.btPreferences.TabIndex = 5;
            this.btPreferences.Text = "Настройки";
            this.btPreferences.UseVisualStyleBackColor = true;
            this.btPreferences.Click += new System.EventHandler(this.btPreferences_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelDistribution);
            this.groupBox2.Controls.Add(this.editGenerationCount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btRandomGenerate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(778, 199);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Распределение";
            // 
            // panelDistribution
            // 
            this.panelDistribution.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDistribution.Location = new System.Drawing.Point(3, 16);
            this.panelDistribution.Name = "panelDistribution";
            this.panelDistribution.Size = new System.Drawing.Size(203, 180);
            this.panelDistribution.TabIndex = 6;
            // 
            // editGenerationCount
            // 
            this.editGenerationCount.ChangeSignDecPlaces = 0;
            this.editGenerationCount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.editGenerationCount.Location = new System.Drawing.Point(368, 20);
            this.editGenerationCount.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.editGenerationCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editGenerationCount.Name = "editGenerationCount";
            this.editGenerationCount.Size = new System.Drawing.Size(93, 20);
            this.editGenerationCount.TabIndex = 5;
            this.editGenerationCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editGenerationCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Объем генерации";
            // 
            // btRandomGenerate
            // 
            this.btRandomGenerate.Location = new System.Drawing.Point(697, 153);
            this.btRandomGenerate.Name = "btRandomGenerate";
            this.btRandomGenerate.Size = new System.Drawing.Size(75, 23);
            this.btRandomGenerate.TabIndex = 2;
            this.btRandomGenerate.Text = "Generate";
            this.btRandomGenerate.UseVisualStyleBackColor = true;
            this.btRandomGenerate.Click += new System.EventHandler(this.btRandomGenerate_Click);
            // 
            // sourcePanel
            // 
            this.sourcePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sourcePanel.Location = new System.Drawing.Point(3, 66);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(235, 431);
            this.sourcePanel.TabIndex = 1;
            this.sourcePanel.Title = "Входящий поток";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 526);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(800, 560);
            this.Name = "frmMain";
            this.Text = "ODIS\'11";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editStopTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSMOCount)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editStreamsTime)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProcessesTime)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editGenerationCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelStopTime;
        private System.Windows.Forms.Label labelStopEventCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkStopTime;
        private System.Windows.Forms.CheckBox checkStopEventCount;
        private System.Windows.Forms.Button btRunModel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btRandomGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btPreferences;
        private PanelServerBlock serverBlockPanel;
        private MyNumericUpDown editGenerationCount;
        private MyNumericUpDown editStopTime;
        private MyNumericUpDown editSMOCount;
        private System.Windows.Forms.Panel panel1;
        private panelDistribution panelDistribution;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private MyNumericUpDown editProcessesTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btRunProcesses;
        private panelProcess panelProcess;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox4;
        private panelStream panelStream;
        private MyNumericUpDown editStreamsTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btRunStreams;
        private panelSource sourcePanel;
    }
}

