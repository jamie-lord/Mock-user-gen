using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Bogus;
using CsvHelper;

namespace MfAccountUserMockGen
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 1000;

            var users = new List<User>();

            var faker = new Faker<User>()
                .RuleFor(u => u.Email, s => $"{Guid.NewGuid()}-{s.Person.Email}")
                .RuleFor(u => u.FirstName, s => s.Person.FirstName)
                .RuleFor(u => u.Surname, s => s.Person.LastName)
                .RuleFor(u => u.ContactNumber, s => s.Person.Phone)
                .RuleFor(u => u.StartDate, s => s.Date.Past(100).ToShortDateString());

            users.AddRange(faker.Generate(count));

            using (var writer = new StreamWriter($"{count}_users.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string StartDate { get; set; }
        public string ContactNumber { get; set; }
    }
}
