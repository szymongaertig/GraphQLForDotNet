using CatteryRegister.DataContext;
using CatteryRegister.Model;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatteryRegister
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<CatteryDbContext>(b =>
                b.UseNpgsql(GetConnectionString()));
            services.AddDbContext<CatteryDbContext>(b =>
                b.UseNpgsql(GetConnectionString()));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<CatType>()
                .AddType<ParentType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .SetPagingOptions(new PagingOptions
                {
                    IncludeTotalCount = true
                });

            services.AddHostedService<DbInitializer>();
        }

        private string GetConnectionString()
        {
            var result =  _configuration.GetConnectionString("PostgreSqlConnectionString");
            return result;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
        }
    }
}