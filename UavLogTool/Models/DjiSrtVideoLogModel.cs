using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UavLogTool.Models
{
    public class DjiSrtVideoLogModel
    {
        public int Id { get; set; }
        public TimeSpan StartLog { get; set; }
        public TimeSpan? EndLog { get; set; }
        public string HomeLatitud { get; set; }
        public string HomeLongitud { get; set; }
        public DateTime DateTimeLog { get; set; }
        public string UavLatitud { get; set; }
        public string UavLongitud { get; set; }
        public double? UavAltitud { get; set; }
        public double? Barometer { get; set; }
        public int? ISO { get; set; }
        public string Shutter { get; set; }
        public string Ev { get; set; }
        public string Fnum { get; set; }



    }
}
