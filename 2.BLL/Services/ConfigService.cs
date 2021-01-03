using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public static class ConfigService
    {
        private static IConfiguration Configuration { get; set; }

        public static void SetConfig(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string GetDefaultConnection()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }
    }
}