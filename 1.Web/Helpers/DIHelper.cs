using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Helpers
{
    public static class DIHelper
    {
        /// <summary>注入服務</summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly"></param>
        /// <param name="implementAssembly"></param>
        public static void AddScoped(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface);
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x) && (!x.IsInterface));
                if (type != null)
                {
                    services.AddScoped(item, type);
                }
            }
        }

        /// <summary>注入服務</summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly"></param>
        /// <param name="implementAssembly"></param>
        public static void AddSingleton(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface);
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x) && (!x.IsInterface));
                if (type != null)
                {
                    services.AddSingleton(item, type);
                }
            }
        }

        /// <summary>注入服務</summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly"></param>
        /// <param name="implementAssembly"></param>
        public static void AddTransient(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface);
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x) && (!x.IsInterface));
                if (type != null)
                {
                    services.AddTransient(item, type);
                }
            }
        }
    }
}
