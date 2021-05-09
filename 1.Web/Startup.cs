using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core.Data;
using System;
using Core.Helpers;
using System.Reflection;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //ConfigService.SetConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RJGDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<RJGDbContext>();

            DIHelper.AddTransient(services, Assembly.Load("BLL"), Assembly.Load("BLL"));
            DIHelper.AddTransient(services, Assembly.Load("DAL"), Assembly.Load("DAL"));
            DIHelper.AddTransient(services, Assembly.Load("Web"), Assembly.Load("Web"));
            services.AddSingleton<RedisHelper>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.ConfigureIdentity();

            services.ConfigureApplicationCookie(options => {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //設定Menu權限
            Helpers.StartupHelper.SetMenuList(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // 驗證
            app.UseAuthorization(); // 授權

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

    public static class ServicesExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options => {
                // Password settings.
                options.Password.RequireDigit = true; // 密碼中需要0-9 之間的數位
                options.Password.RequireNonAlphanumeric = false; //密碼中需要非英數位元
                options.Password.RequireUppercase = false; //密碼中需要有大寫字元
                options.Password.RequireLowercase = true; //密碼中需要有小寫字元
                options.Password.RequiredLength = 6; //密碼的最小長度
                options.Password.RequiredUniqueChars = 0; //需要密碼中的相異字元數目

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}