using CompanyApi.Models;
using CompanyApi.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Company> companies = new(); /// In-memory storage for companies
int nextId = 1; /// Simple ID generator

app.MapGet("/", () => "Welcome to the CompanyAPI"); /// Root endpoint

// POST /companies
/// Accepts a Company object in the request body
/// Validates the object and adds it to the in-memory list if valid
/// Returns 400 Bad Request with error details if validation fails
app.MapPost("/companies", (Company company) =>
{
    var (isCompanyValid, validationMessage) = CompanyValidator.Validate(company);

    if (!isCompanyValid)
    {
        return Results.BadRequest(new
        {
            Status = 400,
            Message = "Oops! Something's wrong with the data you sent.",
            Details = validationMessage
        });
    }

    company.Id = nextId++;
    companies.Add(company);

    return Results.Created($"/companies/{company.Id}", company);
});

// GET /companies
/// Returns the list of all stored companies
app.MapGet("/companies", () => companies);

app.Run(); /// Start the application
