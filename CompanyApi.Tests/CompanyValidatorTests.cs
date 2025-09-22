using CompanyApi.Models;
using CompanyApi.Services;
using Xunit;

public class CompanyValidatorTests
{
    [Fact]
    /// Test for too short company name
    public void ShortName_ShouldFail()
    {
        var company = new Company { Name = "AB", WebsiteUrl = "https://abc.com" };
        var (valid, _) = CompanyValidator.Validate(company);
        Assert.False(valid);
    }

    [Fact]
    /// Test for invalid URL format
    public void InvalidUrl_ShouldFail()
    {
        var company = new Company { Name = "MyCompany", WebsiteUrl = "bad-url" };
        var (valid, _) = CompanyValidator.Validate(company);
        Assert.False(valid);
    }

    [Fact]
    /// Test for irrelevant company name and URL
    public void Irrelevant_ShouldFail()
    {
        var company = new Company { Name = "Microsoft", WebsiteUrl = "https://www.google.com" };
        var (valid, _) = CompanyValidator.Validate(company);
        Assert.False(valid);
    }

    [Fact]
    /// Test for valid company data
    public void ValidCompany_ShouldPass()
    {
        var company = new Company { Name = "Google", WebsiteUrl = "https://www.google.com" };
        var (valid, _) = CompanyValidator.Validate(company);
        Assert.True(valid);
    }
}
