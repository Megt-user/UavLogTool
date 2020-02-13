using System;
using System.Collections.Generic;
using System.Text;
using UavLogTool;
using Xunit;

namespace UavLogToolTest
{
    public class HelpersTests
    {
        [Fact]
        public void TestDecimalToDMS()
        {

            var coordcinatesDictionary = Helpers.ConvertLatToDMS(59.409581, 9.131822, 168.4);

            double lat = 59.409581;
            double lon = -45.197069205344;

           

           

        }
    }
}
