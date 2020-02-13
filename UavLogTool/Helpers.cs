using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UavLogTool.Models;

namespace UavLogTool
{
    public class Helpers
    {
        public static UavLog GetPositionScreemshotPostionFromVideo(string photoTimeStamp, List<UavLog> uavLogs)
        {
            UavLog uavLog = null;
            var formatStrings = new string[] { @"mm\:ss\.ff", @"mm\:ss\:ff", @"mm\:ss" };
            TimeSpan imageTimeStamp;
            if (TimeSpan.TryParseExact(photoTimeStamp, formatStrings, null, out imageTimeStamp))
            {
                try
                {
                    var videoLenght = GetVideoLenghtInSeconds(uavLogs);
                    if (imageTimeStamp.TotalMilliseconds < videoLenght)
                    {
                        var firstVideoLog = uavLogs.Select(l => l.DateTime).Min();
                        var photoTimeInVideo = firstVideoLog + imageTimeStamp;

                        //https://stackoverflow.com/a/7016646
                        var nearestDiff = uavLogs.Min(log => Math.Abs((log.DateTime - photoTimeInVideo).Ticks));
                        var nearest = uavLogs.First(log => Math.Abs((log.DateTime - photoTimeInVideo).Ticks) == nearestDiff);

                        //https://stackoverflow.com/a/4075380
                        var uavLogIndex = uavLogs.Select((value, index) => new { value, index })
                            .First(log => Math.Abs((log.value.DateTime - photoTimeInVideo).Ticks) == nearestDiff);

                        uavLog = uavLogIndex.value;
                        var LatLong = $"{nearest.UavLatititud},{nearest.UavLongitud}"; //"59.409424,9.132055"
                        var dif1 = ConvertMilisecondsToHMSmm(TimeSpan.FromTicks(nearestDiff).TotalMilliseconds);
                    }
                }
                catch
                {

                    //return null;
                }
            }
            return uavLog;
        }


        public static Dictionary<int, List<UavLog>> SplitVideosFromUavLog(List<UavLog> uavLogs)
        {
            var videoLogs = new Dictionary<int, List<UavLog>>();

            var uavlogsTemp = new List<UavLog>();
            string previus = "1";
            bool flag = false;
            int videoNumber = 1;
            string logName = "VideoRecordTime";

            foreach (var log in uavLogs)
            {
                var actual = log.VideoRecordTime;
                if (previus == "1" && previus != actual && !flag)
                {
                    flag = true;
                }


                if (flag && actual == "1")
                {
                    videoLogs.Add(videoNumber, uavlogsTemp);
                    videoNumber++;
                    previus = actual;
                    flag = false;
                    uavlogsTemp = new List<UavLog>();
                }
                log.VideoNumber = videoNumber;
                uavlogsTemp.Add(log);
            }
            videoLogs.Add(videoNumber, uavlogsTemp);

            return videoLogs;
        }

        public static double GetVideoLenghtInSeconds(List<UavLog> uavLogs)
        {

            var dateTimeFirst = uavLogs.First().DateTime;
            var dateTimeLast = uavLogs.Last().DateTime;
            var videoLenght = (dateTimeLast - dateTimeFirst).TotalMilliseconds;
            return videoLenght;
        }

        public static string ConvertMilisecondsToHMSmm(double milliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(milliseconds);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

            return answer;
        }

        public static CoordinatesModel ConvertLatToDMS(double? latitud, double? longitud, double? altitud = null)
        {
            if (latitud.HasValue && longitud.HasValue)
            {
                //https://stackoverflow.com/a/27996566

                string latitudDir = (latitud.Value >= 0 ? "N" : "S");
                latitud = Math.Abs(latitud.Value);
                double latMinPart = ((latitud.Value - Math.Truncate(latitud.Value) / 1) * 60);
                double latSecPart = ((latMinPart - Math.Truncate(latMinPart) / 1) * 60);

                string lonDir = (longitud.Value >= 0 ? "E" : "W");
                longitud = Math.Abs(longitud.Value);
                double lonMinPart = ((longitud.Value - Math.Truncate(longitud.Value) / 1) * 60);
                double lonSecPart = ((lonMinPart - Math.Truncate(lonMinPart) / 1) * 60);

                var latitudDeg = (int)Math.Truncate(latitud.Value);
                var latitudMin = (int)Math.Truncate(latMinPart);
                var latitudSec = Math.Round(latSecPart, 3);
                var longitudDeg = (int)Math.Truncate(longitud.Value);
                var longitudMin = (int)Math.Truncate(lonMinPart);
                var longitudSec = Math.Round(lonSecPart, 3);


                var coordinates = new CoordinatesModel()
                {
                    LatitudDirection = latitudDir,
                    LatitudDegrees = latitudDeg,
                    LatitudMinutes = latitudMin,
                    LatitudSeconds = latitudSec,
                    LongitudDirection = lonDir,
                    LongitudDegrees = longitudDeg,
                    LongitudMinutes = longitudMin,
                    LongitudSeconds = longitudSec,
                    Altitud = altitud ?? 0
                };
                return coordinates;
            }
            return null;
        }
    }
}
