using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Web.Helpers
{
    public static class StartupHelper
    {
        public static void SetMenuList(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BackHome",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "backhomeOK"));
                options.AddPolicy("Privacy",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "privacyOK"));
                options.AddPolicy("Introduce",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "introduceOK"));
            });
        }
    }
}
