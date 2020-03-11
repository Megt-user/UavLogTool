namespace UavLogConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadDjiCsv = new System.Windows.Forms.Button();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.jsonOutput = new System.Windows.Forms.TextBox();
            this.GetGpsLocationFormVideoLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ScreenShotTimeStamptextBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadDjiCsv
            // 
            this.LoadDjiCsv.Location = new System.Drawing.Point(64, 27);
            this.LoadDjiCsv.Name = "LoadDjiCsv";
            this.LoadDjiCsv.Size = new System.Drawing.Size(78, 48);
            this.LoadDjiCsv.TabIndex = 0;
            this.LoadDjiCsv.Text = " DJI CSV File";
            this.LoadDjiCsv.UseVisualStyleBackColor = true;
            this.LoadDjiCsv.Click += new System.EventHandler(this.LoadDjiCsv_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(124, 227);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(547, 23);
            this.filePathTextBox.TabIndex = 1;
            // 
            // jsonOutput
            // 
            this.jsonOutput.Location = new System.Drawing.Point(124, 256);
            this.jsonOutput.Multiline = true;
            this.jsonOutput.Name = "jsonOutput";
            this.jsonOutput.Size = new System.Drawing.Size(546, 147);
            this.jsonOutput.TabIndex = 2;
            // 
            // GetGpsLocationFormVideoLog
            // 
            this.GetGpsLocationFormVideoLog.Enabled = false;
            this.GetGpsLocationFormVideoLog.Location = new System.Drawing.Point(342, 81);
            this.GetGpsLocationFormVideoLog.Name = "GetGpsLocationFormVideoLog";
            this.GetGpsLocationFormVideoLog.Size = new System.Drawing.Size(107, 60);
            this.GetGpsLocationFormVideoLog.TabIndex = 3;
            this.GetGpsLocationFormVideoLog.Text = "Video Log Csv File";
            this.GetGpsLocationFormVideoLog.UseVisualStyleBackColor = true;
            this.GetGpsLocationFormVideoLog.Click += new System.EventHandler(this.GetGpsLocationFormVideoLog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Convert DJI csv to Personalize Video log";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Get GPS position from video log CSV";
            // 
            // ScreenShotTimeStamptextBox1
            // 
            this.ScreenShotTimeStamptextBox1.Location = new System.Drawing.Point(349, 52);
            this.ScreenShotTimeStamptextBox1.Name = "ScreenShotTimeStamptextBox1";
            this.ScreenShotTimeStamptextBox1.Size = new System.Drawing.Size(100, 23);
            this.ScreenShotTimeStamptextBox1.TabIndex = 6;
            this.ScreenShotTimeStamptextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenShotTimeStamptextBox1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "[mm:ss.ff] or [mm:ss:ff]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Scrennm Shot Time Stamp";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GetGpsLocationFormVideoLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ScreenShotTimeStamptextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoadDjiCsv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.jsonOutput);
            this.Name = "Form1";
            this.Text = "Uav Log Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadDjiCsv;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.TextBox jsonOutput;
        private System.Windows.Forms.Button GetGpsLocationFormVideoLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ScreenShotTimeStamptextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

