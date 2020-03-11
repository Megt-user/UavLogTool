using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UavLogTool.Models;

namespace UavLogTool
{
    public class SrtConverter
    {
        public static DjiSrtVideoLogModel GetTimeStamp(string timeStamp, ref DjiSrtVideoLogModel djiSrtVideoLogModel)
        {
            //"00:09:05,000 --> 00:09:06,000";
            var timeStamps = timeStamp.Split(" --> ");

            djiSrtVideoLogModel.StartLog = Helpers.GetTimeSpan(timeStamps[0]);
            djiSrtVideoLogModel.EndLog = Helpers.GetTimeSpan(timeStamps[1]);

            return djiSrtVideoLogModel;
        }

        public static DjiSrtVideoLogModel GetHomePointAndDateTime(string homePointAndDateTime, ref DjiSrtVideoLogModel djiSrtVideoLogModel)
        {
            //"HOME(9.1534,59.4047) 2020.03.06 17:07:31";

            var firstSpaceIndex = homePointAndDateTime.IndexOf(" ");
            var firstString = homePointAndDateTime.Substring(0, firstSpaceIndex); // HOME(9.1534,59.4047)
            var secondString = homePointAndDateTime.Substring(firstSpaceIndex + 1); // 2020.03.06 17:07:31

            var homeLatLong = Regex.Match(firstString, @"\(([^)]*)\)").Groups[1].Value;
            var longitudLatitud = homeLatLong.Split(",");
            djiSrtVideoLogModel.HomeLongitud = longitudLatitud[0];
            djiSrtVideoLogModel.HomeLatitud = longitudLatitud[1];
            djiSrtVideoLogModel.DateTimeLog = Helpers.GetDateTime(secondString);

            return djiSrtVideoLogModel;
        }

        public static DjiSrtVideoLogModel GetGpsPointAndBarometer(string GpsPointAndBarometer, ref DjiSrtVideoLogModel djiSrtVideoLogModel)
        {
            //"GPS(9.1604,59.4049,19) BAROMETER:50.1";

            var firstSpaceIndex = GpsPointAndBarometer.IndexOf(" ");
            var firstString = GpsPointAndBarometer.Substring(0, firstSpaceIndex); // GPS(9.1604,59.4049,19)
            var secondString = GpsPointAndBarometer.Substring(firstSpaceIndex + 1); // BAROMETER:50.1

            var gpsLatLong = Regex.Match(firstString, @"\(([^)]*)\)").Groups[1].Value;

            var longitudLatitud = gpsLatLong.Split(",");
            djiSrtVideoLogModel.UavLongitud = longitudLatitud[0];
            djiSrtVideoLogModel.UavLatitud = longitudLatitud[1];

            double altitud = 0;
            double.TryParse(GetValue(secondString), out altitud);
            djiSrtVideoLogModel.UavAltitud = altitud;

            double barometer = 0;
            double.TryParse(GetValue(secondString), out barometer);
            djiSrtVideoLogModel.Barometer = barometer;

            return djiSrtVideoLogModel;
        }

        public static DjiSrtVideoLogModel GetCameraInfo(string cameraInfo, ref DjiSrtVideoLogModel djiSrtVideoLogModel)
        {
            //"ISO:400 Shutter:40 EV: Fnum:F2.8";

            var lines = cameraInfo.Split(" ");
            var isoString = GetValue(lines[0]);
            var shutter = GetValue(lines[1]);
            var eV = GetValue(lines[2]);
            var fNum = GetValue(lines[3]);

            int iso = 0;
            int.TryParse(isoString, out iso);
            djiSrtVideoLogModel.ISO = iso;

            djiSrtVideoLogModel.Shutter = shutter;
            djiSrtVideoLogModel.Ev = eV;
            djiSrtVideoLogModel.Fnum = fNum;

            return djiSrtVideoLogModel;
        }

        public static List<UavLog> ConvertSrtToUavLog(List<DjiSrtVideoLogModel> djiSrtVideoLogModels)
        {
            List<UavLog> uavLogs = new List<UavLog>();

            foreach (var djiSrt in djiSrtVideoLogModels)
            {
                var uavLog = new UavLog()
                {
                    UavLatititud = djiSrt.UavLatitud,
                    UavLongitud = djiSrt.UavLongitud,
                    VideoRecordTime = Helpers.ConvertMilisecondsToHMSmm(djiSrt.StartLog.TotalMilliseconds),
                    FlyTime = djiSrt.DateTimeLog.Minute.ToString(),
                    DateTime = djiSrt.DateTimeLog
                };
                uavLogs.Add(uavLog);
            }

            return uavLogs;
        }
        private static string GetValue(string line)
        {
            var index = line.IndexOf(":");
            var value = line.Substring(index + 1);
            return value;
        }
    }
}
