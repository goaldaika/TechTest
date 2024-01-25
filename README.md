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
