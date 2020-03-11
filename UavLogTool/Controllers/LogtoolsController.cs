using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using UavLogTool.Models;





namespace UavLogTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogtoolsController : ControllerBase
    {



        // POST: api/Logtools
        [HttpPost("GetCsvVideoInfo")]
        public async Task<IActionResult> GetCsvVideoLogInfo(IFormFile djiCsvLog)
        {
            long size = djiCsvLog.Length;
            string extension = Path.GetExtension(djiCsvLog.FileName).ToLower();
            if (size > 0)
            {
                if (extension != ".csv")
                {
                    return BadRequest("wrong file format");
                }
            }
            else
            {
                return BadRequest("CSV File Not Found");
            }

            List<VideoInfoModel> videoInfoModels;
            using (TextFieldParser csvParser = new TextFieldParser(djiCsvLog.OpenReadStream()))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                //Processing row
                string[] headers = csvParser.ReadFields();
                var djiHeaderDictionary = CsvUtilities.GetDjiHeaderDictionary(headers);


                var uavLogs = new List<UavLog>();

                if (djiHeaderDictionary.Any())
                {
                    int rowNumber = 1;
                    while (csvParser.PeekChars(1) != null)
                    {

                        rowNumber++;
                        string[] fields = csvParser.ReadFields();
                        var index = djiHeaderDictionary["VideoRecordTime"];

                        var noko = fields[index];

                        if (fields.Length > index && !string.IsNullOrEmpty(fields[index]) && fields[index] != "0")
                        {
                            uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary, rowNumber));
                        }
                    }
                }
                if (!uavLogs.Any())
                {
                    return BadRequest("No Video log found in file");
                }

                var csv = String.Join(",", uavLogs);

                //TODO not working
                //var uavlogsSort = uavLogs.OrderBy(l => l.DateTime).ToList();

                var dictionarylog = Helpers.SplitVideosFromUavLog(uavLogs);
                var video1LenghInMilliseconds = Helpers.GetVideoLenghtInMilliseconds(dictionarylog.FirstOrDefault().Value);





                foreach (var videologs in dictionarylog)
                {
                    var csvVideoLogs = CsvUtilities.ToCsv(",", videologs.Value);


                    var filename = $"{videologs.Value.FirstOrDefault().DateTime.ToString("_yyyy-MM-dd_HH-mm-ss")}_{videologs.Key}.csv";
                    var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", filename), csvVideoLogs);
                }
                videoInfoModels = Helpers.GetVideoInfoModels(dictionarylog);
            }

            if (videoInfoModels != null)
            {
                var csvVideoInfoModels = CsvUtilities.ToCsv(",", videoInfoModels);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(djiCsvLog.FileName);
                var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", $"{fileNameWithoutExtension}_resume.csv"), csvVideoInfoModels);
                return Ok(videoInfoModels);
            }



            return BadRequest("Something Went wrong");



        }

        // POST: api/Logtools
        [HttpPost("TrimCsvLog")]
        public async Task<IActionResult> TrimCsvLog(IFormFile uavLogsCsv, string startTime, string endTime)
        {
            long csvFilLength = uavLogsCsv.Length;
            string csvFileExtension = Path.GetExtension(uavLogsCsv.FileName).ToLower();
            if (csvFilLength > 0)
            {
                if (csvFileExtension != ".csv")
                {
                    return BadRequest("wrong CSV file format");
                }
            }
            else
            {
                return BadRequest("CSV File Not Found");

            }
            List<UavLog> uavLogs = new List<UavLog>();

            using (TextFieldParser csvParser = new TextFieldParser(uavLogsCsv.OpenReadStream()))
            {
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string[] headers = csvParser.ReadFields();
                var djiHeaderDictionary = CsvUtilities.GetHeaderDictionary(headers);



                if (djiHeaderDictionary.Any())
                {
                    int rowNumber = 1;
                    while (csvParser.PeekChars(1) != null)
                    {
                        rowNumber++;
                        string[] fields = csvParser.ReadFields();
                        uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary, rowNumber));
                    }
                }

                string csv = String.Join(",", uavLogs);
            }
            var sortUavList = uavLogs;
            sortUavList.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));



            //var newUavlogs = Helpers.TrimUavLogs(sortUavList, "01:00:22", "01:36:22");
            TimeSpan startTimeSpan = Helpers.GetTimeSpan(startTime);

            TimeSpan endTimeSpan = Helpers.GetTimeSpan(endTime);
            if (startTimeSpan == TimeSpan.Zero)
            {
                return BadRequest($"cant get video start time from string '{startTime}'");
            }

            if (endTimeSpan == TimeSpan.Zero)
            {
                return BadRequest($"cant get video end time from string '{endTime}'");
            }

            var newUavlogs = Helpers.TrimUavLogs(sortUavList, startTimeSpan, endTimeSpan);

            var videoInfoModels = Helpers.GetVideoInfoModels(newUavlogs);


            if (videoInfoModels != null)
            {
                var csvVideoInfoModels = CsvUtilities.ToCsv(",", videoInfoModels);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uavLogsCsv.FileName);
                var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", $"{fileNameWithoutExtension}_resume.csv"), csvVideoInfoModels);





                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(string.Concat(csvVideoInfoModels));
                var stream = new MemoryStream(byteArray);
                return File(stream, "application/octet-stream", $"{ fileNameWithoutExtension}_resume.csv");
            }
            return BadRequest("No file Created");
        }





        // POST: api/Logtools

        [HttpPost("UpdateImageExfifFromCsv")]
        public async Task<IActionResult> UpdateImageExfifFromCsv(IFormFile uavLogsCsv, IFormFile image, string time)
        {
            long csvFilLength = uavLogsCsv.Length;
            long imageFilLength = uavLogsCsv.Length;
            string csvFileExtension = Path.GetExtension(uavLogsCsv.FileName).ToLower();
            string imageFileExtension = Path.GetExtension(image.FileName).ToLower();
            if (csvFilLength > 0)
            {
                if (csvFileExtension != ".csv")
                {
                    return BadRequest("wrong CSV file format");
                }
            }
            else
            {
                return BadRequest("CSV File Not Found");

            }

            if (imageFilLength > 0)
            {
                if (imageFileExtension != ".jpg")
                {
                    return BadRequest("wrong Image file format");
                }
            }
            else
            {
                return BadRequest("Image File Not Found");
            }

            List<UavLog> uavLogs = new List<UavLog>();

            //using (TextReader reader = new StreamReader(uavLogsCsv.OpenReadStream()))
            //{
            //    uavLogs = CsvUtilities.GetUavLosFromCsv(reader);
            //}


            return Ok();

        }
        // POST: api/Logtools
        [HttpPost("GetCsvVideoGpS")]
        public async Task<IActionResult> GetCsvVideoPosition(IFormFile uavLogsCsv, string time)
        {
            long csvFilLength = uavLogsCsv.Length;
            long imageFilLength = uavLogsCsv.Length;
            string csvFileExtension = Path.GetExtension(uavLogsCsv.FileName).ToLower();
            if (csvFilLength > 0)
            {
                if (csvFileExtension != ".csv")
                {
                    return BadRequest("wrong CSV file format");
                }
            }
            else
            {
                return BadRequest("CSV File Not Found");

            }
            List<UavLog> uavLogs = new List<UavLog>();

            using (TextFieldParser csvParser = new TextFieldParser(uavLogsCsv.OpenReadStream()))
            {
                uavLogs = CsvUtilities.GetUavLogFromTextFile(csvParser);
            }
            TimeSpan timeSpan = Helpers.GetTimeSpan(time);

            var photolog = Helpers.GetUavLogFromVideoTimeStamp(timeSpan, uavLogs);//"03:56:22"

            return Ok(photolog);
        }
        // POST: api/Logtools
        [HttpPost("GetCsvFromSrt")]
        public async Task<IActionResult> GetCsvFromSrt(IFormFile srtLog)
        {
            long size = srtLog.Length;
            string extension = Path.GetExtension(srtLog.FileName).ToLower();
            if (size > 0)
            {
                if (!extension.Equals(".SRT",StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("wrong file format");
                }
            }
            else
            {
                return BadRequest("SRT File Not Found");
            }
            string line;

            DjiSrtVideoLogModel djiSrtVideoLogModel = new DjiSrtVideoLogModel();
            List<DjiSrtVideoLogModel> djiSrtVideoLogModels = new List<DjiSrtVideoLogModel>();

            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(srtLog.OpenReadStream()))
                {
                    int lineCount = 0;
                    int id = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        int idTemp;
                        if (int.TryParse(line, out idTemp))
                        {
                            djiSrtVideoLogModel = new DjiSrtVideoLogModel();
                            id = idTemp;
                            lineCount++;
                        }
                        if (id >= 1)
                        {
                            switch (lineCount)
                            {
                                case 1:
                                    djiSrtVideoLogModel.Id = id;
                                    lineCount++;
                                    break;
                                case 2:
                                    djiSrtVideoLogModel = SrtConverter.GetTimeStamp(line, ref djiSrtVideoLogModel);
                                    lineCount++;
                                    break;
                                case 3:
                                    djiSrtVideoLogModel = SrtConverter.GetHomePointAndDateTime(line, ref djiSrtVideoLogModel);
                                    lineCount++;
                                    break;
                                case 4:
                                    djiSrtVideoLogModel = SrtConverter.GetGpsPointAndBarometer(line, ref djiSrtVideoLogModel);
                                    lineCount++;
                                    break;
                                case 5:
                                    djiSrtVideoLogModel = SrtConverter.GetCameraInfo(line, ref djiSrtVideoLogModel);
                                    djiSrtVideoLogModels.Add(djiSrtVideoLogModel);
                                    lineCount = 0;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (djiSrtVideoLogModels.Any())
                    {
                        var csvVideoInfoModels = CsvUtilities.ToCsv(",", djiSrtVideoLogModels);
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(srtLog.FileName);
                        var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", $"{fileNameWithoutExtension}_resume.csv"), csvVideoInfoModels);

                        return Ok("Ok creating Srt to Csv");
                    }
                    else
                    {
                        return BadRequest("djiSrtVideoLogModels dont have any record");

                    }
                }

            }
            catch (Exception)
            {

                return BadRequest("Problem saving Srt to csv");
            }
        }
    }
}
