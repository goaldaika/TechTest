using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using TechTest.Data;
using TechTest.Helper;
using TechTest.Interface;
using TechTest.Model;

namespace TechTest.Repository
{
    public class RecordRepository : IRecord
    {
        private readonly DataContext _ctx;
        public RecordRepository(DataContext ctx)
        {
            _ctx = ctx;
        }
        public double AverageCallCost()
        {
            return _ctx.Record.Average(c => c.cost);
        }

        public bool CallerExists(string caller_id)
        {
            return _ctx.Record.Any(c => c.caller_id == caller_id);
        }

        public bool DeleteRecord(Record record)
        {
            _ctx.Remove(record);
            return Save();
        }

        public bool DownloadRecords()
        {
            var records = _ctx.Record.ToList();
            var path = ("records.csv");

            try
            {
                using (var writer = new StreamWriter(path))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(records);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Record GetRecord(string reference)
        {
            return _ctx.Record.Where(r => r.reference == reference).FirstOrDefault();
        }

        public ICollection<Record> GetRecords(int pageNumber, int pageSize)
        {
            // Ensure page number and page size are positive values
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, pageSize);

            // Calculate the number of records to skip based on page size and number
            int recordsToSkip = (pageNumber - 1) * pageSize;

            // Retrieve records with pagination
            var paginatedRecords = _ctx.Record.Skip(recordsToSkip).Take(pageSize).ToList();

            return paginatedRecords;
        }

        public ICollection<Record> GetRecordsOfCaller(string caller_id)
        {
            return _ctx.Record.Where(r => r.caller_id == caller_id).ToList();
        }

        public ICollection<Record> GetRecordsOfNullId()
        {
            return _ctx.Record.Where(r => String.IsNullOrEmpty(r.caller_id)).ToList();
        }

        public ICollection<Record> LongestCalls()
        {
            return _ctx.Record.OrderByDescending(d => d.duration).Take(5).ToList();
        }

        public bool RecordExists(string reference)
        {
            return _ctx.Record.Any(r => r.reference == reference);
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UploadRecord(IFormFile formFile)
        {
            // Upload file to temp location
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                // Create the file
                formFile.CopyTo(stream);
            }

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, //  CSV file has a header row
                MissingFieldFound = null, // Ignore missing fields in the CSV
            };

            // Read CSV file and save data to the database
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                csv.Context.RegisterClassMap<RecordMap>(); // Register the custom class map
                // Assuming your CSV maps to the Record model
                var records = csv.GetRecords<Record>().ToList();

                // Save records to the database
                _ctx.Record.AddRange(records);
            }

            // Delete temp file after data is imported

            File.Delete(filePath); return Save();

        }

    }


}
