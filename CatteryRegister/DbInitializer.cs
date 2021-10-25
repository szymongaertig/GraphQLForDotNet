using System.Formats.Asn1;
using System.Threading;
using System.Threading.Tasks;
using CatteryRegister.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CatteryRegister
{
    public class DbInitializer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DbInitializer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<CatteryDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}