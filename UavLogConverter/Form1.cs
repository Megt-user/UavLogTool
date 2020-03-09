using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UavLogTool;
using UavLogTool.Models;

namespace UavLogConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void LoadDjiCsv_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string djiCsvLog = openFileDialog.FileName;
                filePathTextBox.Text = string.Format("{0}/{1}", Path.GetDirectoryName(djiCsvLog), djiCsvLog);
                filePathTextBox.Refresh();
                DialogResult messageResult = MessageBox.Show($"Do you wanna transform: '{djiCsvLog}'?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (messageResult == DialogResult.Yes)
                {
                    //code for No


                    try
                    {
                        string extension = Path.GetExtension(djiCsvLog).ToLower();
                        if (extension != ".csv")
                        {
                            string message = "Wrong File Type";
                            string title = "Error";
                            MessageBox.Show(message, title);
                        }
                        //string message = "CSV File Not Found";
                        //string title = "Error";
                        //MessageBox.Show(message, title);

                        List<VideoInfoModel> videoInfoModels;
                        var uavLogs = new List<UavLog>();

                        using (TextFieldParser csvParser = new TextFieldParser(djiCsvLog))
                        {
                            uavLogs = CsvUtilities.GetUavLogFromDjiCsv(csvParser);
                        }

                        if (!uavLogs.Any())
                        {
                            string message = "No Video log found in file";
                            string title = "Error";
                            MessageBox.Show(message, title);
                        }

                        var csv = String.Join(",", uavLogs);

                        var dictionarylog = Helpers.SplitVideosFromUavLog(uavLogs);
                        var video1LenghInMilliseconds = Helpers.GetVideoLenghtInMilliseconds(dictionarylog.FirstOrDefault().Value);

                        foreach (var videologs in dictionarylog)
                        {
                            var csvVideoLogs = CsvUtilities.ToCsv(",", videologs.Value);

                            var filename = $"{videologs.Value.FirstOrDefault().DateTime.ToString("_yyyy-MM-dd_HH-mm-ss")}_{videologs.Key}.csv";
                            var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", filename), csvVideoLogs);
                        }
                        videoInfoModels = Helpers.GetVideoInfoModels(dictionarylog);


                        if (videoInfoModels != null)
                        {
                            jsonOutput.Text = JArray.FromObject(videoInfoModels).ToString();
                            jsonOutput.ScrollBars = ScrollBars.Vertical;
                            jsonOutput.WordWrap = false;

                            //var csvVideoInfoModels = CsvUtilities.ToCsv(",", videoInfoModels);
                            //var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(djiCsvLog);
                            //var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", $"{fileNameWithoutExtension}_resume.csv"), csvVideoInfoModels);
                            string message = "File Saved";
                            string title = "OK";

                            MessageBox.Show(message, title);
                        }
                    }
                    catch (IOException)
                    {
                        string message = "Something Went wrong";
                        string title = "Error";
                        MessageBox.Show(message, title);
                    }
                }
            }
        }
    }
}
