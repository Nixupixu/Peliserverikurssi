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

using projectapi.Middlewares;
using projectapi.Processors;
using projectapi.Repositories;
using projectapi.Controllers;
using projectapi.MongoDB;
using projectapi.Models;

namespace projectapi
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
            services.AddSingleton<CharacterProcessor>();
            //services.AddSingleton<ItemProcessor>();
            //services.AddSingleton<IPlayerRepository, PlayerInMemoryRepository>();
            services.AddSingleton<MongoDBClient>();
            services.AddSingleton<IPlayerRepository, MongoDBRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
