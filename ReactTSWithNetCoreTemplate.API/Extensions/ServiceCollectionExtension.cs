using System.Reflection;
using ReactTSWithNetCoreTemplate.Core.Settings;

namespace ReactTSWithNetCoreTemplate.API.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsTypes = Assembly.GetAssembly(typeof(AppSettings)).GetTypes()
                .Where(w => w.Name.EndsWith("Settings"))
                .ToList();

            foreach (var settingsType in settingsTypes)
            {
                var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethod("Configure", new[] { typeof(IServiceCollection), typeof(IConfiguration) })
                    .MakeGenericMethod(settingsType);

                configureMethod.Invoke(null, new object[] { services, configuration.GetSection(settingsType.Name) });
            }

            return services;
        }
    }
}
