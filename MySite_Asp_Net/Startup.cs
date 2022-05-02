using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;        // для роботи з конфігураціями.
using Microsoft.EntityFrameworkCore;
using MySite_Asp_Net.Models;
using Microsoft.EntityFrameworkCore.Proxies;    //для установки і витягу звязків між таблицями БД(один до багатьох, багато до багатьох).
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    // для роботи з Identity
using Microsoft.AspNetCore.Identity;            // для роботи з Identity

namespace MySite_Asp_Net
{
    public class Startup
    {
        public IConfiguration Configuration { get; }    // властивість для доступу до БД з запуском проекту.
        public Startup(IConfiguration configuration)        // для роботи з конфігураціями.
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
           // string connection = Configuration.GetConnectionString("DefaultSqlConnection");
            services.AddMvc();  // загальний клас для всього.
            services.AddControllersWithViews();
            // передаєм дані в StorageContext.
            //services.AddDbContext<StorageContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));
            services.AddEntityFrameworkSqlServer();
            services.AddRazorPages();
            // Proxy - для установки і витягу звязків між таблицями БД(один до багатьох, багато до багатьох).
            services.AddDbContext<StorageContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(o => { o.LoginPath = new PathString("/Account/Login"); 
                                o.AccessDeniedPath = new PathString("/Account/Login"); });  // для автентифікації.
           
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Всі application потрібно додавати після UseRouting.
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();  // для роботи з Razor Page i View. 

                endpoints.MapControllerRoute(
                    name: "default",                 // Імя шляху
                    pattern: "{controller=MainPage}/{action=ViewStart}/{id?}"       // шлях до обєкту що має виконатися
                    );
            });
        }
    }
}
