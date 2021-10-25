using System;

namespace CatteryRegister.Model
{
    public class Litter
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public Cattery Cattery { get; set; }
    }
}