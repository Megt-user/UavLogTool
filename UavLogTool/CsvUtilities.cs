using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        public static UavLog GetUavLog(string[] fields, Dictionary<string, int> headers)
        {
            var uavLog = new UavLog();
            var djiLog = UavLog.GetDJIMappingProperty();


            uavLog.FlyTime = fields[headers["FlyTime"]];
            uavLog.UavLatititud = fields[headers["UavLatititud"]];
            uavLog.UavLongitud = fields[headers["UavLongitud"]];
            uavLog.VideoRecordTime = fields[headers["VideoRecordTime"]];
            var pointDateTime = fields[headers["DateTime"]];

            DateTime dateTime;
            var formatStrings = new string[] { "yyyy/MM/dd HH:mm:ss.fff", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd", "mm:ss.f" };
            CultureInfo enUS = new CultureInfo("en-US");

            if (DateTime.TryParseExact(pointDateTime, formatStrings, enUS, DateTimeStyles.None, out dateTime))
                uavLog.DateTime = dateTime;

            return uavLog;
        }


        //public static string ToCsv<UavLog>(string separator, IEnumerable<UavLog> objectlist)
        public static string[] ToCsv<UavLog>(string separator, IEnumerable<UavLog> objectlist)
        {
            Type t = typeof(UavLog);
            //FieldInfo[] fields = t.GetFields();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            string header = String.Join(separator, fields.Select(f => f.Name).ToArray());

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);
            List<string> lines = new List<string>();
            lines.Add(header);
            foreach (var o in objectlist)
            {
                var objectLine = ToCsvFields(separator, fields, o);
                lines.Add(objectLine);
                csvdata.AppendLine(ToCsvFields(separator, fields, o));

            }

            //return csvdata.ToString();
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
                        var dateTime =(DateTime) x;
                        linie.Append(dateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
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
    }
}
