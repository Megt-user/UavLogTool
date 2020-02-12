using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UavLogTool.Models
{
    public class UavLog
    {
        public string UavLatititud { get; set; }
        public string UavLongitud { get; set; }
        public string VideoRecordTime { get; set; }
        public string FlyTime { get; set; }
        public DateTime DateTime { get; set; }
        public int VideoNumber { get; set; }

        public static Dictionary<string, string> GetDJIMappingProperty()
        {
            var djiMappingModel = new Dictionary<string, string>() {
                {"UavLatititud","OSD.latitude" },
                {"UavLongitud","OSD.longitude" },
                {"FlyTime","OSD.flyTime [s]" },
                {"DateTime","CUSTOM.updateTime" },
                {"VideoRecordTime","CAMERA_INFO.videoRecordTime" }
            };

            return djiMappingModel;
        }
    }


}
