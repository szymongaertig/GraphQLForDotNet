using System.Threading.Tasks;
using CatteryRegister.DataContext;
using CatteryRegister.Exceptions;
using CatteryRegister.Model;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;

namespace CatteryRegister
{
    public class Mutation
    {
        public async Task<Cattery?> CreateCattery(string name,
            [Service] CatteryDbContext dbContext)
        {
            switch (name)
            {
                case "already_used":
                    throw new BusinessException("cattery-with-given-name-exists");
                    break;
            }

            return new Cattery()
            {
                Name = name
            };
        }

        [Authorize]
        public string DeleteCattery(int id, [Service]UserContext context)
        {
            if (!context.IsAdmin() && !context.IsCatteryOwner(id))
                throw new BusinessException("forbidden");
            return "true";
        }
    }
}