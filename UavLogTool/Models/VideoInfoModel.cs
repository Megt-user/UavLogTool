using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UavLogTool.Models
{
    public class VideoInfoModel
    {
        public DateTime DateTime { get; set; }
        public String Duration { get; set; }
        public string StartLatitud { get; set; }
        public string StartLongitud { get; set; }
        public string FileName { get; set; }

    }
}
