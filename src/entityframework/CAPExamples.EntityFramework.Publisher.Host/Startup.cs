using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPExamples.EntityFramework.Abstractions.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CAPExamples.EntityFramework.Publisher.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationAssemblyName = typeof(Startup).GetType().Namespace;
            services.AddDbContext<AppDbContext>(builder => 
                builder.UseSqlServer(connectionString, options=>
                    options.MigrationsAssembly(migrationAssemblyName)
            ));

            services.AddCap(options =>
            {
                options.UseEntityFramework<AppDbContext>(efOptions =>
                {
                    efOptions.Schema = DotNetCore.CAP.EFOptions.DefaultSchema;
                });

                options.UseRabbitMQ(mqOptions =>
                {
                    mqOptions.HostName = "localhost";
                    mqOptions.UserName = "guest";
                    mqOptions.Password = "guest";
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCap();

            app.UseMvcWithDefaultRoute();
        }
    }
}
