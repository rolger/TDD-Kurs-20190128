using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Castle.Core.Internal;

namespace GarbageServiceExercise
{
    public class Company
    {
        public Company(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<string> Garbage { get; set; }
    }

    public interface ICompanyRepository
    {
        List<Company> FindBy(string keyword);
        void SaveNewCompany(string name, List<string> garbage);
    }

    public class SearchEngine
    {
        private readonly ICompanyRepository repo;
        private readonly GarbageParser parser;

        public SearchEngine(GarbageParser parser, ICompanyRepository repo)
        {
            this.parser = parser;
            this.repo = repo;
        }

        public FoundCompanies Search(string input)
        {
            var foundCompanies = new FoundCompanies();
            if (input.IsNullOrEmpty())
            {
                return foundCompanies;
            }

            var parsedInput = parser.Parse(input);
            var companies = new List<Company>();
            foreach (var keyword in parsedInput)
            {
                companies.AddRange(repo.FindBy(keyword));     
            }

            foundCompanies.Names = companies.Select(x => x.Name).ToList();
            return foundCompanies;
        }

    }
}
