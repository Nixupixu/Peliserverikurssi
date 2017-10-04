using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using gameapi.Middlewares;
using gameapi.Processors;
using gameapi.Repositories;
using gameapi.Controllers;
using gameapi.MongoDB;
using gameapi.Models;

namespace gameapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthKey>(Configuration);

            services.AddMvc();
            
            services.AddSingleton<PlayerProcessor>();
            services.AddSingleton<ItemProcessor>();
            //services.AddSingleton<IPlayerRepository, PlayerInMemoryRepository>();
            services.AddSingleton<MongoDBClient>();
            services.AddSingleton<IPlayerRepository, MongoDBRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
