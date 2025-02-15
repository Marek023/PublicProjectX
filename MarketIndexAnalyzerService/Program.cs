using MarketIndexAnalyzer;
using MarketIndexAnalyzer.Data.Context;
using MarketIndexAnalyzer.Models.AppSettings;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Implementations;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;
using MarketIndexAnalyzer.Services.Calculations.Implementations;
using MarketIndexAnalyzer.Services.Calculations.Interfaces;
using MarketIndexAnalyzer.Services.IndexServices.Implementations;
using MarketIndexAnalyzer.Services.IndexServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddOptions<ApiSettings>()
    .BindConfiguration(nameof(ApiSettings))
    .ValidateDataAnnotations();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
    options.UseNpgsql(connectionString);
    options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());


builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(build =>
    {
        build.WithOrigins("https://trusted-origin.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<INasdaqISharesService, NasdaqISharesService>();
builder.Services.AddSingleton<ICalculationOfSlumps, CalculationOfSlumps>();

builder.Services.AddScoped<IIndexDataRepository, IndexDataRepository>();
builder.Services.AddScoped<IIndexTypeRepository, IndexTypeRepository>();



builder.Services.AddHostedService<Worker>();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

var host = builder.Build();

logger.Information("Application MarketDataAnalyzerService started");

host.Run();