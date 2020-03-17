using GeoCoordinatePortable;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private TimeSpan _timeSpan;
        private TimeSpan _StartTimeSpan;
        private TimeSpan _EndTimeSpan;
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

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
                    try
                    {
                        string extension = Path.GetExtension(djiCsvLog).ToLower();
                        if (extension != ".csv")
                        {
                            string message = "Wrong File Type";
                            string title = "Error";
                            MessageBox.Show(message, title);
                        }

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

                        videoInfoModels = Helpers.GetVideoInfoModels(dictionarylog);


                        if (videoInfoModels != null)
                        {
                            jsonOutput.Text = JArray.FromObject(videoInfoModels).ToString();
                            jsonOutput.ScrollBars = ScrollBars.Vertical;
                            jsonOutput.WordWrap = false;


                            AddUavLogToMap(uavLogs);

                            DialogResult saveMessageResult = MessageBox.Show($"Do you wanna to save the Output files?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (saveMessageResult == DialogResult.Yes)
                            {
                                // Show the FolderBrowserDialog.
                                DialogResult folderBrowser = folderBrowserDialog1.ShowDialog();
                                if (folderBrowser == DialogResult.OK)
                                {
                                    string folderPath = folderBrowserDialog1.SelectedPath;
                                    foreach (var videologs in dictionarylog)
                                    {
                                        var csvVideoLogs = CsvUtilities.ToCsv(",", videologs.Value);

                                        var filename = $"{videologs.Value.FirstOrDefault().DateTime.ToString("_yyyy-MM-dd_HH-mm-ss")}_{videologs.Key}.csv";
                                        var csvFileSaved = CsvUtilities.SaveCsvTofile(Path.Combine(folderPath, filename), csvVideoLogs);
                                        if (!csvFileSaved)
                                        {
                                            saveMessageResult = MessageBox.Show($"cant save csv file {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            jsonOutput.Text = "";
                                            jsonOutput.Refresh();
                                            return;
                                        }

                                    }

                                    var csvVideoInfoModels = CsvUtilities.ToCsv(",", videoInfoModels);
                                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(djiCsvLog);
                                    var savedVideoSummary = CsvUtilities.SaveCsvTofile(Path.Combine(folderPath, $"{fileNameWithoutExtension}_resume.csv"), csvVideoInfoModels);
                                    if (!savedVideoSummary)
                                    {
                                        saveMessageResult = MessageBox.Show($"cant save csv file {fileNameWithoutExtension}_resume.csv", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        jsonOutput.Text = "";
                                        jsonOutput.Refresh();
                                        return;
                                    }
                                    saveMessageResult = MessageBox.Show($"files saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
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

        private void GetGpsLocationFormVideoLog_Click(object videoLogCsv, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string videoLogCsvPath = openFileDialog.FileName;

                filePathTextBox.Text = string.Format("{0}/{1}", Path.GetDirectoryName(videoLogCsvPath), videoLogCsvPath);
                filePathTextBox.Refresh();
                DialogResult messageResult = MessageBox.Show($"You want to get screenshot '{ScreenShotTimeStamptextBox1.Text}' GPS position from file:'{Path.GetFileName(videoLogCsvPath)}'?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (messageResult == DialogResult.Yes)
                {
                    try
                    {
                        string extension = Path.GetExtension(videoLogCsvPath).ToLower();
                        if (extension != ".csv")
                        {
                            string message = "Wrong File Type";
                            string title = "Error";
                            MessageBox.Show(message, title);
                            GetGpsLocationFormVideoLog.PerformClick();
                        }
                        else
                        {


                            List<UavLog> uavLogs = new List<UavLog>();

                            using (TextFieldParser csvParser = new TextFieldParser(videoLogCsvPath))
                            {
                                uavLogs = CsvUtilities.GetUavLogFromTextFile(csvParser);
                            }

                            var sortedUavLogs = Helpers.FilterUavlogAndSort(uavLogs);
                            var videoLenght = Helpers.GetVideoLenghtInMilliseconds(sortedUavLogs);
                            var videoLengh = Helpers.ConvertMilisecondsToHMSmm(videoLenght);
                            if (_timeSpan.TotalMilliseconds < videoLenght)
                            {
                                var photolog = Helpers.GetUavLogFromVideoTimeStamp(_timeSpan, uavLogs);
                                jsonOutput.Text = JsonConvert.SerializeObject(photolog);
                                jsonOutput.ScrollBars = ScrollBars.Vertical;
                                jsonOutput.WordWrap = false;

                                AddUavLogToMap(photolog);
                            }
                            else
                            {
                                MessageBox.Show($"Screenshot time {ScreenShotTimeStamptextBox1.Text} is bigger than the video time {videoLengh}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ScreenShotTimeStamptextBox1.Focus();
                                GetGpsLocationFormVideoLog.Enabled = false;
                                filePathTextBox.Text = "";
                                filePathTextBox.Refresh();
                            }
                        }
                    }
                    catch
                    {
                        //
                    }
                }
                else
                {
                    GetGpsLocationFormVideoLog.PerformClick();
                }
            }
        }

        private void ScreenShotTimeStamptextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(ScreenShotTimeStamptextBox1.Text))
                {
                    MessageBox.Show($"You must enter the timestamp from the screenshot", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ScreenShotTimeStamptextBox1.Focus();
                    GetGpsLocationFormVideoLog.Enabled = false;
                }
                else
                {
                    var timeStamp = Helpers.GetTimeSpan(ScreenShotTimeStamptextBox1.Text);
                    if (timeStamp == TimeSpan.Zero)
                    {
                        MessageBox.Show($"wrong screenshot timestamp", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ScreenShotTimeStamptextBox1.Focus();
                        GetGpsLocationFormVideoLog.Enabled = false;
                    }
                    else
                    {
                        _timeSpan = timeStamp;
                        GetGpsLocationFormVideoLog.Enabled = true;
                    }
                }
            }
        }
        SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
        private void endTimetextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (endTimetextBox.Text == "0" || string.IsNullOrEmpty(endTimetextBox.Text))
                {
                    MessageBox.Show($"You must enter the End time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ScreenShotTimeStamptextBox1.Focus();
                    GetGpsLocationFormVideoLog.Enabled = false;
                    endTimetextBox.Focus();
                }
                else
                {
                    var timeStamp = Helpers.GetTimeSpan(endTimetextBox.Text);
                    if (timeStamp == TimeSpan.Zero)
                    {
                        MessageBox.Show($"wrong end time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        endTimetextBox.Focus();
                    }
                    else
                    {
                        _EndTimeSpan = timeStamp;
                        loadVideoToTrim.Enabled = true;
                    }

                }
            }
        }
        private void StartTimetextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (startTimetextBox.Text == "0" || string.IsNullOrEmpty(startTimetextBox.Text))
                {
                    _StartTimeSpan = TimeSpan.Zero;
                    startTimetextBox.Text = "00:00.00";
                    startTimetextBox.Refresh();
                    endTimetextBox.Enabled = true;
                    endTimetextBox.Focus();
                }
                else
                {
                    var timeStamp = Helpers.GetTimeSpan(startTimetextBox.Text);
                    if (timeStamp == TimeSpan.Zero)
                    {
                        MessageBox.Show($"wrong screenshot timestamp", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        startTimetextBox.Focus();
                        endTimetextBox.Enabled = false;
                        loadVideoToTrim.Enabled = false;
                    }
                    else
                    {
                        _StartTimeSpan = timeStamp;
                        endTimetextBox.Enabled = true;
                        endTimetextBox.Focus();
                    }

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string videoLogCsvPath = openFileDialog.FileName;

                filePathTextBox.Text = string.Format("{0}/{1}", Path.GetDirectoryName(videoLogCsvPath), videoLogCsvPath);
                filePathTextBox.Refresh();
                DialogResult messageResult = MessageBox.Show($"You want trim Video Log '{Path.GetFileName(videoLogCsvPath)}' from: '{startTimetextBox.Text} to {endTimetextBox.Text}' ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (messageResult == DialogResult.Yes)
                {
                    try
                    {
                        string extension = Path.GetExtension(videoLogCsvPath).ToLower();
                        if (extension != ".csv")
                        {
                            string message = "Wrong File Type";
                            string title = "Error";
                            MessageBox.Show(message, title);
                            GetGpsLocationFormVideoLog.PerformClick();
                        }
                        else
                        {


                            List<UavLog> uavLogs = new List<UavLog>();

                            using (TextFieldParser csvParser = new TextFieldParser(videoLogCsvPath))
                            {
                                uavLogs = CsvUtilities.GetUavLogFromTextFile(csvParser);
                            }

                            var sortUavList = Helpers.FilterUavlogAndSort(uavLogs);

                            var videoLenght = Helpers.GetVideoLenghtInMilliseconds(sortUavList);
                            var videoLengh = Helpers.ConvertMilisecondsToHMSmm(videoLenght);

                            if (_EndTimeSpan.TotalMilliseconds > videoLenght)
                            {
                                MessageBox.Show($"The end time '{endTimetextBox.Text}' exceeds the length of the video '{videoLengh}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                endTimetextBox.Focus();
                                loadVideoToTrim.Enabled = false;
                                return;
                            }

                            var newUavlogs = Helpers.TrimUavLogs(sortUavList, _StartTimeSpan, _EndTimeSpan);
                            var videoInfoModels = Helpers.GetVideoInfoModels(newUavlogs);

                            if (videoInfoModels != null)
                            {

                                jsonOutput.Text = JArray.FromObject(videoInfoModels).ToString();
                                jsonOutput.ScrollBars = ScrollBars.Vertical;
                                jsonOutput.WordWrap = false;



                                DialogResult saveMessageResult = MessageBox.Show($"Do you wanna to save the Output fil?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (saveMessageResult == DialogResult.Yes)
                                {
                                    // Show the FolderBrowserDialog.
                                    DialogResult folderBrowser = folderBrowserDialog1.ShowDialog();
                                    if (folderBrowser == DialogResult.OK)
                                    {
                                        string folderPath = folderBrowserDialog1.SelectedPath;
                                        var csvVideoLogs = CsvUtilities.ToCsv(",", newUavlogs);

                                        var filename = $"{newUavlogs.FirstOrDefault().DateTime.ToString("_yyyy-MM-dd_HH-mm-ss")}_trimmed.csv";
                                        var csvFileSaved = CsvUtilities.SaveCsvTofile(Path.Combine(folderPath, filename), csvVideoLogs);
                                        if (!csvFileSaved)
                                        {
                                            saveMessageResult = MessageBox.Show($"cant save csv file {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            jsonOutput.Text = "";
                                            jsonOutput.Refresh();
                                            return;
                                        }


                                        var csvVideoInfoModels = CsvUtilities.ToCsv(",", videoInfoModels);
                                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(videoLogCsvPath);
                                        var savedVideoSummary = CsvUtilities.SaveCsvTofile(Path.Combine(folderPath, $"{fileNameWithoutExtension}_trimmed_resume.csv"), csvVideoInfoModels);
                                        if (!savedVideoSummary)
                                        {
                                            saveMessageResult = MessageBox.Show($"cant save csv file {fileNameWithoutExtension}_trimmed_resume.csv", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            jsonOutput.Text = "";
                                            jsonOutput.Refresh();
                                            return;
                                        }
                                        saveMessageResult = MessageBox.Show($"files saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show($"Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        public void AddUavLogToMap(List<UavLog> uavLogs)
        {
            //https://stackoverflow.com/a/31764053

            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.MapProvider = GMapProviders.GoogleMap;



            //TODO worck with To points
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
            //  GMarkerGoogleType.green);
            //markersOverlay.Markers.Add(marker);

            //marker = new GMarkerGoogle(new PointLatLng(-43.966688, 11.580528),
            // GMarkerGoogleType.green);
            //markersOverlay.Markers.Add(marker);
            //gMapControl1.Overlays.Add(markersOverlay);


            //gMapControl1.ZoomAndCenterMarkers("markers");


            //todo Zoom to marker not working
            //Clean the map
            gMapControl1.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");

            var newUavlog = GMapUtilities.FilterUavLogByDistance(uavLogs, 80);

            var points = new List<PointLatLng>();
            for (int index = 0; index < newUavlog.Count - 1; index++)
            {

                double lat = Convert.ToDouble(newUavlog[index].UavLatititud);
                double lng = Convert.ToDouble(newUavlog[index].UavLongitud);

                PointLatLng pointLatLng = new PointLatLng(lat, lng);
                points.Add(pointLatLng);


                //Create a red marker
                GMarkerGoogle marker1 = new GMarkerGoogle(pointLatLng, GMarkerGoogleType.green);

                //Add a marker on the overlay
                markersOverlay.Markers.Add(marker1);
            }


            var route = new GMapRoute(points, "sample route");

            route.Stroke = new Pen(new Color());
            route.Stroke.Color = Color.DarkRed;
            route.Stroke.Width = 4;
            route.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            route.Stroke.StartCap = LineCap.NoAnchor;
            route.Stroke.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            route.Stroke.LineJoin = LineJoin.Round;

            GMapOverlay uavTrack = new GMapOverlay("Uav track");

            uavTrack.Routes.Add(route);



            //Add the overlay on the gMapControl1(Map)
            gMapControl1.Overlays.Add(uavTrack);
            gMapControl1.Overlays.Add(markersOverlay);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            //double zoomAtual = gMapControl1.Zoom;
            //gMapControl1.Zoom = zoomAtual + 1;
            //gMapControl1.Zoom = zoomAtual;
            gMapControl1.ZoomAndCenterMarkers("markers");


        }
        public void AddUavLogToMap(UavLog uavLog)
        {
            //https://stackoverflow.com/a/31764053

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;

            //Clean the map
            gMapControl1.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");


            double lat = Convert.ToDouble(uavLog.UavLatititud);
            double lng = Convert.ToDouble(uavLog.UavLongitud);
            PointLatLng pointLatLng = new PointLatLng(lat, lng);

            //Create a red marker
            GMarkerGoogle marker1 = new GMarkerGoogle(pointLatLng, GMarkerGoogleType.green);

            //Add a marker on the overlay
            markersOverlay.Markers.Add(marker1);

            //Add the overlay on the gMapControl1(Map)
            gMapControl1.Overlays.Add(markersOverlay);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            double zoomAtual = gMapControl1.Zoom;
            gMapControl1.Zoom = zoomAtual + 1;
            gMapControl1.Zoom = zoomAtual;
        }
    }
}
