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
            PropertyItem propertyTagGpsLongitude = image1.GetPropertyItem(4);
            PropertyItem propertyTagGpsLongitudeRef = image1.GetPropertyItem(3);
            var valueStringRef = System.Text.Encoding.Default.GetString(propertyTagGpsLongitudeRef.Value);

            PropertyItem propertyTagGpsLaitude = image1.GetPropertyItem(6);
            var valueString = System.Text.Encoding.Default.GetString(propertyTagGpsLongitude.Value);
            propertyTagGpsLongitude.Value =ImageUtilities.FloatToExifGps(79, 48, 33775);
            image1.SetPropertyItem(propertyTagGpsLongitude);


            image1.Save(@"C:\Temp\image_Test.JPG");
        }

        [Fact]
        public void testCreateImageProperty()
        {
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/71d8de37-f52d-4faa-887a-793f8041110a/managing-general-exif-info-with-imagesetpropertyitem?forum=netfxbcl
            var path = @"Data\test.JPG";

            ImageUtilities.ExtractLocation(path);

            Image image1 = Image.FromFile(path);
            PropertyItem propertyTagGpsLongitude = image1.PropertyItems[4];
            PropertyItem propertyTagGpsLongitudeRef = image1.PropertyItems[3];
            PropertyItem propertyTagGpsLatudeRef = image1.PropertyItems[5];
            PropertyItem propertyTagGpsLatude = image1.PropertyItems[6];

            var valueString = System.Text.Encoding.Default.GetString(propertyTagGpsLongitude.Value);
            var longitudValue=propertyTagGpsLongitude.Value = ImageUtilities.FloatToExifGps(79, 48, 33775);
            var latitudValue=propertyTagGpsLongitude.Value = ImageUtilities.FloatToExifGps(5, 48, 33775);
            ImageUtilities.SetProperty(ref propertyTagGpsLongitude, 4, "5", longitudValue);
            ImageUtilities.SetProperty(ref propertyTagGpsLongitudeRef, 4, "2", "N");
            ImageUtilities.SetProperty(ref propertyTagGpsLatude, 4, "5", latitudValue);
            ImageUtilities.SetProperty(ref propertyTagGpsLatudeRef, 3, "2", "O");
            image1.SetPropertyItem(propertyTagGpsLongitude);


            image1.Save(@"C:\Temp\image_wthoutGPSData_Test.JPG");

        }
    }
}
