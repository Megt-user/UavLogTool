using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UavLogConverter;
using UavLogConverter.Models;
using Xunit;

namespace UavLogToolTest
{
    public class HelperConverterTest
    {

        [Fact]
        public void GetDistance()
        {
            List<UavLog> uavLogs = new List<UavLog>();
            List<UavLog> uavLogsNew = new List<UavLog>();
            var path = @"Data\200122_12-28-09_1.csv"; // "," dateTime: "2020/01/22 12:25:55.734"

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                uavLogs = CsvUtilities.GetUavLogFromTextFile(csvParser);
            }

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
                        var distance = GMapUtilities.GetCoordinateDistance(uavLogs[index].UavLatititud, uavLogs[index].UavLongitud, uavLogs[index2].UavLatititud, uavLogs[index2].UavLongitud);
                        if (distance >= 5)
                        {
                            uavLogsNew.Add(uavLogs[index2]);
                            index = index2;
                            break;
                        }
                    }
                }
            }
            //            
        }
    }
}
