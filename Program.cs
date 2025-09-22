using CompanyApi.Models;
using CompanyApi.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Company> companies = new(); /// In-memory storage for companies
int nextId = 1; /// Simple ID generator
var validator = new CompanyValidator(companies); 

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

// GET /companies/search?name=googl
app.MapGet("/companies/search", (string name) =>
{
    if (string.IsNullOrWhiteSpace(name))
        return Results.BadRequest(new
        {
            Status = 400,
            Message = "Please provide a valid name to search."
        });

    var results = companies
        .Where(c => c.Name.Trim().Contains(name.Trim(), StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (!results.Any())
    {
        return Results.BadRequest(new
        {
            Message = "No company available"
        });
    }

    // If found, return only the company names
    var companyNames = results.Select(c => c.Name);
    return Results.Ok(new
    {
        Companies = companyNames
    });
});

app.Run(); /// Start the application