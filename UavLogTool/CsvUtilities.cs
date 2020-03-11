using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UavLogTool.Models;

namespace UavLogTool
{
    public class CsvUtilities
    {

        public static Dictionary<string, int> GetDjiHeaderDictionary(string[] headres)
        {
            var djiLogs = UavLog.GetDJIMappingProperty();
            var headerDictionary = new Dictionary<string, int>();
            for (var i = 0; i < headres.Count(); i++)
            {

                foreach (var djilog in djiLogs)
                {
                    if (headres[i] == djilog.Value)
                    {
                        headerDictionary.Add(djilog.Key, i);
                    }

                }
            }
            return headerDictionary;
        }

        public static Dictionary<string, int> GetHeaderDictionary(string[] headres)
        {
            var headerDictionary = new Dictionary<string, int>();
            for (var i = 0; i < headres.Count(); i++)
            {

                headerDictionary.Add(headres[i], i);
            }
            return headerDictionary;
        }

        public static List<UavLog> GetUavLogFromDjiCsv(TextFieldParser csvParser)
        {
            var uavLogs = new List<UavLog>();
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            //Processing row
            string[] headers = csvParser.ReadFields();
            var djiHeaderDictionary = CsvUtilities.GetDjiHeaderDictionary(headers);


            if (djiHeaderDictionary.Any())
            {
                int rowNumber = 1;
                while (csvParser.PeekChars(1) != null)
                {

                    rowNumber++;
                    string[] fields = csvParser.ReadFields();
                    var index = djiHeaderDictionary["VideoRecordTime"];

                    var noko = fields[index];

                    if (fields.Length > index && !string.IsNullOrEmpty(fields[index]) && fields[index] != "0")
                    {
                        uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary, rowNumber));
                    }
                }
            }
            return uavLogs;
        }
        public static List<UavLog> GetUavLogFromTextFile(TextFieldParser csvParser)
        {

            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            string[] headers = csvParser.ReadFields();
            var djiHeaderDictionary = CsvUtilities.GetHeaderDictionary(headers);

            var uavLogs = new List<UavLog>();

            if (djiHeaderDictionary.Any())
            {
                int rowNumber = 1;
                while (csvParser.PeekChars(1) != null)
                {
                    rowNumber++;
                    string[] fields = csvParser.ReadFields();
                    uavLogs.Add(GetUavLog(fields, djiHeaderDictionary, rowNumber));
                }
            }
            return uavLogs;
        }
        public static UavLog GetUavLog(string[] fields, Dictionary<string, int> headers, int rowNumber)
        {
            var uavLog = new UavLog();
            var djiLog = UavLog.GetDJIMappingProperty();


            //uavLog.RowNumber = rowNumber;
            uavLog.FlyTime = fields[headers["FlyTime"]];
            uavLog.UavLatititud = fields[headers["UavLatititud"]];
            uavLog.UavLongitud = fields[headers["UavLongitud"]];
            uavLog.VideoRecordTime = fields[headers["VideoRecordTime"]];
            var pointDateTime = fields[headers["DateTime"]];

            DateTime dateTime;
            var formatStrings = new string[] { "yyyy/MM/dd HH:mm:ss.fff", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd", "mm:ss.f", "MM-dd-yyyy hh:mm:ss.fff tt", "dd-MM-yyyy hh:mm:ss.fff tt", "dd/MM/yyyy hh:mm:ss.fff" };
            CultureInfo enUS = new CultureInfo("en-US");

            if (DateTime.TryParseExact(pointDateTime, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                uavLog.DateTime = dateTime;  
            
            return uavLog;
        }





        //public static string ToCsv<UavLog>(string separator, IEnumerable<UavLog> objectlist)
        public static string[] ToCsv(string separator, IEnumerable<object> objectlist)
        {
            Type t = objectlist.FirstOrDefault().GetType();
            //FieldInfo[] fields = t.GetFields();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            var headersName = fields.Select(h => h.Name).ToArray();
            var headers = new List<string>();
            foreach (var name in headersName)
            {
                if (name.Contains(">"))
                {
                    var headerName = name.Substring(1, name.LastIndexOf(">", StringComparison.Ordinal) - 1);
                    headers.Add(headerName);
                }
            }
            string header = String.Join(separator, headers);

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);
            var lines = new List<string> { header };
            foreach (var o in objectlist)
            {
                var objectLine = ToCsvFields(separator, fields, o);
                lines.Add(objectLine);
                csvdata.AppendLine(ToCsvFields(separator, fields, o));
            }
            return lines.ToArray();
        }

        public static string ToCsvFields(string separator, FieldInfo[] fields, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in fields)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                {
                    if (x is DateTime)
                    {
                        var dateTime = (DateTime)x;

                        //linie.Append(dateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                        linie.Append(dateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt"));
                    }
                    else
                    {
                        linie.Append(x.ToString());
                    }

                }
            }

            return linie.ToString();
        }

        public static bool SaveCsvTofile(string path, string[] csvString)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    foreach (string line in csvString)
                        file.WriteLine(line);
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static void ConvertCalssToCsv(VideoInfoModel[] objects, string path)
        {
            //using (TextWriter writer = new StreamWriter(path))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //    foreach (var value in objects)
            //    {
            //        csv.WriteRecord(value);
            //    }



            //}
            //var csv = new CsvWriter(writer);

            //csv.Configuration.Encoding = Encoding.UTF8;
            //foreach (var value in valuess)
            //{
            //    csv.WriteRecord(value);
            //}
            //writer.Close();
        }
    }
}
