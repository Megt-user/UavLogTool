using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UavLogTool;
using UavLogTool.Models;
using Xunit;


namespace UavLogToolTest
{
    public class SrtConverterTests
    {
        [Fact]
        public void TrimUavLogs()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            DjiSrtVideoLogModel djiSrtVideoLogModel = new DjiSrtVideoLogModel();
            List<DjiSrtVideoLogModel> djiSrtVideoLogModels = new List<DjiSrtVideoLogModel>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(@"H:\Code\UavLogTool\UavLogToolTest\Data\DJI_0012.SRT"))
            {
                int lineCount = 0;
                int id = 0;
                while ((line = file.ReadLine()) != null)
                {
                    int idTemp;
                    if (int.TryParse(line, out idTemp))
                    {
                        djiSrtVideoLogModel = new DjiSrtVideoLogModel();
                        id = idTemp;
                        lineCount++;
                    }
                    if (id >= 1)
                    {
                        switch (lineCount)
                        {
                            case 1:
                                djiSrtVideoLogModel.Id = id;
                                lineCount++;
                                break;
                            case 2:
                                djiSrtVideoLogModel = SrtConverter.GetTimeStamp(line, ref djiSrtVideoLogModel);
                                lineCount++;
                                break;
                            case 3:
                                djiSrtVideoLogModel = SrtConverter.GetHomePointAndDateTime(line, ref djiSrtVideoLogModel);
                                lineCount++;
                                break;
                            case 4:
                                djiSrtVideoLogModel = SrtConverter.GetGpsPointAndBarometer(line, ref djiSrtVideoLogModel);
                                lineCount++;
                                break;
                            case 5:
                                djiSrtVideoLogModel = SrtConverter.GetCameraInfo(line, ref djiSrtVideoLogModel);
                                djiSrtVideoLogModels.Add(djiSrtVideoLogModel);
                                lineCount = 0;
                                break;
                            default:
                                break;
                        }
                    }
                    counter++;
                }
            }
            
            var filePath = Path.Combine(@"C:\Temp\", "djiSrtVideo.csv");

            var noko = SrtConverter.ConvertSrtToUavLog(djiSrtVideoLogModels);
            var csvVideoLogs = CsvUtilities.ToCsv(",", djiSrtVideoLogModels.ToArray());
            var saved = CsvUtilities.SaveCsvTofile(filePath, csvVideoLogs);

        }

        [Fact]
        public void srtTimeStamp()
        {
            var timeStmapTest = "00:09:05,000 --> 00:09:06,000";
            var timeStamps = timeStmapTest.Split(" --> ");
            foreach (var item in timeStamps)
            {
                var nokoTid = Helpers.GetTimeSpan(item);
            }

        }
        [Fact]
        public void srtHomeDateTime()
        {
            var line = "HOME(9.1534,59.4047) 2020.03.06 17:07:31";

            var firstSpaceIndex = line.IndexOf(" ");
            var firstString = line.Substring(0, firstSpaceIndex); // HOME(9.1534,59.4047)
            var secondString = line.Substring(firstSpaceIndex + 1); // 2020.03.06 17:07:31

            var homeLatLong = Regex.Match(firstString, @"\(([^)]*)\)").Groups[1].Value;
            var dateTimeLog = Helpers.GetDateTime(secondString);

            var noko = homeLatLong;

        }
        [Fact]
        public void srtBarometer()
        {
            var line = "GPS(9.1604,59.4049,19) BAROMETER:50.1";

            var firstSpaceIndex = line.IndexOf(" ");
            var firstString = line.Substring(0, firstSpaceIndex); // GPS(9.1604,59.4049,19)
            var secondString = line.Substring(firstSpaceIndex + 1); // BAROMETER:50.1

            var gpsLatLong = Regex.Match(firstString, @"\(([^)]*)\)").Groups[1].Value;

            var index = secondString.IndexOf(":");
            var barometer = secondString.Substring(index + 1); // BAROMETER:50.1

            var noko = gpsLatLong;
        }
        [Fact]
        public void srtCameraInfo()
        {
            var line = "ISO:400 Shutter:40 EV: Fnum:F2.8";

            var lines = line.Split(" ");
            var Iso = GetValue(lines[0]);
            var Shutter = GetValue(lines[1]);
            var EV = GetValue(lines[2]);
            var Fnum = GetValue(lines[3]);
        }


        private static string GetValue(string line)
        {
            var index = line.IndexOf(":");
            var value = line.Substring(index + 1);
            return value;
        }
    }
}
