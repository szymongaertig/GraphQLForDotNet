using System;

namespace PII.Model
{
    public class User
    {
        public User(Guid id, string firstname, string surname)
        {
            Id = id;
            Firstname = firstname;
            Surname = surname;
        }

        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}