using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UavLogTool.Models
{
    public class CoordinatesModel
    {
        public string LatitudDirection { get; set; }
        public int LatitudDegrees { get; set; }
        public int LatitudMinutes { get; set; }
        public double LatitudSeconds { get; set; }
        public string LongitudDirection { get; set; }
        public int LongitudDegrees { get; set; }
        public int LongitudMinutes { get; set; }
        public double LongitudSeconds { get; set; }
        public double Altitud { get; set; }

    }
}
