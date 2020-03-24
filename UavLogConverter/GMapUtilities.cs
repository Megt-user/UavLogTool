using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeoCoordinatePortable;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using UavLogTool.Models;
using UavLog = UavLogConverter.Models.UavLog;

namespace UavLogConverter
{
    public class GMapUtilities
    {
        private static object markersOverlay;

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

        public static List<UavLog> SplitUavLogByDistance(List<UavLog> uavLogs, int distance)
        {
            var uavLogsNew = new List<UavLog>();
            for (int index = 0; index <= uavLogs.Count - 1; index++)
            {
                if (index == 0)
                {
                    uavLogsNew.Add(uavLogs.FirstOrDefault());
                    continue;
                }

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
            uavLogsNew.Add(uavLogs.Last());

            return uavLogsNew;
        }

        public static GMapControl AddlatLngToGMapControl(ref GMapControl gMapControl, UavLog uavLog)
        {
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            double lat = Convert.ToDouble(uavLog.UavLatititud);
            double lng = Convert.ToDouble(uavLog.UavLongitud);
            PointLatLng pointLatLng = new PointLatLng(lat, lng);

            //Create a red marker
            GMarkerGoogle marker1 = new GMarkerGoogle(pointLatLng, GMarkerGoogleType.green);

            //Add a marker on the overlay
            markersOverlay.Markers.Add(marker1);

            //Add the overlay on the gMapControl1(Map)
            gMapControl.Overlays.Add(markersOverlay);
            return gMapControl;
        }

        public static GMapControl ConfigureGoogleMap(ref GMapControl gMapControl)
        {
            gMapControl.DragButton = MouseButtons.Left;
            gMapControl.MapProvider = GMapProviders.GoogleMap;
            return gMapControl;
        }

        public static GMapControl FixZoomToMarker(ref GMapControl gMapControl)
        {
            gMapControl.MinZoom = 5;
            gMapControl.MaxZoom = 100;
            double zoomAtual = gMapControl.Zoom;
            gMapControl.Zoom = zoomAtual + 1;
            gMapControl.Zoom = zoomAtual;
            return gMapControl;
        }

        public static Dictionary<int, List<PointLatLng>> GetPointLatlngsFromUavLogs(Dictionary<int, List<UavLog>> uavLogs)
        {
            var pointsDictionary = new Dictionary<int, List<PointLatLng>>();
            foreach (var uavLog in uavLogs)
            {
                var newUavlog = GMapUtilities.SplitUavLogByDistance(uavLog.Value, 80);
                pointsDictionary.Add(uavLog.Key, GetPointLatlngsFromUavLogs(newUavlog));
            }
            return pointsDictionary;
        }

        public static List<PointLatLng> GetPointLatlngsFromUavLogs(List<UavLog> uavLogs)
        {
            var pointLatLngs = new List<PointLatLng>();
            for (int index = 0; index < uavLogs.Count - 1; index++)
            {

                double lat = Convert.ToDouble(uavLogs[index].UavLatititud);
                double lng = Convert.ToDouble(uavLogs[index].UavLongitud);

                PointLatLng pointLatLng = new PointLatLng(lat, lng);
                pointLatLngs.Add(pointLatLng);

            }

            return pointLatLngs;
        }

        public static GMapRoute AddPointsLatLgnToRout(List<PointLatLng> pointLatLngs, int index)
        {
            GMapRoute route = new GMapRoute(pointLatLngs, "sample route")
            {
                Stroke = new Pen(new Color())
                {
                    Color = GetRoutColor(index),
                    Width = 4,
                    DashStyle = DashStyle.DashDot,
                    StartCap = LineCap.NoAnchor,
                    EndCap = LineCap.ArrowAnchor,
                    LineJoin = LineJoin.Round
                }
            };

            return route;
        }

        private static Color GetRoutColor(int index)
        {
            switch (index)
            {
                case 0:
                    return Color.Blue;
                    break;
                case 1:
                    return Color.Green;
                    break;
                case 2:
                    return Color.Yellow;
                    break;
                case 3:
                    return Color.LightBlue;
                    break;
                case 4:
                    return Color.Orange;
                    break;
                case 5:
                    return Color.Pink;
                    break;
                case 6:
                    return Color.Red;
                default:
                    return Color.Green;
                    break;
            }
        }

        public static GMarkerGoogleType GetColor(int index)
        {
            switch (index)
            {
                case 0:
                    return GMarkerGoogleType.blue;
                    break;
                case 1:
                    return GMarkerGoogleType.green;
                    break;
                case 2:
                    return GMarkerGoogleType.yellow;
                    break;
                case 3:
                    return GMarkerGoogleType.lightblue;
                    break;
                case 4:
                    return GMarkerGoogleType.orange;
                    break;
                case 5:
                    return GMarkerGoogleType.pink;
                    break;
                case 6:
                    return GMarkerGoogleType.red;
                default:
                    return GMarkerGoogleType.green;
                    break;
            }
        }


    }
}
