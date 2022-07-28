
namespace CETAK_KARTU_MANULIFE
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.LblCycle = new System.Windows.Forms.Label();
            this.LblData = new System.Windows.Forms.Label();
            this.TextData = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TextCycle = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnProses = new System.Windows.Forms.Button();
            this.LblPersen = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(776, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "CETAK KARTU MANULIFE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblCycle
            // 
            this.LblCycle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblCycle.Location = new System.Drawing.Point(12, 102);
            this.LblCycle.Name = "LblCycle";
            this.LblCycle.Size = new System.Drawing.Size(167, 30);
            this.LblCycle.TabIndex = 1;
            this.LblCycle.Text = "PILIH CYCLE";
            this.LblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblData
            // 
            this.LblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblData.Location = new System.Drawing.Point(12, 143);
            this.LblData.Name = "LblData";
            this.LblData.Size = new System.Drawing.Size(167, 30);
            this.LblData.TabIndex = 1;
            this.LblData.Text = "DATA";
            this.LblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextData
            // 
            this.TextData.Location = new System.Drawing.Point(195, 143);
            this.TextData.Multiline = true;
            this.TextData.Name = "TextData";
            this.TextData.Size = new System.Drawing.Size(593, 188);
            this.TextData.TabIndex = 2;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(195, 102);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(380, 26);
            this.dateTimePicker.TabIndex = 3;
            this.dateTimePicker.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
            // 
            // TextCycle
            // 
            this.TextCycle.Location = new System.Drawing.Point(594, 102);
            this.TextCycle.Name = "TextCycle";
            this.TextCycle.Size = new System.Drawing.Size(194, 26);
            this.TextCycle.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(195, 343);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(593, 31);
            this.progressBar.TabIndex = 4;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(620, 385);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(168, 50);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.Text = "KELUAR";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnProses
            // 
            this.BtnProses.Location = new System.Drawing.Point(432, 385);
            this.BtnProses.Name = "BtnProses";
            this.BtnProses.Size = new System.Drawing.Size(168, 50);
            this.BtnProses.TabIndex = 5;
            this.BtnProses.Text = "PROSES";
            this.BtnProses.UseVisualStyleBackColor = true;
            this.BtnProses.Click += new System.EventHandler(this.BtnProses_Click);
            // 
            // LblPersen
            // 
            this.LblPersen.AutoSize = true;
            this.LblPersen.BackColor = System.Drawing.Color.Transparent;
            this.LblPersen.Location = new System.Drawing.Point(467, 349);
            this.LblPersen.Name = "LblPersen";
            this.LblPersen.Size = new System.Drawing.Size(32, 20);
            this.LblPersen.TabIndex = 6;
            this.LblPersen.Text = "0%";
            this.LblPersen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LblPersen);
            this.Controls.Add(this.BtnProses);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.TextCycle);
            this.Controls.Add(this.TextData);
            this.Controls.Add(this.LblData);
            this.Controls.Add(this.LblCycle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCycle;
        private System.Windows.Forms.Label LblData;
        private System.Windows.Forms.TextBox TextData;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox TextCycle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnProses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblPersen;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

