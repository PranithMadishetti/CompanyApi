using CompanyApi.Models;
using CompanyApi.Services;
using Xunit;

namespace CompanyApi.Tests
{
    /// Tests for the RelevanceChecker service
    public class RelevanceCheckerTests
    {
        [Fact]

        /// Test for simple relevance check
        public void SimpleRelevance_ShouldPass_WhenNameMatchesDomain() /// Test for simple relevance check
        {
            /// Arrange
            var company = new Company
            {
                Name = "Google",
                WebsiteUrl = "https://www.google.com"
            };

            /// Act
            var result = RelevanceChecker.IsRelevant(company);

            /// Assert
            Assert.True(result, "Expected relevance check to pass for matching name and domain.");
        }

        [Fact]

        /// Test for simple relevance check failure
        public void SimpleRelevance_ShouldFail_WhenNameDoesNotMatchDomain()
        {
            // Arrange
            var company = new Company
            {
                Name = "Microsoft",
                WebsiteUrl = "https://www.google.com"
            };

            /// Act
            var result = RelevanceChecker.IsRelevant(company);

            /// Assert
            Assert.False(result, "Expected relevance check to fail when company name is unrelated to website.");
        }

        [Fact]
        /// Test for fuzzy relevance check
        public void FuzzyRelevance_ShouldPass_ForSlightlyDifferentNames()
        {
            /// Arrange
            var company = new Company
            {
                Name = "Gooogle",
                WebsiteUrl = "https://www.google.com"
            };

            /// Act
            var result = RelevanceChecker.IsRelevant(company);

            /// Assert
            Assert.True(result, "Fuzzy relevance should pass for small typos in company name.");
        }

        [Fact]
        /// Test for fuzzy relevance check failure
        public void KeywordRelevance_ShouldPass_WhenAnyWordMatchesDomain()
        {
            /// Arrange
            var company = new Company
            {
                Name = "Google Search",
                WebsiteUrl = "https://www.google.com"
            };

            /// Act
            var result = RelevanceChecker.IsRelevant(company);

            /// Assert
            Assert.True(result, "Keyword relevance should pass if any word in the company name appears in the domain.");
        }

        [Fact]
        /// Test for keyword relevance failure
        public void KeywordRelevance_ShouldFail_WhenNoWordMatchesDomain()
        {
            /// Arrange
            var company = new Company
            {
                Name = "Microsoft Office",
                WebsiteUrl = "https://www.google.com"
            };

            /// Act
            var result = RelevanceChecker.IsRelevant(company);

            /// Assert
            Assert.False(result, "Expected keyword relevance to fail when no words match the website domain.");
        }
    }
}
