using Serilog;
using Serilog.Events;
using Template.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

builder.Host.UseSerilog();

var logFilePath = configuration.GetSection("AppSettings")["LogFilePath"];

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File($"{logFilePath}/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSettings(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
