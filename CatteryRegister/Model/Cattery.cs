using System.Collections.Generic;

namespace CatteryRegister.Model
{
    public class Cattery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public ICollection<Litter>? Litters { get; set; }
    }
}