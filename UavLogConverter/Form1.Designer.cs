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
            this.loadVideoToTrim = new System.Windows.Forms.Button();
            this.startTimetextBox = new System.Windows.Forms.TextBox();
            this.endTimetextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.filePathTextBox.Location = new System.Drawing.Point(111, 210);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(547, 23);
            this.filePathTextBox.TabIndex = 1;
            // 
            // jsonOutput
            // 
            this.jsonOutput.Location = new System.Drawing.Point(112, 256);
            this.jsonOutput.Multiline = true;
            this.jsonOutput.Name = "jsonOutput";
            this.jsonOutput.Size = new System.Drawing.Size(546, 147);
            this.jsonOutput.TabIndex = 2;
            // 
            // GetGpsLocationFormVideoLog
            // 
            this.GetGpsLocationFormVideoLog.Enabled = false;
            this.GetGpsLocationFormVideoLog.Location = new System.Drawing.Point(304, 71);
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
            this.label2.Location = new System.Drawing.Point(253, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Get GPS position from video log CSV";
            // 
            // ScreenShotTimeStamptextBox1
            // 
            this.ScreenShotTimeStamptextBox1.Location = new System.Drawing.Point(304, 42);
            this.ScreenShotTimeStamptextBox1.Name = "ScreenShotTimeStamptextBox1";
            this.ScreenShotTimeStamptextBox1.Size = new System.Drawing.Size(100, 23);
            this.ScreenShotTimeStamptextBox1.TabIndex = 6;
            this.ScreenShotTimeStamptextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenShotTimeStamptextBox1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "[mm:ss.ff] or [mm:ss:ff]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Scrennm Shot Time Stamp";
            // 
            // loadVideoToTrim
            // 
            this.loadVideoToTrim.Enabled = false;
            this.loadVideoToTrim.Location = new System.Drawing.Point(622, 88);
            this.loadVideoToTrim.Name = "loadVideoToTrim";
            this.loadVideoToTrim.Size = new System.Drawing.Size(130, 26);
            this.loadVideoToTrim.TabIndex = 9;
            this.loadVideoToTrim.Text = "Load Video to Trim";
            this.loadVideoToTrim.UseVisualStyleBackColor = true;
            this.loadVideoToTrim.Click += new System.EventHandler(this.button1_Click);
            // 
            // startTimetextBox
            // 
            this.startTimetextBox.Location = new System.Drawing.Point(574, 42);
            this.startTimetextBox.Name = "startTimetextBox";
            this.startTimetextBox.Size = new System.Drawing.Size(100, 23);
            this.startTimetextBox.TabIndex = 10;
            this.startTimetextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartTimetextBox_KeyDown);
            // 
            // endTimetextBox
            // 
            this.endTimetextBox.Enabled = false;
            this.endTimetextBox.Location = new System.Drawing.Point(692, 41);
            this.endTimetextBox.Name = "endTimetextBox";
            this.endTimetextBox.Size = new System.Drawing.Size(100, 23);
            this.endTimetextBox.TabIndex = 11;
            this.endTimetextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.endTimetextBox_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(622, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Trim video log csv";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(598, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Start Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(715, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "End Time";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 416);
            this.Controls.Add(this.loadVideoToTrim);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.endTimetextBox);
            this.Controls.Add(this.startTimetextBox);
            this.Controls.Add(this.jsonOutput);
            this.Controls.Add(this.GetGpsLocationFormVideoLog);
            this.Controls.Add(this.ScreenShotTimeStamptextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LoadDjiCsv);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Button loadVideoToTrim;
        private System.Windows.Forms.TextBox startTimetextBox;
        private System.Windows.Forms.TextBox endTimetextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

