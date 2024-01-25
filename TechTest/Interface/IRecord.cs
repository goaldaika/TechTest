using TechTest.Model;

namespace TechTest.Interface
{
    public interface IRecord
    {
        ICollection<Record> GetRecords(int pageNumber, int pageSize); //Get records of a given page. 
        ICollection<Record> GetRecordsOfNullId(); //Get all the records that have the caller_id is null
        ICollection<Record> GetRecordsOfCaller(string caller_id); //Get record base on provided caller_id
        Record GetRecord(string reference); //Get record base on UNIQUE reference
        bool RecordExists(string reference); // check if a record exists ?
        bool CallerExists(string caller_id); // check if a caller exists ?
        bool DeleteRecord(Record record); //record deleted based on the given reference
        double AverageCallCost(); //calculate the average cost of all calls
        ICollection<Record> LongestCalls(); //get 5 calls that have the longest duration.
        bool UploadRecord(IFormFile formFile); //The uploaded data will join with the existed data in the database 
        bool DownloadRecords(); //the records are downloaded to csv file, and stored in the project directory
        bool Save(); //save any change in the database


    }
}
