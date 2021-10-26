using System.Linq;
using System.Threading.Tasks;
using CatteryRegister.DataContext;
using CatteryRegister.Model;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace CatteryRegister
{
    public class Query
    {
        [UsePaging]
        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Cat> GetCats([Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cat>()
                .Include(x => x.Litter).ThenInclude(x => x.Cattery);
        }

        public Task<Cat> GetCat(int id, [Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cat>()
                .Include(x => x.Litter)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        [UseFiltering]
        public IQueryable<Cattery> GetCatteries([Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cattery>();
        }

        [Authorize]
        public string? NotAnonymous()
        {
            return "ok";
        }
    }
}