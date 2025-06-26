using ReactTSWithNetCoreTemplate.API.Extensions;
using Serilog;
using Serilog.Enrichers.CallerInfo;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

builder.Host.UseSerilog();

var logFilePath = configuration.GetSection("AppSettings")["LogFilePath"];
var outputTemplate = "{Timestamp:yyyy-MM-ddTHH:mm:ss.fff} [{Level}] [{Namespace}.{Method}] {Message}{NewLine}{Exception}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithCallerInfo(includeFileInfo: true, assemblyPrefix: "ReactTSWithNetCoreTemplate.")
    .WriteTo.Console()
    .WriteTo.File($"{logFilePath}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSettings(configuration);
builder.Services.AddServices(configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

app.AddUsings();

app.Run();
