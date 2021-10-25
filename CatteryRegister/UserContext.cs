using System.Linq;

namespace CatteryRegister
{
    public class UserContext
    {
        public string[] Permissions { get; set; }

        public bool IsCatteryOwner(int id)
        {
            return Permissions != null
                   && Permissions.Any(x => x.Contains($"owner:{id}"));
        }

        public bool IsAdmin()
        {
            return Permissions != null && Permissions.Contains("admin");
        }
    }
}