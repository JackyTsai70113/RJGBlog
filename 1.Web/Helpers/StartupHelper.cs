using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Helpers
{
    public static class StartupHelper
    {
        public static void SetMenuList(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //Back 後台
                options.AddPolicy("BackHome",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "BackHomeAllOK"));
                options.AddPolicy("BackRole",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "BackRoleAllOK"));

                //NoBack 前台
                options.AddPolicy("Privacy",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "PrivacyAllOK"));
                options.AddPolicy("Introduce",
                    policy => policy.RequireClaim(ClaimTypes.Authentication, "introduceOK"));
            });
        }
    }
}
