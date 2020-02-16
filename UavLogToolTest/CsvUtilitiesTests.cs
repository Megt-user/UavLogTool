using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using UavLogTool;
using UavLogTool.Models;
using Xunit;

namespace UavLogToolTest
{
    public class CsvUtilitiesTests
    {
        [Fact]
        public void Test1()
        {
            var path = @"Data\DJIFlightRecord_2020-01-22_[13-25-55]-TxtLogToCsv.csv"; // "," dateTime: "2020/01/22 12:25:55.734"
            //var path = @"Data\DJIFlightRecord_2020-01-22_13-25-55-verbose.csv"; // ";"  dateTime:"28:52.3"
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

               
                
               

                var dictionarylog = Helpers.SplitVideosFromUavLog(uavLogs);
                var video1LenghInMilliseconds = Helpers.GetVideoLenghtInSeconds(dictionarylog[2]);
                var time = Helpers.ConvertMilisecondsToHMSmm(video1LenghInMilliseconds);
                var photolog = Helpers.GetPositionScreemshotPostionFromVideo("03:56:22", dictionarylog[2]);
                foreach (var videologs in dictionarylog)
                {
                    var csvVideoLogs = CsvUtilities.ToCsv(",", videologs.Value);
                    var filename = $"{videologs.Value.FirstOrDefault().DateTime.ToString("yyMMdd")}_{videologs.Key}.csv";
                    var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", filename), csvVideoLogs);
                }
                //var csvString = CsvUtilities.ToCsv(",", uavLogs);
                //var saved = CsvUtilities.SaveCsvTofile(@"C:\Temp\WriteLines2.csv", csvString);
            }
        }
    }
}
