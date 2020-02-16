using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
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
                    while (csvParser.PeekChars(1) != null)
                    {
                        string[] fields = csvParser.ReadFields();
                        var index = djiHeaderDictionary["VideoRecordTime"];

                        var noko = fields[index];
                        if (fields.Length > index && fields[index] != "0")
                        {
                            uavLogs.Add(CsvUtilities.GetUavLog(fields, djiHeaderDictionary));
                        }
                    }
                }
                var csv = String.Join(",", uavLogs);

                //TODO not working
                //var uavlogsSort = uavLogs.OrderBy(l => l.DateTime).ToList();

                var dictionarylog = Helpers.SplitVideosFromUavLog(uavLogs);
                var video1LenghInMilliseconds = Helpers.GetVideoLenghtInSeconds(dictionarylog.FirstOrDefault().Value);


                foreach (var videologs in dictionarylog)
                {
                    var csvVideoLogs = CsvUtilities.ToCsv(",", videologs.Value);
                    var filename = $"{videologs.Value.FirstOrDefault().DateTime.ToString("yyMMdd")}_{videologs.Key}.csv";
                    var saved = CsvUtilities.SaveCsvTofile(Path.Combine(@"C:\Temp\", filename), csvVideoLogs);
                }
            }
            return Ok();
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

            var filePathTemp = Path.GetTempFileName();

            List<UavLog> uavLogs = new List<UavLog>();

            using (TextReader reader = new StreamReader(uavLogsCsv.OpenReadStream()))
            {
                uavLogs = CsvUtilities.GetUavLosFromCsv(reader);
            }

            return Ok();

        }


        // GET: api/Logtools
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Logtools/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT: api/Logtools/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
