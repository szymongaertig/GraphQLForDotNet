using System;
using System.Linq;
using HotChocolate.Data;
using PII.Model;

namespace PII
{
    public class Query
    {
        private static User[] _users =
        {
            new User(Guid.Parse("369164dd-be3b-41b2-9c75-d1cf6fe79c8b"), "Grzegorz", "Brzęczyszczykiewicz"),
            new User(Guid.Parse("98aa7676-8d0c-447b-8590-aececfb4d842"), "Memphis", "Raines"),
            new User(Guid.Parse("6b99c185-dd65-4e3f-a204-32a0a7819e60"), "Marion", "Cabretti"),
            new User(Guid.Parse("5598e809-89b9-480a-8f57-b8e1ce833563"), "John", "Matrix"),
            new User(Guid.Parse("2cabd14a-8a5e-46a1-8a23-1e54c80926a6"), "Ivan", "Danko"),
            new User(Guid.Parse("1643eeab-c564-4d73-8ffb-5d57cc5ec807"), "Lee", "Christms"),
            new User(Guid.Parse("52e943e6-aa54-4604-b7f2-b432bf227233"), "Yin", "Yang")
        };

        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers() => _users.AsQueryable();

        public User? GetUser(Guid userId) => _users.FirstOrDefault(x => x.Id == userId);
    }
}