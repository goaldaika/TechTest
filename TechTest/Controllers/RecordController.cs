using Microsoft.AspNetCore.Mvc;
using System.IO;
using TechTest.Interface;
using TechTest.Model;

namespace TechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : Controller
    {
        private readonly IRecord _record;
        public RecordController(IRecord record)
        {
            _record = record;

        }

        #region Get records of a page 

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Record>))]
        public IActionResult GetRecords([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if(pageNumber < 1 || pageSize < 1) return BadRequest(); 
            var records = _record.GetRecords(pageNumber, pageSize);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(records);
        }
        #endregion

        #region Get all the records of one specific caller
        [HttpGet("recordsOfCaller/{caller_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Record>))]
        public IActionResult GetRecordsOfCaller(string caller_id)
        {

            if (!_record.CallerExists(caller_id)) return NotFound();

            var record = _record.GetRecordsOfCaller(caller_id);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(record);
        }
        #endregion

        #region Get the record base on UNIQUE reference
        [HttpGet("{reference}")]
        [ProducesResponseType(200, Type = typeof(Record))]
        [ProducesResponseType(400)]
        public IActionResult GetRecord(string reference)
        {
            if (!_record.RecordExists(reference)) return NotFound();

            var records = _record.GetRecord(reference);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(records);
        }
        #endregion

        #region Get all the records of every record that has null caller_id
        [HttpGet("nullId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Record>))]
        public IActionResult GetRecordsOfNullId()
        {
            var records = _record.GetRecordsOfNullId();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(records);
        }
        #endregion

        #region Get 5 records that has the longest duration
        [HttpGet("duration")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Record>))]
        public IActionResult LongestCalls()
        {
            var record = _record.LongestCalls();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(record);
        }
        #endregion

        #region Calculate the average cost from all records
        [HttpGet("cost")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Record>))]
        public IActionResult AverageCallCost()
        {
            var cost = _record.AverageCallCost();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(cost);
        }
        #endregion

        #region Delete record
        [HttpDelete("{reference}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecord(string reference)
        {
            if (!_record.RecordExists(reference))
            {
                return NotFound();
            }

            var recordToDelete = _record.GetRecord(reference);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_record.DeleteRecord(recordToDelete))
            {
                ModelState.AddModelError("", "Something wrong happened");
            }

            return NoContent();
        }
        #endregion

        #region Download record to csv file
        [HttpGet("downloadRecords")]
        public IActionResult DownloadRecords()
        {
            try 
            { 
                var success = _record.DownloadRecords();

                return Ok("Records downloaded successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest("Failed to download records.");
            }
        }
        #endregion

        #region Upload new file to the database
        [HttpPost("uploadRecord")]
        public IActionResult UploadRecord(IFormFile formFile)
        {
            try
            {

                _record.UploadRecord(formFile);

                return Ok("Upload successful");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate response
                return BadRequest($"Upload failed. Error: {ex.Message}");
            }
        }
        #endregion

    }
}
