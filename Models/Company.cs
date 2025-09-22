namespace CompanyApi.Models;

/// Represents a company entity.
public record Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
}
