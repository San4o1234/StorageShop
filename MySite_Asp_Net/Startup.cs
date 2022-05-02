using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;        // ��� ������ � ��������������.
using Microsoft.EntityFrameworkCore;
using MySite_Asp_Net.Models;
using Microsoft.EntityFrameworkCore.Proxies;    //��� ��������� � ������ ������ �� ��������� ��(���� �� ��������, ������ �� ��������).
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    // ��� ������ � Identity
using Microsoft.AspNetCore.Identity;            // ��� ������ � Identity

namespace MySite_Asp_Net
{
    public class Startup
    {
        public IConfiguration Configuration { get; }    // ���������� ��� ������� �� �� � �������� �������.
        public Startup(IConfiguration configuration)        // ��� ������ � ��������������.
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
           // string connection = Configuration.GetConnectionString("DefaultSqlConnection");
            services.AddMvc();  // ��������� ���� ��� ������.
            services.AddControllersWithViews();
            // ������� ��� � StorageContext.
            //services.AddDbContext<StorageContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));
            services.AddEntityFrameworkSqlServer();
            services.AddRazorPages();
            // Proxy - ��� ��������� � ������ ������ �� ��������� ��(���� �� ��������, ������ �� ��������).
            services.AddDbContext<StorageContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(o => { o.LoginPath = new PathString("/Account/Login"); 
                                o.AccessDeniedPath = new PathString("/Account/Login"); });  // ��� ��������������.
           
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // �� application ������� �������� ���� UseRouting.
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();  // ��� ������ � Razor Page i View. 

                endpoints.MapControllerRoute(
                    name: "default",                 // ��� �����
                    pattern: "{controller=MainPage}/{action=ViewStart}/{id?}"       // ���� �� ����� �� �� ����������
                    );
            });
        }
    }
}
