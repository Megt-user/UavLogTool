using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                csvParser.CommentTokens = new string[] {"#"};
                csvParser.SetDelimiters(new string[] {","});
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
                    while (csvParser.PeekChars(1) != null)
                    {
                        string[] fields = csvParser.ReadFields();
                        var index = djiHeaderDictionary["VideoRecordTime"];

                        var noko = fields[index];
                        if (fields.Length > index && fields[index] != "0")
                        {
                            uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary));
                        }
                    }
                }

                string csv = String.Join(",", uavLogs);


                videosLogs = Helpers.SplitVideosFromUavLog(uavLogs);
            }

            var videoInfoModels = Helpers.GetVideoInfoModels(videosLogs);

            
        }
    }
}
