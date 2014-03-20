namespace ODIS.WinApp
{
    partial class frmQueueStatistic
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
            this.components = new System.ComponentModel.Container();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBuffer = new System.Windows.Forms.Button();
            this.btnSaveInSystem = new System.Windows.Forms.Button();
            this.btSaveOutput = new System.Windows.Forms.Button();
            this.btSaveQueueLength = new System.Windows.Forms.Button();
            this.btSaveProcessed = new System.Windows.Forms.Button();
            this.btSaveSource = new System.Windows.Forms.Button();
            this.btSaveRejected = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelStatistic = new ODIS.WinApp.PanelStatistic();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBuffer);
            this.panel1.Controls.Add(this.btnSaveInSystem);
            this.panel1.Controls.Add(this.btSaveOutput);
            this.panel1.Controls.Add(this.btSaveQueueLength);
            this.panel1.Controls.Add(this.btSaveProcessed);
            this.panel1.Controls.Add(this.btSaveSource);
            this.panel1.Controls.Add(this.btSaveRejected);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 392);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 34);
            this.panel1.TabIndex = 5;
            // 
            // btnBuffer
            // 
            this.btnBuffer.Location = new System.Drawing.Point(473, 6);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(75, 23);
            this.btnBuffer.TabIndex = 7;
            this.btnBuffer.Text = "Буфер";
            this.toolTip1.SetToolTip(this.btnBuffer, "Статистика состояния буфера");
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
            // 
            // btnSaveInSystem
            // 
            this.btnSaveInSystem.Location = new System.Drawing.Point(192, 6);
            this.btnSaveInSystem.Name = "btnSaveInSystem";
            this.btnSaveInSystem.Size = new System.Drawing.Size(75, 23);
            this.btnSaveInSystem.TabIndex = 6;
            this.btnSaveInSystem.Text = "Состояние";
            this.toolTip1.SetToolTip(this.btnSaveInSystem, "Сохранить в файле данные по состоянию системы");
            this.btnSaveInSystem.UseVisualStyleBackColor = true;
            this.btnSaveInSystem.Visible = false;
            this.btnSaveInSystem.Click += new System.EventHandler(this.btnSaveInSystem_Click);
            // 
            // btSaveOutput
            // 
            this.btSaveOutput.Location = new System.Drawing.Point(554, 6);
            this.btSaveOutput.Name = "btSaveOutput";
            this.btSaveOutput.Size = new System.Drawing.Size(75, 23);
            this.btSaveOutput.TabIndex = 5;
            this.btSaveOutput.Text = "Выходящий";
            this.toolTip1.SetToolTip(this.btSaveOutput, "Анализ выходящего потока");
            this.btSaveOutput.UseVisualStyleBackColor = true;
            this.btSaveOutput.Click += new System.EventHandler(this.btSaveOutput_Click);
            // 
            // btSaveQueueLength
            // 
            this.btSaveQueueLength.Location = new System.Drawing.Point(192, 6);
            this.btSaveQueueLength.Name = "btSaveQueueLength";
            this.btSaveQueueLength.Size = new System.Drawing.Size(75, 23);
            this.btSaveQueueLength.TabIndex = 3;
            this.btSaveQueueLength.Text = "Save";
            this.btSaveQueueLength.UseVisualStyleBackColor = true;
            this.btSaveQueueLength.Visible = false;
            this.btSaveQueueLength.Click += new System.EventHandler(this.btSaveQueueLength_Click);
            // 
            // btSaveProcessed
            // 
            this.btSaveProcessed.Location = new System.Drawing.Point(192, 6);
            this.btSaveProcessed.Name = "btSaveProcessed";
            this.btSaveProcessed.Size = new System.Drawing.Size(75, 23);
            this.btSaveProcessed.TabIndex = 1;
            this.btSaveProcessed.Text = "Save";
            this.btSaveProcessed.UseVisualStyleBackColor = true;
            this.btSaveProcessed.Visible = false;
            this.btSaveProcessed.Click += new System.EventHandler(this.btSaveProcessed_Click);
            // 
            // btSaveSource
            // 
            this.btSaveSource.Location = new System.Drawing.Point(192, 6);
            this.btSaveSource.Name = "btSaveSource";
            this.btSaveSource.Size = new System.Drawing.Size(75, 23);
            this.btSaveSource.TabIndex = 1;
            this.btSaveSource.Text = "Save";
            this.btSaveSource.UseVisualStyleBackColor = true;
            this.btSaveSource.Visible = false;
            this.btSaveSource.Click += new System.EventHandler(this.btSaveSource_Click);
            // 
            // btSaveRejected
            // 
            this.btSaveRejected.Location = new System.Drawing.Point(192, 6);
            this.btSaveRejected.Name = "btSaveRejected";
            this.btSaveRejected.Size = new System.Drawing.Size(75, 23);
            this.btSaveRejected.TabIndex = 2;
            this.btSaveRejected.Text = "Save";
            this.btSaveRejected.UseVisualStyleBackColor = true;
            this.btSaveRejected.Visible = false;
            this.btSaveRejected.Click += new System.EventHandler(this.btSaveRejected_Click);
            // 
            // panelStatistic
            // 
            this.panelStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatistic.Location = new System.Drawing.Point(0, 0);
            this.panelStatistic.Name = "panelStatistic";
            this.panelStatistic.Size = new System.Drawing.Size(632, 392);
            this.panelStatistic.TabIndex = 7;
            // 
            // frmQueueStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 426);
            this.Controls.Add(this.panelStatistic);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(640, 460);
            this.Name = "frmQueueStatistic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Статистика";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveInSystem;
        private System.Windows.Forms.Button btSaveOutput;
        private System.Windows.Forms.Button btSaveQueueLength;
        private System.Windows.Forms.Button btSaveProcessed;
        private System.Windows.Forms.Button btSaveSource;
        private System.Windows.Forms.Button btSaveRejected;
        private PanelStatistic panelStatistic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuffer;
    }
}