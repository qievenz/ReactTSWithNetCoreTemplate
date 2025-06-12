using ReactTSWithNetCoreTemplate.API.Extensions;
using Serilog;
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
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

        if (!builder.Environment.IsDevelopment())
            context.ProblemDetails.Detail = null;
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Services

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
