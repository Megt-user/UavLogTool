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
            this.SuspendLayout();
            // 
            // LoadDjiCsv
            // 
            this.LoadDjiCsv.Location = new System.Drawing.Point(12, 65);
            this.LoadDjiCsv.Name = "LoadDjiCsv";
            this.LoadDjiCsv.Size = new System.Drawing.Size(96, 23);
            this.LoadDjiCsv.TabIndex = 0;
            this.LoadDjiCsv.Text = "Load DJI CSV";
            this.LoadDjiCsv.UseVisualStyleBackColor = true;
            this.LoadDjiCsv.Click += new System.EventHandler(this.LoadDjiCsv_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(124, 65);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(547, 23);
            this.filePathTextBox.TabIndex = 1;
            // 
            // jsonOutput
            // 
            this.jsonOutput.Location = new System.Drawing.Point(125, 266);
            this.jsonOutput.Multiline = true;
            this.jsonOutput.Name = "jsonOutput";
            this.jsonOutput.Size = new System.Drawing.Size(546, 147);
            this.jsonOutput.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.jsonOutput);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.LoadDjiCsv);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadDjiCsv;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.TextBox jsonOutput;
    }
}

