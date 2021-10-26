using System.Threading.Tasks;
using CatteryRegister.DataContext;
using CatteryRegister.Exceptions;
using CatteryRegister.Model;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CatteryRegister
{
    public class Mutation
    {
        public async Task<Cattery?> CreateCattery(string name,
            [Service] CatteryDbContext dbContext)
        {
            if (await dbContext.Set<Cattery>().AnyAsync(x => x.Name == name))
                throw new BusinessException("cattery-name-already-used");
            var result = (await dbContext.AddAsync(new Cattery
            {
                Name = name
            })).Entity;

            await dbContext.SaveChangesAsync();
            return result;
        }

        [Authorize]
        public string DeleteCattery(int id, [Service] UserContext context)
        {
            if (!context.IsAdmin() && !context.IsCatteryOwner(id))
                throw new BusinessException("forbidden");
            return "true";
        }
    }
}