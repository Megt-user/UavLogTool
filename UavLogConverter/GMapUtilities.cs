using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoCoordinatePortable;
using UavLogTool.Models;
using UavLog = UavLogConverter.Models.UavLog;

namespace UavLogConverter
{
    public class GMapUtilities
    {
        public static double GetCoordinateDistance(string latitud, string longitud, string latitud2, string longitud2)
        {
            
            double lat = Convert.ToDouble(latitud);
            double lng = Convert.ToDouble(longitud);
            double lat2 = Convert.ToDouble(latitud2);
            double lng2 = Convert.ToDouble(longitud2);
            var sCoord = new GeoCoordinate(lat, lng);
            var eCoord = new GeoCoordinate(lat2, lng2);

            var distance = sCoord.GetDistanceTo(eCoord);
            return distance;
        }

        public static List<UavLog> FilterUavLogByDistance(List<UavLog> uavLogs, int distance)
        {
            var uavLogsNew = new List<UavLog>();
            for (int index = 0; index < uavLogs.Count - 1; index++)
            {
                if (index == 0)
                {
                    uavLogsNew.Add(uavLogs.FirstOrDefault());
                }

                if (index == uavLogs.Count - 1)
                {
                    uavLogsNew.Add(uavLogs.LastOrDefault());
                }
                else
                {
                    //Create a new overlay 
                    for (int index2 = index; index2 < uavLogs.Count - 1; index2++)
                    {
                        var coordinateDistance = GetCoordinateDistance(uavLogs[index].UavLatititud, uavLogs[index].UavLongitud, uavLogs[index2].UavLatititud, uavLogs[index2].UavLongitud);
                        if (coordinateDistance >= distance)
                        {
                            uavLogsNew.Add(uavLogs[index2]);
                            index = index2;
                            break;
                        }
                    }
                }
            }

            return uavLogsNew;
        }
    }
}
