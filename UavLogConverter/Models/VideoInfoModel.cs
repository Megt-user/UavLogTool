using System;

namespace UavLogConverter.Models
{
    public class VideoInfoModel
    {
        public DateTime DateTime { get; set; }
        public String Duration { get; set; }
        public string StartLatitud { get; set; }
        public string StartLongitud { get; set; }
        public string EndLatitud { get; set; }
        public string EndLongitud { get; set; }
        public string FileName { get; set; }

    }
}
