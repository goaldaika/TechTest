using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Controllers;
using TechTest.Interface;
using TechTest.Model;

namespace TechTest_Test.Controller
{
    [TestFixture]
    public class RecordControllerTests
    {
        private Mock<IRecord> _recordMock;
        private RecordController _recordController;

        [SetUp]
        public void Setup()
        {
            _recordMock = new Mock<IRecord>();
            _recordController = new RecordController(_recordMock.Object);
        }

        [Test]
        public void GetRecords_ReturnsRecords()
        {
            // Arrange
            _recordMock.Setup(x => x.GetRecords(It.IsAny<int>(), It.IsAny<int>()))
                       .Returns(new List<Record> { new Record() });

            // Act
            var result = _recordController.GetRecords(1, 10) as OkObjectResult;
            var records = result?.Value as List<Record>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(records);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(records.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetRecord_WithValidReference_ReturnsRecord()
        {
            // Arrange
            var validReference = "validReference";
            _recordMock.Setup(x => x.RecordExists(validReference)).Returns(true);
            _recordMock.Setup(x => x.GetRecord(validReference)).Returns(new Record());

            // Act
            var result = _recordController.GetRecord(validReference) as OkObjectResult;
            var record = result?.Value as Record;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(record);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetRecord_WithInvalidReference_ReturnsNotFound()
        {
            // Arrange
            var invalidReference = "invalidReference";
            _recordMock.Setup(x => x.RecordExists(invalidReference)).Returns(false);

            // Act
            var result = _recordController.GetRecord(invalidReference) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetRecords_WithValidPageNumberAndPageSize_ReturnsRecords()
        {
            // Arrange
            _recordMock.Setup(x => x.GetRecords(It.IsAny<int>(), It.IsAny<int>()))
                       .Returns(new List<Record> { new Record() });

            // Act
            var result = _recordController.GetRecords(1, 10) as OkObjectResult;
            var records = result?.Value as List<Record>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(records);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(records.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetRecords_WithInvalidPageNumber_ReturnsBadRequest()
        {
            // Arrange
            // Simulate an invalid page number (less than 1)
            var invalidPageNumber = 0;

            // Act
            var result = _recordController.GetRecords(invalidPageNumber, 10);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void DeleteRecord_WithValidReference_ReturnsNoContent()
        {
            // Arrange
            string validReference = "validReference";
            var existingRecord = new Record { /* Your existing record here */ };
            _recordMock.Setup(r => r.RecordExists(validReference)).Returns(true);
            _recordMock.Setup(r => r.GetRecord(validReference)).Returns(existingRecord);
            _recordMock.Setup(r => r.DeleteRecord(existingRecord)).Returns(true);

            // Act
            var result = _recordController.DeleteRecord(validReference);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteRecord_WithInvalidReference_ReturnsNotFoundResult()
        {
            // Arrange
            string invalidReference = "invalidReference";
            _recordMock.Setup(r => r.RecordExists(invalidReference)).Returns(false);

            // Act
            var result = _recordController.DeleteRecord(invalidReference);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DownloadRecords_Successful_ReturnsOkResult()
        {
            // Arrange
            _recordMock.Setup(r => r.DownloadRecords()).Returns(true);

            // Act
            var result = _recordController.DownloadRecords();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DownloadRecords_Failure_ReturnsBadRequestResult()
        {
            // Arrange
            _recordMock.Setup(r => r.DownloadRecords()).Throws(new Exception("Simulated failure"));

            // Act
            var result = _recordController.DownloadRecords() as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.That(result.Value, Is.EqualTo("Failed to download records."));
        }





        [Test]
        public void UploadRecord_Successful_ReturnsOkResult()
        {
            // Arrange
            var formFile = new Mock<IFormFile>();
            _recordMock.Setup(r => r.UploadRecord(formFile.Object));

            // Act
            var result = _recordController.UploadRecord(formFile.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UploadRecord_Failure_ReturnsBadRequestResult()
        {
            // Arrange
            var formFile = new Mock<IFormFile>();
            _recordMock.Setup(r => r.UploadRecord(formFile.Object)).Throws(new Exception("Simulated upload failure"));

            // Act
            var result = _recordController.UploadRecord(formFile.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }



}

