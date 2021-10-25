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
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Cat> GetCats([Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cat>()
                .AsNoTracking();
        }

        public Task<Cat> GetCat(int id, [Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cat>()
                .Include(x => x.Litter)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        [UseFiltering]
        public IQueryable<Cattery> GetCatteries([Service] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cattery>()
                .AsNoTracking();
        }

        [Authorize]
        public string? NotAnonymous()
        {
            return "ok";
        }
    }
}