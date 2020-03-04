using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic.FileIO;
using UavLogTool;
using UavLogTool.Models;
using Xunit;

namespace UavLogToolTest
{
    public class HelpersTests
    {
        [Fact]
        public void TestDecimalToDMS()
        {

            var coordcinatesDictionary = Helpers.ConvertLatToDMS(59.409581, 9.131822, 168.4);

            double lat = 59.409581;
            double lon = -45.197069205344;
        }

        [Fact]
        public void TestDictionaryToVideoInfoModel()
        {
            var path = @"Data\DJIFlightRecord_2020-01-22_[13-25-55]-TxtLogToCsv.csv"; // "," dateTime: "2020/01/22 12:25:55.734"
            //var path = @"Data\DJIFlightRecord_2020-01-22_13-25-55-verbose.csv"; // ";"  dateTime:"28:52.3"
            Dictionary<int, List<UavLog>> videosLogs;
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                //// Skip the row with the column names
                //csvParser.ReadLine();


                //var noko = File.ReadAllLines(path);



                //Processing row
                string[] headers = csvParser.ReadFields();
                var djiHeaderDictionary = CsvUtilities.GetDjiHeaderDictionary(headers);


                //foreach (string field in fields)
                //{
                var uavLogs = new List<UavLog>();

                if (djiHeaderDictionary.Any())
                {
                    int rowNumber = 1;
                    while (csvParser.PeekChars(1) != null)
                    {
                        rowNumber++;
                        string[] fields = csvParser.ReadFields();
                        var index = djiHeaderDictionary["VideoRecordTime"];

                        var noko = fields[index];
                        if (fields.Length > index && fields[index] != "0")
                        {
                            uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary, rowNumber));
                        }
                    }
                }

                string csv = String.Join(",", uavLogs);


                videosLogs = Helpers.SplitVideosFromUavLog(uavLogs);
            }

            var videoInfoModels = Helpers.GetVideoInfoModels(videosLogs);


        }

        [Fact]
        public void TrimUavLogs()
        {
            List<UavLog> uavLogs = new List<UavLog>();
            var path = @"Data\200122_12-28-09_1.csv"; // "," dateTime: "2020/01/22 12:25:55.734"

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string[] headers = csvParser.ReadFields();
                var djiHeaderDictionary = CsvUtilities.GetHeaderDictionary(headers);



                if (djiHeaderDictionary.Any())
                {
                    int rowNumber = 1;
                    while (csvParser.PeekChars(1) != null)
                    {
                        rowNumber++;
                        string[] fields = csvParser.ReadFields();
                        uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary, rowNumber));
                    }
                }

                string csv = String.Join(",", uavLogs);
            }
            var sortUavList = uavLogs;
            sortUavList.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            //var noko = Helpers.GetfirstLog(uavLogs);

            //var newUavlogs = Helpers.TrimUavLogs(sortUavList, "01:36:22", "02:00:22");
            TimeSpan startTimeSpan = Helpers.GetTimeSpan("00:01:00.150");
            TimeSpan endTimeStamp = Helpers.GetTimeSpan("00:01:36.150");

            var newUavlogs = Helpers.TrimUavLogs(sortUavList, startTimeSpan, endTimeStamp);


            var filePath = Path.Combine(@"C:\Temp\", "sort_test.csv");
            var csvVideoLogs = CsvUtilities.ToCsv(",", newUavlogs);
            var saved = CsvUtilities.SaveCsvTofile(filePath, csvVideoLogs);
        }
    }
}
