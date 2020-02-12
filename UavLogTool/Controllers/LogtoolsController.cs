using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [HttpPost("ConvertDjiCsv")]
        public async Task<IActionResult> Post(IFormFile djiCsvLog)
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

            var filePathTemp = Path.GetTempFileName();

            using (TextFieldParser csvParser = new TextFieldParser(djiCsvLog.OpenReadStream()))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                //// Skip the row with the column names
                //csvParser.ReadLine();


                //var noko = File.ReadAllLines(path);



                //Processing row
                string[] headers = csvParser.ReadFields();
                var djiHeaderDictionary = CsvUtilities.GetDjiHeaderDictionary(headers);


                //foreach (string field in fields)
                //{
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
                string csv = String.Join(",", uavLogs);
                
                //TODO not working
                //var uavlogsSort = uavLogs.OrderBy(l => l.DateTime).ToList();

                var dictionarylog = Helpers.SplitVideosFromUavLog(uavLogs);
                var video1LenghInMilliseconds = Helpers.GetVideoLenghtInSeconds(dictionarylog.FirstOrDefault().Value);
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
