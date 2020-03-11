using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using UavLogTool.Models;

namespace UavLogTool
{
    public class Helpers
    {
        public static UavLog GetUavLogFromVideoTimeStamp(TimeSpan imageTimeStamp, List<UavLog> uavLogs)
        {
            UavLog uavLog = null;
            var sortedUavLogs = FilterUavlogAndSort(uavLogs);

            if (imageTimeStamp != TimeSpan.Zero)
            {
                try
                {
                    var videoLenght = GetVideoLenghtInMilliseconds(sortedUavLogs);
                    var videoLengh = ConvertMilisecondsToHMSmm(videoLenght);
                    var milli = imageTimeStamp.TotalMilliseconds;
                    var videoLengh2 = ConvertMilisecondsToHMSmm(milli);

                    if (imageTimeStamp.TotalMilliseconds < videoLenght)
                    {
                        var firstVideoLog = GetfirstLog(uavLogs);

                        var photoTimeInVideo = firstVideoLog.DateTime + imageTimeStamp;

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
            else
            {
                uavLog = sortedUavLogs.FirstOrDefault();
            }
            return uavLog;
        }

        public static TimeSpan GetTimeSpan(string timeStamp)
        {
            var formatStrings = new string[] { @"mm\:ss\.ff", @"mm\:ss\:ff", @"mm\:ss", @"hh\:mm\:ss\,fff" }; //00:09:05,000
            TimeSpan imageTimeStamp = TimeSpan.Zero;
            if (TimeSpan.TryParseExact(timeStamp, formatStrings, null, out imageTimeStamp))
                return imageTimeStamp;
            return imageTimeStamp;
        }
        public static DateTime GetDateTime(string dateTimeString)
        {
            var formatStrings = new string[] { "yyyy/MM/dd HH:mm:ss.fff", "yyyy.MM.dd HH:mm:ss", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd", "mm:ss.f", "MM-dd-yyyy hh:mm:ss.fff tt", "dd-MM-yyyy hh:mm:ss.fff tt", "dd/MM/yyyy hh:mm:ss.fff" };
            DateTime dateTime = DateTime.MinValue;
            DateTime.TryParseExact(dateTimeString, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

            return dateTime;
        }
        public static UavLog GetfirstLog(List<UavLog> uavLogs)
        {
            var maxyearLogs = FilterUavlogAndSort(uavLogs);
            var uavLog = maxyearLogs.FirstOrDefault();
            return uavLog;
        }
        public static List<UavLog> FilterUavlogAndSort(List<UavLog> uavLogs)
        {

            uavLogs.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));


            UavLog uavLogFirst = uavLogs.FirstOrDefault();

            var years = uavLogs.Select(l => l.DateTime.Year);
            var distinct = years.Distinct();

            var dictionary = new Dictionary<int, int>();
            foreach (var year in distinct)
            {
                var count = uavLogs.Count(l => l.DateTime.Year == year);
                dictionary.Add(year, count);
            }
            var max = dictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            var maxyearLogs = uavLogs.Where(l => l.DateTime.Year == max).ToList();
            maxyearLogs.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            return maxyearLogs;
        }

        public static Dictionary<int, List<UavLog>> SplitVideosFromUavLog(List<UavLog> uavLogs)
        {
            var videoLogs = new Dictionary<int, List<UavLog>>();

            var uavlogsTemp = new List<UavLog>();
            var uavlogs = new List<UavLog>();
            int videoNumber = 1;

            int actualInt;

            var previous = uavLogs.FirstOrDefault().VideoRecordTime;
            foreach (var log in uavLogs)
            {

                var actual = log.VideoRecordTime;

                var previousInt = int.Parse(previous);
                actualInt = int.Parse(actual);
                if (previous != actual)
                {
                    if (previousInt > actualInt)
                    {
                        uavlogs = FilterUavlogAndSort(uavlogsTemp);
                        videoLogs.Add(videoNumber, uavlogs);
                        videoNumber++;
                        uavlogsTemp = new List<UavLog>();
                    }
                    previous = actual;

                }
                log.VideoNumber = videoNumber;
                uavlogsTemp.Add(log);
            }

            uavlogs = FilterUavlogAndSort(uavlogsTemp);
            videoLogs.Add(videoNumber, uavlogs);

            return videoLogs;
        }

        public static double GetVideoLenghtInMilliseconds(List<UavLog> uavLogs)
        {
            var sortUavList = uavLogs;
            sortUavList.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            var dateTimeFirst = sortUavList.First().DateTime;
            //var dateTimeFirst = uavLogs.Select(log => log.DateTime).Min();
            var dateTimeLast = sortUavList.Last().DateTime;
            //var dateTimeLast =  uavLogs.Select(log=>log.DateTime).Max();
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
                var latitudSec = Math.Round(latSecPart, 4);
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

        public static List<VideoInfoModel> GetVideoInfoModels(List<UavLog> videoLogs)
        {
            var videoInfoModels = new List<VideoInfoModel>();
            var videoDuration = GetVideoLenghtInMilliseconds(videoLogs);
            var time = ConvertMilisecondsToHMSmm(videoDuration);
            var firstVideoLog = videoLogs.First();
            var lastVideoLog = videoLogs.Last();
            var filename = $"{videoLogs.FirstOrDefault()?.DateTime.ToString("_yyyy_MM_dd_HH-mm-ss")}_{time}.csv";

            videoInfoModels.Add(new VideoInfoModel()
            {
                DateTime = firstVideoLog.DateTime,
                Duration = time,
                StartLatitud = firstVideoLog.UavLatititud,
                StartLongitud = firstVideoLog.UavLongitud,
                EndLatitud = lastVideoLog.UavLatititud,
                EndLongitud = lastVideoLog.UavLongitud,
                FileName = filename
            });


            return videoInfoModels;
        }
        public static List<VideoInfoModel> GetVideoInfoModels(Dictionary<int, List<UavLog>> videosLogs)
        {
            var videoInfoModels = new List<VideoInfoModel>();

            foreach (var videosLog in videosLogs)
            {
                var videoDuration = GetVideoLenghtInMilliseconds(videosLog.Value);
                var time = ConvertMilisecondsToHMSmm(videoDuration);
                var firstVideoLog = videosLog.Value.First();
                var lastVideoLog = videosLog.Value.Last();
                var filename = $"{videosLog.Value.FirstOrDefault()?.DateTime.ToString("_yyyy_MM_dd_HH-mm-ss")}_{videosLog.Key}.csv";

                videoInfoModels.Add(new VideoInfoModel()
                {
                    DateTime = firstVideoLog.DateTime,
                    Duration = time,
                    StartLatitud = firstVideoLog.UavLatititud,
                    StartLongitud = firstVideoLog.UavLongitud,
                    EndLatitud = lastVideoLog.UavLatititud,
                    EndLongitud = lastVideoLog.UavLongitud,
                    FileName = filename
                });
            }

            return videoInfoModels;
        }

        public static List<UavLog> TrimUavLogs(List<UavLog> logs, TimeSpan startTime, TimeSpan endTime)
        {
            var newUavLogs = new List<UavLog>();

            var videoLenght = GetVideoLenghtInMilliseconds(logs);
            var videoLengh = ConvertMilisecondsToHMSmm(videoLenght);
            var milli = endTime.TotalMilliseconds;
            var videoLengh2 = ConvertMilisecondsToHMSmm(milli);

            if (endTime.TotalMilliseconds < videoLenght && startTime < endTime)
            {
                var startlLog = GetUavLogFromVideoTimeStamp(startTime, logs);
                var endLog = GetUavLogFromVideoTimeStamp(endTime, logs);

                foreach (var uavLog in logs)
                {
                    if (uavLog.DateTime >= startlLog.DateTime && uavLog.DateTime <= endLog.DateTime)
                    {
                        newUavLogs.Add(uavLog);
                    }
                }
            }
            return newUavLogs;
        }
    }
}
