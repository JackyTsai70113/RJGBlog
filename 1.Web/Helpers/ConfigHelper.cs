using Microsoft.Extensions.Configuration;

namespace Web.Services
{
    public static class ConfigHelper
    {
        private static IConfiguration Configuration { get; set; }

        public static void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string Redis_ConnectionString
        {
            get
            {
                return Configuration.GetSection("Redis")["ConnectionString"];
            }
        }
    }
}