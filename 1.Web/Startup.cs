using DAL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.DA.Interfaces;
using System.Reflection;
using Core.Helpers;
using Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigService.SetConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RJGDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RJGDbContext>();

            DIService.AddTransient(services, Assembly.Load("BLL"), Assembly.Load("BLL"));
            DIService.AddTransient(services, Assembly.Load("DAL"), Assembly.Load("DAL"));

            services.AddSingleton<RedisHelper>();
            services.AddSingleton<JwtHelper>();

            services.AddControllersWithViews();

            services.AddSession();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                    options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // 一般我們都會驗證 Issuer
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                        // 通常不太需要驗證 Audience
                        ValidateAudience = false,
                        //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,

                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = false,

                        // "1234567890123456" 應該從 IConfiguration 取得
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication(); // 驗證
            app.UseAuthorization(); // 授權

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                             name: "areas",
                                             pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                                             name: "default",
                                             pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
