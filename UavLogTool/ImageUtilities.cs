using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UavLogTool
{
    public class ImageUtilities
    {

        public static byte[] FloatToExifGps(int degrees, int minutes, int seconds)
        {
            var secBytes = BitConverter.GetBytes(seconds);
            var secDivisor = BitConverter.GetBytes(100);
            byte[] rv = { (byte)degrees, 0, 0, 0, 1, 0, 0, 0, (byte)minutes, 0, 0, 0, 1, 0, 0, 0, secBytes[0], secBytes[1], 0, 0, secDivisor[0], 0, 0, 0 };
            return rv;
        }

        //https://gist.github.com/5342/3293802
        public static void ExtractLocation(string file)
        {
            if (file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("jpeg"))
            {
                Image image = null;
                try
                {
                    Console.Title = file;

                    try
                    {
                        FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                        image = Image.FromStream(fs);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error opening {0} image may be corupted, {1}", file, ex.Message);
                        return;
                    }

                    // GPS Tag Names
                    // http://www.sno.phy.queensu.ca/~phil/exiftool/TagNames/GPS.html

                    // Check to see if we have gps data
                    if (Array.IndexOf<int>(image.PropertyIdList, 1) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 2) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 3) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 4) != -1)
                    {

                        string gpsLatitudeRef = BitConverter.ToChar(image.GetPropertyItem(1).Value, 0).ToString();
                        string latitude = DecodeRational64u(image.GetPropertyItem(2));
                        string gpsLongitudeRef = BitConverter.ToChar(image.GetPropertyItem(3).Value, 0).ToString();
                        string longitude = DecodeRational64u(image.GetPropertyItem(4));
                        Console.WriteLine("{0}\t{1} {2}, {3} {4}", file, gpsLatitudeRef, latitude, gpsLongitudeRef, longitude);
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error processign {0} {1}", file, ex.Message); }
                finally
                {
                    if (image != null) image.Dispose();
                }


            }
        }

        public static string DecodeRational64u(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            uint dN = BitConverter.ToUInt32(propertyItem.Value, 0);
            uint dD = BitConverter.ToUInt32(propertyItem.Value, 4);
            uint mN = BitConverter.ToUInt32(propertyItem.Value, 8);
            uint mD = BitConverter.ToUInt32(propertyItem.Value, 12);
            uint sN = BitConverter.ToUInt32(propertyItem.Value, 16);
            uint sD = BitConverter.ToUInt32(propertyItem.Value, 20);

            decimal deg;
            decimal min;
            decimal sec;
            // Found some examples where you could get a zero denominator and no one likes to devide by zero
            if (dD > 0) { deg = (decimal)dN / dD; } else { deg = dN; }
            if (mD > 0) { min = (decimal)mN / mD; } else { min = mN; }
            if (sD > 0) { sec = (decimal)sN / sD; } else { sec = sN; }

            if (sec == 0) return string.Format("{0}° {1:0.#######}'", deg, min);
            else return string.Format("{0}° {1:0}' {2:0.#######}\"", deg, min, sec);
        }
    }
}
