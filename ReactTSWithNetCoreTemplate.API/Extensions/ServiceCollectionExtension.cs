using ReactTSWithNetCoreTemplate.Core.Settings;
using System.Reflection;

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

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    if (!isDevelopment)
                        context.ProblemDetails.Detail = null;
                };
            });
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
