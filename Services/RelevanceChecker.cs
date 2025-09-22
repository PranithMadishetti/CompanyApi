using CompanyApi.Models;
using System;
using System.Linq;

namespace CompanyApi.Services;

/// Provides methods to check the relevance of a company based on its name and website URL.
public static class RelevanceChecker
{   
    
    public static bool IsRelevant(Company company)
    {
        /// Checks if the company name is relevant to its website URL using multiple strategies
        return IsRelevantSimple(company)
            || IsRelevantFuzzy(company)
            || IsRelevantKeywords(company);
    }

    private static bool IsRelevantSimple(Company company)
    {
        /// Simple check: does the domain contain the company name?
        var domain = new Uri(company.WebsiteUrl).Host.ToLower();
        var name = company.Name.ToLower().Replace(" ", "");
        return domain.Contains(name);
    }

   private static bool IsRelevantFuzzy(Company company)
{
    /// Fuzzy check: uses Levenshtein distance to allow for minor differences
    var domain = new Uri(company.WebsiteUrl).Host.ToLower()
                    .Replace("www.", "")
                    .Replace(".com", "");

    var companyName = company.Name.ToLower().Replace(" ", "");

    /// Calculate edit distance
    int editDistance = LevenshteinDistance(domain, companyName);

    /// Calculate similarity ratio
    double similarityRatio = 1.0 - (double)editDistance / Math.Max(domain.Length, companyName.Length);

    /// Consider relevant if similarity is above threshold (e.g., 70%)
    return similarityRatio >= 0.7;
}

    /// / Computes the Levenshtein distance between two strings
    /// <param name="source"></param>
    /// <param name="target"></param>
private static int LevenshteinDistance(string source, string target)
{
    int sourceLength = source.Length;
    int targetLength = target.Length;

    /// Create a distance table
    int[,] distanceTable = new int[sourceLength + 1, targetLength + 1];

    /// Initialize the table
    for (int i = 0; i <= sourceLength; i++) distanceTable[i, 0] = i; /// Deleting all chars to match empty target
    for (int j = 0; j <= targetLength; j++) distanceTable[0, j] = j; ///

    /// Inserting all chars to match empty source
    for (int i = 1; i <= sourceLength; i++)
    {
        for (int j = 1; j <= targetLength; j++)
        {
            /// Calculate costs for deletion, insertion, and substitution
            int substitutionCost = (source[i - 1] == target[j - 1]) ? 0 : 1;

            distanceTable[i, j] = new[]
            {
                distanceTable[i - 1, j] + 1,           /// Deletion
                distanceTable[i, j - 1] + 1,           /// Insertion
                distanceTable[i - 1, j - 1] + substitutionCost /// Substitution
            }.Min();
        }
    }

    /// The distance is found in the bottom-right cell
    return distanceTable[sourceLength, targetLength];
}

    private static bool IsRelevantKeywords(Company company)
    {
        var domain = new Uri(company.WebsiteUrl).Host.ToLower();
        var words = company.Name.ToLower().Split(' ');
        return words.Any(word => domain.Contains(word));
    }
}