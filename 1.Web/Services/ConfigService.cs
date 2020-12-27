using Microsoft.Extensions.Configuration;

namespace Web.Services
{
    public static class ConfigService
    {
        private static IConfiguration _configuration { get; set; }

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string DefaultConnection
        {
            get
            {
                return _configuration.GetConnectionString("DefaultConnection");
            }
        }
    }
}