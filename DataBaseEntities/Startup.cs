using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using DataBaseEntities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseEntities.Data.Seeder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using DataBaseEntities.Models;

namespace DataBaseEntities
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpContextAccessor();
            //services.AddSession();

            services.AddRazorPages();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISeeder,DbSeeder>();

           // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddIdentity<ApplicationUSer, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors(
              x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials


            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ISeeder>().Seed();
            }
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapRazorPages();
                
            });
        }
    }

    
}