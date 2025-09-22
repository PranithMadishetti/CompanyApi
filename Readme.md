Company API - Coding exercise

A minimal .NET API for managing the company data. This project demonstrates the basic API design, validation, and configuration using .NET.

Features:
- Minimal API Architecture with 'Program.cs'.
- In memory storage for companies.
- End points for both creating and listing the companies. 
- Validation of the company name and it's respective domain names (keyword + fuzzy match)

API Endpoints:

- ROOT API - GET / - Returns a welcome message as "Welcome to the CompanyAPI"
- CREATE Company - POST/companies - used to create a company. 
    Request Body:
    {
        "name": "OpenAI",
        "websiteUrl": "https://openai.com"
    }
    Success Response:
    {
        "id": 1,
        "name": "OpenAI",
        "websiteUrl": "https://openai.com"
    }
    Error Response:
    {
        "status": 400,
        "message": "Oops! Something's wrong with the data you sent.",
        "details": "Website URL must be a valid absolute URL."
    }
- Get all companies - GET/companies -  used to list all the companies created. 

Running the Project:

bash:
- git clone <your_repo_url>
- cd CompanyApi

Restore the dependencies:
- dotnet restore

Run the application:
- dotnet run

Access the API:
- Base URL: http:localhost:5000


TESTS:

This project includes a dedicated test project (CompanyApi.Tests) to ensure reliability and maintainability of the API. 

What's Covered:

Unit Tests:
- Validation Logic (CompanyValidatorTests.cs)
- Relevance Checker (RelevanceCheckerTests.cs)

API Tests:
- POST/companies with valid/invalid payloads. 
- GET/companies returns expected results

Error Handling Tests:
- Bad requests return 400 with helpful details
- Successful requests returns correct status codes. (201, 200)


Running The tests:

bash:
dotnet test

- This will run both the API and the test projects
- Run all unit and integration tests
- Show pass/fail results in the terminal 


This project is a starting point for building minimal APIs with .NET Core. It demonstrates how to structure the endpoints, and manage configurations in a clean and maintainable way. 


