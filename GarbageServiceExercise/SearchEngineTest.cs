using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GarbageServiceExercise
{
    public class SearchEngineTest
    {
        [Fact]
        public void SearchEmptyStringFindsNoCompanies()
        {
            SearchEngine searchEngine = new SearchEngine(null, null);

            var companies = searchEngine.Search(string.Empty);

            companies.Names.Count.Should().Be(0);
        }

        [Fact]
        public void ShouldReturnOneCompanyForGivenSearchKeyword()
        {
            var parser = new Mock<GarbageParser>();
            parser.Setup(x => x.Parse("bottle")).Returns(new List<string>{"BOTTLE"});

            var companyRepo = new Mock<ICompanyRepository>();
            List<Company> expectedCompanies = new List<Company>{new Company("C1") };
            companyRepo.Setup(x => x.FindBy("BOTTLE")).Returns(expectedCompanies);

            SearchEngine searchEngine = new SearchEngine(parser.Object, companyRepo.Object);
            var companies = searchEngine.Search("bottle");

            companies.Names.Should().BeEquivalentTo("C1");
        }

        [Fact]
        public void ShouldReturnTwoCompaniesForTwoGivenSearchKeywords()
        {
            var parserStub = new Mock<GarbageParser>();
            parserStub.Setup(x => x.Parse("bottle, vienna")).Returns(new List<string> { "BOTTLE", "VIENNA" });
            GarbageParser parser = parserStub.Object;

            var companyRepoStub = new Mock<ICompanyRepository>();
            companyRepoStub.Setup(x => x.FindBy("BOTTLE")).Returns(new List<Company> { new Company("C1") });
            companyRepoStub.Setup(x => x.FindBy("VIENNA")).Returns(new List<Company> { new Company("C2") });
            ICompanyRepository repo = companyRepoStub.Object;

            SearchEngine searchEngine = new SearchEngine(parser, repo);
            var companies = searchEngine.Search("bottle, vienna");

            companies.Names.Should().BeEquivalentTo("C1", "C2");
        }

    }
}