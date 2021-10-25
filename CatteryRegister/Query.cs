using System.Linq;
using CatteryRegister.DataContext;
using CatteryRegister.Model;
using HotChocolate;
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
    }
}