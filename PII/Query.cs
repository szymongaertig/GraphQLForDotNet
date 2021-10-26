using System.Linq;
using PII.Model;

namespace PII
{
    public class Query
    {
        private static User[] _users =
        {
            new User(1, "Grzegorz", "Brzęczyszczykiewicz"),
            new User(2, "Memphis", "Raines"),
            new User(3, "Marion", "Cabretti"),
            new User(4, "John", "Matrix"),
            new User(5, "Ivan", "Danko"),
            new User(6, "Lee", "Christms"),
            new User(7, "Yin", "Yang")
        };

        public User? GetUserById(int userId) => _users.FirstOrDefault(x => x.Id == userId);
    }
}