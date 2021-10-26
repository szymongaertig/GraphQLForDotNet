using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("pii",
                (sp, client) => { client.BaseAddress = new Uri("https://localhost:5002/graphql"); });
            services.AddHttpClient("cat",
                (sp, client) => { client.BaseAddress = new Uri("https://localhost:5003/graphql"); });

            services.AddGraphQLServer()
                .AddRemoteSchema("pii", ignoreRootTypes: true)
                .AddRemoteSchema("cat", ignoreRootTypes: false)
                .AddTypeExtensionsFromFile("Stitching.Graphql");
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