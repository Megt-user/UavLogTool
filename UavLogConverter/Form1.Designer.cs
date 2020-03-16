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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.splitter1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadDjiCsv
            // 
            this.LoadDjiCsv.Location = new System.Drawing.Point(55, 36);
            this.LoadDjiCsv.Name = "LoadDjiCsv";
            this.LoadDjiCsv.Size = new System.Drawing.Size(67, 42);
            this.LoadDjiCsv.TabIndex = 0;
            this.LoadDjiCsv.Text = " DJI CSV File";
            this.LoadDjiCsv.UseVisualStyleBackColor = true;
            this.LoadDjiCsv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LoadDjiCsv_Click);

            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(12, 334);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(258, 20);
            this.filePathTextBox.TabIndex = 1;
            // 
            // jsonOutput
            // 
            this.jsonOutput.Location = new System.Drawing.Point(9, 360);
            this.jsonOutput.Multiline = true;
            this.jsonOutput.Name = "jsonOutput";
            this.jsonOutput.Size = new System.Drawing.Size(261, 210);
            this.jsonOutput.TabIndex = 2;
            // 
            // GetGpsLocationFormVideoLog
            // 
            this.GetGpsLocationFormVideoLog.Enabled = false;
            this.GetGpsLocationFormVideoLog.Location = new System.Drawing.Point(51, 158);
            this.GetGpsLocationFormVideoLog.Name = "GetGpsLocationFormVideoLog";
            this.GetGpsLocationFormVideoLog.Size = new System.Drawing.Size(92, 52);
            this.GetGpsLocationFormVideoLog.TabIndex = 3;
            this.GetGpsLocationFormVideoLog.Text = "Video Log Csv File";
            this.GetGpsLocationFormVideoLog.UseVisualStyleBackColor = true;
            this.GetGpsLocationFormVideoLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GetGpsLocationFormVideoLog_Click);

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Convert DJI csv to Personalize Video log";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Get GPS position from video log CSV";
            // 
            // ScreenShotTimeStamptextBox1
            // 
            this.ScreenShotTimeStamptextBox1.Location = new System.Drawing.Point(55, 135);
            this.ScreenShotTimeStamptextBox1.Name = "ScreenShotTimeStamptextBox1";
            this.ScreenShotTimeStamptextBox1.Size = new System.Drawing.Size(86, 20);
            this.ScreenShotTimeStamptextBox1.TabIndex = 6;
            this.ScreenShotTimeStamptextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenShotTimeStamptextBox1_KeyDown);

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "[mm:ss.ff] or [mm:ss:ff]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Scrennm Shot Time Stamp";
            // 
            // loadVideoToTrim
            // 
            this.loadVideoToTrim.Enabled = false;
            this.loadVideoToTrim.Location = new System.Drawing.Point(50, 296);
            this.loadVideoToTrim.Name = "loadVideoToTrim";
            this.loadVideoToTrim.Size = new System.Drawing.Size(111, 23);
            this.loadVideoToTrim.TabIndex = 9;
            this.loadVideoToTrim.Text = "Load Video to Trim";
            this.loadVideoToTrim.UseVisualStyleBackColor = true;
            this.loadVideoToTrim.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_Click);

            // 
            // startTimetextBox
            // 
            this.startTimetextBox.Location = new System.Drawing.Point(9, 257);
            this.startTimetextBox.Name = "startTimetextBox";
            this.startTimetextBox.Size = new System.Drawing.Size(86, 20);
            this.startTimetextBox.TabIndex = 10;
            this.startTimetextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartTimetextBox_KeyDown);

            // 
            // endTimetextBox
            // 
            this.endTimetextBox.Enabled = false;
            this.endTimetextBox.Location = new System.Drawing.Point(110, 256);
            this.endTimetextBox.Name = "endTimetextBox";
            this.endTimetextBox.Size = new System.Drawing.Size(86, 20);
            this.endTimetextBox.TabIndex = 11;
            this.endTimetextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.endTimetextBox_KeyDown);

            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Trim video log csv";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Start Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "End Time";
            // 
            // splitter1
            // 
            this.splitter1.Controls.Add(this.ScreenShotTimeStamptextBox1);
            this.splitter1.Controls.Add(this.label4);
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(280, 582);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "[mm:ss.ff] or [mm:ss:ff]";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(341, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Map";
            // 
            // gMapControl1
            // 
            this.gMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(300, 95);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 100;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(688, 475);
            this.gMapControl1.TabIndex = 16;
            this.gMapControl1.Zoom = 0D;

            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 582);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GetGpsLocationFormVideoLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.jsonOutput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.loadVideoToTrim);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LoadDjiCsv);
            this.Controls.Add(this.startTimetextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endTimetextBox);
            this.Controls.Add(this.splitter1);
            this.Name = "Form1";
            this.Text = "Uav Log Converter";
            this.splitter1.ResumeLayout(false);
            this.splitter1.PerformLayout();
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
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}

