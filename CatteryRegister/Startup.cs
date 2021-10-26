using System;
using CatteryRegister.DataContext;
using CatteryRegister.Exceptions;
using CatteryRegister.Model;
using CatteryRegister.Services;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Authentication;
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

            services.AddTransient<ParentsService>();
            services.AddHttpClient("pii",
                (sp, client) => { client.BaseAddress = new Uri("https://localhost:5002/graphql"); });


            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<CatType>()
                .AddType<ParentType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddDataLoader<ParentsDataLoader>()
                .SetPagingOptions(new PagingOptions
                {
                    IncludeTotalCount = true
                });
            
            services.AddErrorFilter<GraphQlErrorFilter>();
            services.AddSingleton<RandomCatImageClient>();
            services.AddHostedService<DbInitializer>();
            services.AddAuthorization();
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme
                        = CatteryAuthenticationHandler.Schema;
                })
                .AddScheme<AuthenticationSchemeOptions, CatteryAuthenticationHandler>
                    (CatteryAuthenticationHandler.Schema, op => { });
            services.AddScoped<UserContext>();
        }

        private string GetConnectionString()
        {
            var result = _configuration.GetConnectionString("PostgreSqlConnectionString");
            return result;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<AuthMiddleware>()
                .UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
        }
    }
}