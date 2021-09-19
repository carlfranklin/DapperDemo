using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            
            services.AddSingleton<DapperRepository<Customer>>(s =>
                new DapperRepository<Customer>(
                    new DataContext<Customer>(Configuration.GetConnectionString("ChinnokConnectionString")),
                    Configuration.GetConnectionString("ChinnokConnectionString")));

            services.AddSingleton<DapperRepository<Instrument>>(s =>
                new DapperRepository<Instrument>(
                    new DataContext<Instrument>(Configuration.GetConnectionString("BandBookerConnectionString")),
                    Configuration.GetConnectionString("BandBookerConnectionString")));

            services.AddSingleton<DapperRepository<Customers>>(s =>
                new DapperRepository<Customers>(
                    new DataContext<Customers>(Configuration.GetConnectionString("NorthwindConnectionString")),
                    Configuration.GetConnectionString("NorthwindConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
