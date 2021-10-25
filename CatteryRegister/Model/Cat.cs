using System;

namespace CatteryRegister.Model
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Litter? Litter { get; set; }
        public Guid ImageId { get; set; }
    }
}