# Project Overview

## Frameworks and Technologies

- **Development:**
  - ASP.NET Web API
  - EF Core
  - Microsoft SQL
  - CSV Helper
  
- **Testing:**
  - NUnit
  - Moq
  - EF Core

## Application Structure

The application consists of 9 API methods:

1. **Get Records:**
   - Retrieve a specified number of records from a given page, considering pagination.

2. **Get Records of a Given caller_id**
  
3. **Get Records of a Given UNIQUE Reference**

4. **Get All Records with Null caller_id**
  
5. **Get 5 Records with the Longest Duration**
  
6. **Calculate the Average Cost from All Records**

7. **Delete a Record Based on a Given UNIQUE Reference**

8. **Download Current Records to a CSV File**
   - Default save location is in the project directory.

9. **Upload a New CSV File to the Database**
   - Existing data in the database will remain.

## Application Architecture

The application is developed using the Repository Pattern (RP). Key folders include:

- **Data:**
  - Contains DataContext to establish a connection with the database.
      
- **Interface:**
  - Contains blueprints for methods or features to be implemented.

- **Repository:**
  - Classes for handling data logic and interactions with the database (CRUD operations).

- **Controller:**
  - Classes responsible for handling routes and requests.

- **Helper:**
  - Contains a helper class to map data obtained from the application to the database.

## Notable Considerations and Future Enhancements

- **Deployment**
  - The API is deployed, however, not every request works as expectation, would need more time to finish.
    
- **Exception Handling:**
  - Implement enhanced exception handling to avoid exposing errors directly to users.

- **Logging Framework Integration:**
  - Integrate a logging framework (e.g., Serilog or NLog) to capture relevant information, warnings, and errors for debugging and monitoring.

- **Download Records Method:**
  - Allow users to specify the file location or provide a default download location as a parameter or from configuration settings.

- **Pagination Enhancement:**
  - Enhance pagination implementation to include additional information in response headers, such as total records or pages.

- **Unit Testing:**
  - Implement more unit tests to ensure comprehensive coverage for all possible cases.


 
## Deployment

- **https://techtest20240125064152.azurewebsites.net/api/Record/nullId**
  - GET request to get all the records that have null caller_id

- **https://techtest20240125064152.azurewebsites.net/api/Record/cost**
  - GET request to get the average cost from all records in the database

- **https://techtest20240125064152.azurewebsites.net/api/Record/duration**
  - GET request to get 5 records that have longest call duration

- **https://techtest20240125064152.azurewebsites.net/api/Record/recordsOfCaller/443330134974**
  - GET request to get all records of the given caller_id, change the last string to another caller_id to get the corresponding records

- **https://techtest20240125064152.azurewebsites.net/api/Record?pageNumber=10&pageSize=500**
  - GET request to get the amount of records required from given page. Change the number to get different records on different page, "pageNumber" is the number of page, "pageSize" is the amounts of records required to get.
 
- **https://techtest20240125064152.azurewebsites.net/api/Record/C5DA9724701EEBBA95CA2CC5617BA93E4**
  - GET request to get the record corresponding with the reference.

- **https://techtest20240125064152.azurewebsites.net/api/Record/uploadRecord**
  - Suppose to be POST request to upload new file to the database, but it return the error: '{"type":"https://tools.ietf.org/html/rfc9110#section-15.5.5","title":"Not Found","status":404,"traceId":"00-194cb7724664dbda2dce83d59253a272-c9d2a326fd1e2cb0-00"}'. This request works well on local machine.

- **https://techtest20240125064152.azurewebsites.net/api/Record/downloadRecords**
  - Suppose to be GET request to download record, it will return a valid response, but the downloaded file is no where to be found. Highly recommend to not use or access it. Works well on local machine, since the file will be default saved in the project directory.

- **https://techtest20240125064152.azurewebsites.net/api/Record/delete/C5DA9724701EEBBA95CA2CC5617BA93E4**
  - Suppose to be DELETE request to delete record base on given reference, however, it entirely couldn't work. Still works well on local machine.
