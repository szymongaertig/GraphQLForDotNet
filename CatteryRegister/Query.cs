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
        [UseDbContext(typeof(CatteryDbContext))]
        [UsePaging]
        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Cat> GetCats([ScopedService] CatteryDbContext dbContext)
        {
            return dbContext.Set<Cat>()
                .Include(x => x.Litter).ThenInclude(x => x.Cattery);
        }

        [UseDbContext(typeof(CatteryDbContext))]
        public Task<Cat> GetCat(int id, [ScopedService] CatteryDbContext dbContext)
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

        public Cattery GetCattery()
        {
            return new Cattery()
            {
                Id = 11,
                Name = "TestCattery",
                OwnerId = 2
            };
        }

        [Authorize]
        public string? NotAnonymous()
        {
            return "ok";
        }
    }
}