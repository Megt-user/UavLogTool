using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Text;
using Xunit;
using System.Drawing.Imaging;
using UavLogTool;

namespace UavLogToolTest
{
    public class ImageUtilitiesTests
    {
        [Fact]
        public void Test1()
        {
            var path = @"Data\DJI_0739.JPG";

            ImageUtilities.ExtractLocation(path);

            Image image1 = Image.FromFile(path);
            // Start looking at the exif tags, which are PropertyItems
            //foreach (PropertyItem item in image1.PropertyItems)
            //{
            //    var count = image1.PropertyItems.Length;
            //    var noko = item.Value; // er ...
            //    var valueString = System.Text.Encoding.Default.GetString(noko);


            //    var idInt = item.Id;
            //    var hex = idInt.ToString("x8");
            //    var hex8 = idInt.ToString("X");
               
            //    if (hex.Contains("0001"))
            //    {
                    
            //    } 
            //    if (hex.Contains("0002"))
            //    {
                    
            //    } if (hex.Contains("0002"))
            //    {
                    
            //    }
            //}
            PropertyItem PropertyTagGpsLongitude = image1.GetPropertyItem(4);
            var valueString = System.Text.Encoding.Default.GetString(PropertyTagGpsLongitude.Value);
            PropertyTagGpsLongitude.Value =ImageUtilities.FloatToExifGps(79, 48, 33775);
            image1.SetPropertyItem(PropertyTagGpsLongitude);


            image1.Save(@"C:\Temp\image_Test.JPG");
        }
    }
}
