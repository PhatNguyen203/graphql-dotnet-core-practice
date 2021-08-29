using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLPractice.GraphQL;
using GraphQLPractice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Server.Ui.Voyager;
using GraphQLPractice.GraphQL.Platforms;
using GraphQLPractice.GraphQL.Commands;

namespace GraphQLPractice
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlConn")));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<PlatformType>()
                .AddType<AddPlatformInputType>()
                .AddType<AddPlatformPayloadType>()
                .AddType<CommandType>()
                .AddType<AddCommandInputType>()
                .AddType<AddCommandPayloadType>()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            
            app.UseGraphQLVoyager("/ui/graphql-voyager");
        }
    }
}
