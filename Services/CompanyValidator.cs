using System.Reflection.Metadata.Ecma335;
using CompanyApi.Models;

namespace CompanyApi.Services;


/// Validates Company objects for correct name length, URL format, and relevance

public class CompanyValidator
{
    public static (bool IsValid, string ErrorMessage) Validate(Company company) /// Validates the company object
    {

        if (string.IsNullOrWhiteSpace(company.Name) || company.Name.Length < 3)
            return (false, "Company name is too short, please use at least 3 characters."); /// Name validation

        if (string.IsNullOrWhiteSpace(company.WebsiteUrl) ||
            !Uri.TryCreate(company.WebsiteUrl, UriKind.Absolute, out var uriResult) ||
            !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            return (false, "Website URL doesn't look right. Make sure it starts with http:// or https://."); /// URL validation

        if (!RelevanceChecker.IsRelevant(company))
            return (false, "Hmm, the company name doesn't seem related to the website you provided."); /// Relevance check

        return (true, string.Empty); /// Valid company
    }

    private readonly List<Company> comp;

    public CompanyValidator(List<Company> companies)
    {
        comp = companies;
    }

    public IEnumerable<Company> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Enumerable.Empty<Company>();

        return comp
            .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}