using Microsoft.EntityFrameworkCore;
using TechTest.Data;
using TechTest.Model;
using TechTest.Repository;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace TechTest_Test.Repository
{
    internal class RecordRepositoryTest
    {
        private DataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            databaseContext.Record.Add(
            new Record()
            {
                caller_id = "35273243234788",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 89,
                recipient = "NJIASCJKNSACJNIKSC",
                reference = "WVRTWEBBTEWBEB"
            });            
            databaseContext.Record.Add(
            new Record()
            {
                caller_id = "35273243234788",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 35,
                recipient = "NJIASCJKNSACJNIKSC",
                reference = "VWRETVWREVV"
            });      
            databaseContext.Record.Add(
            new Record()
            {
                caller_id = "35273243234788",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 73,
                recipient = "WRBTRWEBEFWQERV",
                reference = "NJKOJNKCAJKNC"
            });                
            databaseContext.Record.Add(
            new Record()
            {
                caller_id = "35273243234788",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 123,
                recipient = "DFBVETNRTNERBEFV",
                reference = "WDRFVVWERRV"
            });
            databaseContext.Record.Add(
            new Record()
            {
                caller_id = "35273248339248",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "CZK",
                duration = 50,
                recipient = "WCJNC;JKDCN;E",
                reference = "DASDVWWECAXCZV"
            });
            databaseContext.Record.Add(
            new Record()
            {
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "VND",
                duration = 50,
                recipient = "WEWQCCEWECS",
                reference = "ASCWEVEWRVWVWERVWV"
            });
            databaseContext.Record.Add(
            new Record()
            {
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 31,
                recipient = "51235422412341243",
                reference = "EDBERVFWEVREWRV"
            });
            databaseContext.Record.Add(
            new Record()
            {
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 22,
                recipient = "ERVEDCCDQEWQEC",
                reference = "ERGBERTVRETB"
            }); 
            databaseContext.Record.Add(
            new Record()
            {
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 94,
                recipient = "DFBJNTJOVENROJRV",
                reference = "SFVERBTNRNEBRWRETB"
            });
            databaseContext.Record.Add(
            new Record()
            {
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 87,
                recipient = "VERTBNTNRNBRBVR",
                reference = "WRTNEWRBRTBRTBRB"
            });
            databaseContext.SaveChangesAsync();
               
            return databaseContext;
        }

        [Test]
        public  void RecordRepository_GetRecord_ReturnsRecord()
        {
            //Arrange
            var reference = "NJKOJNKCAJKNC";
            var dbContext =  GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            //Act
            var result = recordRepository.GetRecord(reference);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Record>(result);
        }

        [Test]
        public  void RecordRepository_CheckAverageCost()
        {
            //Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);
            var cost = recordRepository.AverageCallCost();

            //Act
            var rest = dbContext.Record.Average(c => c.cost);

            //Assert
            Assert.That(rest, Is.EqualTo(cost));
        }   
        
        [Test]
        public  void RecordRepository_CheckCallerExists()
        {
            //Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            //Act
            var res1 = recordRepository.CallerExists("35273243234788");
            var res2 = recordRepository.CallerExists("35273248339248");
            var res3 = recordRepository.CallerExists("");

            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
            Assert.IsFalse(res3);
        } 
        
        [Test]
        public  void RecordRepository_CheckRecordExists()
        {
            //Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            //Act
            var res1 = recordRepository.RecordExists("SFVERBTNRNEBRWRETB");
            var res2 = recordRepository.RecordExists("ASCWEVEWRVWVWERVWV");
            var res3 = recordRepository.RecordExists("123123123123");

            //Assert
            Assert.IsTrue(res1);
            Assert.IsTrue(res2);
            Assert.IsFalse(res3);
        }
        [Test]
        public void RecordRepository_DeleteRecord()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Add a new record to the database
            var newRecord = new Record
            {
                caller_id = "123456789",
                call_date = DateOnly.MaxValue,
                end_time = TimeOnly.MaxValue,
                cost = 0.5,
                currency = "USD",
                duration = 50,
                recipient = "TestRecipient",
                reference = "TestReference"
            };
            dbContext.Record.Add(newRecord);
            dbContext.SaveChanges();

            // Act
            var resultBeforeDelete = dbContext.Record.Find(newRecord.Id);
            var deleteResult = recordRepository.DeleteRecord(newRecord);
            var resultAfterDelete = dbContext.Record.Find(newRecord.Id);

            // Assert
            Assert.IsNotNull(resultBeforeDelete);  // Check that the record exists before deletion
            Assert.IsTrue(deleteResult);            // Check that the deletion was successful
            Assert.IsNull(resultAfterDelete);       // Check that the record is no longer in the database after deletion
        }

        [Test]
        public void RecordRepository_SuccessfullyDownloadsRecords()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Act
            var success = recordRepository.DownloadRecords();

            // Assert
            Assert.IsTrue(success, "DownloadRecords should return true for successful download");
            Assert.IsTrue(File.Exists("records.csv"), "CSV file should be created");
            File.Delete("records.csv");
        }

        [Test]
        public void RecordRepository_GetRecords_ReturnsCorrectPaginatedRecords()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Act
            int pageNumber = 1;
            int pageSize = 5;
            var paginatedRecords = recordRepository.GetRecords(pageNumber, pageSize);

            // Assert
            Assert.IsNotNull(paginatedRecords, "Paginated records should not be null");
            Assert.That(paginatedRecords.Count, Is.EqualTo(pageSize), "Number of records should match the specified page size");
        }

        [Test]
        public void RecordRepository_GetRecordsOfCaller_ReturnsCorrectRecords()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Act
            string targetCallerId = "35273243234788";
            var recordsOfCaller = recordRepository.GetRecordsOfCaller(targetCallerId);
   

            // Assert
            Assert.IsNotNull(recordsOfCaller, "Records of caller should not be null");
            Assert.That(recordsOfCaller.Count, Is.EqualTo(4), "Number of records should match the expected count");

            var retrievedRecord = recordsOfCaller.First();
            Assert.That(retrievedRecord.caller_id, Is.EqualTo(targetCallerId), "Caller ID should match the expected value");
        }

        [Test]
        public void RecordRepository_LongestCalls_ReturnsCorrectRecords()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Act
            var longestCalls = recordRepository.LongestCalls();

            // Assert
            Assert.IsNotNull(longestCalls, "Longest calls should not be null");
            Assert.That(longestCalls.Count, Is.EqualTo(5), "Number of longest calls should match the expected count");

            // Check if the records are ordered by duration in descending order
            var expectedDurations = new List<int> { 123, 94, 89, 87, 73 };
            var actualDurations = longestCalls.Select(record => record.duration).ToList();
            CollectionAssert.AreEqual(expectedDurations, actualDurations, "Durations should be in descending order");
        }

        [Test]
        public void RecordRepository_UploadRecord_SavesRecords()
        {
            // Arrange
            var dbContext = GetDatabaseContext();
            var recordRepository = new RecordRepository(dbContext);

            // Create a mock IFormFile with CSV content
            var csvContent = "caller_id,recipient,call_date,end_time,duration,cost,reference,currency\n" +
                  "441215598896,448000096481,16/08/2016,14:21:33,43,0,C5DA9724701EEBBA95CA2CC5617BA93E4,GBP";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
            var formFile = new FormFile(stream, 0, stream.Length, "data", "data.csv");

            // Act
            var success = recordRepository.UploadRecord(formFile);

            // Assert
            Assert.IsTrue(success, "UploadRecord should return true on successful upload");

            // Check if records are saved to the database
            var savedRecords = dbContext.Record.ToList();
            // 10 records created + 1 record in the mock csv file
            Assert.That(savedRecords.Count, Is.EqualTo(11), "Number of saved records should match the expected count");
            
            // Clean up: Delete the added records (optional)
            dbContext.Record.RemoveRange(savedRecords);
            dbContext.SaveChanges();
        }



    }
}
