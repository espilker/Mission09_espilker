using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission09_espilker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_espilker
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BooksDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            //Corresponds to the wwwwroot
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("categorypage",
                    "{bookCategory}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                name: "Paging",
                pattern: "Page{pageNum}",
                defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category", "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

            

            endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
