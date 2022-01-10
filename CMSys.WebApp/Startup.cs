using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSys.Infrastructure;
using CMSys.Core;
using CMSys.Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace CMSys.WebApp
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration config) => Config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UnitOfWorkOptions>(x => new UnitOfWorkOptions
            {
                ConnectionString = "Data Source=(local);Initial Catalog=CMSys;Integrated Security=True"
            });
            //services.Configure<UnitOfWorkOptions>(Config.GetSection("UnitOfWork"));
            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/login";
                    //options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
        }
    }
}
