using MarketDataService;
using MarketDataService.Helpers.AssetHistoricalDataHelper;
using MarketDataService.Models.AppSettings;
using MarketDataService.Repositories.AssetRepositories.Implementations;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Repositories.UserRepositories.Implementations;
using MarketDataService.Repositories.UserRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Impelementations;
using MarketDataService.Services.AssetServices.Interfaces;
using MarketDataService.Services.HTMLTemplateServices.Impelementations;
using MarketDataService.Services.HTMLTemplateServices.Interfaces;
using MarketDataService.Services.NotificationServices.Implemenetations;
using MarketDataService.Services.NotificationServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ProjectX.Data.Contexts;
using ProjectX.Models.Options;
using ProjectX.Services.EmailService.Implementation;
using ProjectX.Services.EmailService.Interface;
using Quartz;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddOptions<ConnectionSettings>()
    .BindConfiguration(nameof(ConnectionSettings))
    .ValidateDataAnnotations();

builder.Services.AddOptions<MarketDataService.Models.AppSettings.Quartz>()
    .BindConfiguration(nameof(MarketDataService.Models.AppSettings.Quartz))
    .ValidateDataAnnotations();

builder.Services.AddOptions<ApiSettings>()
    .BindConfiguration(nameof(ApiSettings))
    .ValidateDataAnnotations();

builder.Services.AddOptions<EmailOptions>()
    .BindConfiguration(nameof(EmailOptions))
    .ValidateDataAnnotations();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
    options.UseNpgsql(connectionString);
    options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());


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


builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetCategoryRepository, AssetCategoryRepository>();
builder.Services.AddScoped<IAssetHistoricalRepository, AssetHistoricalRepository>();
builder.Services.AddScoped<INewAssetRepository, NewAssetRepository>();
builder.Services.AddScoped<IExcludedAssetRepository, ExcludedAssetRepository>();
builder.Services.AddScoped<IAssetHistoricalDataRepository, AssetHistoricalDataRepository>();
builder.Services.AddScoped<IUserSettingRepository, UserSettingRepository>();
builder.Services.AddScoped<IAssetNotificationQueueRepository, AssetNotificationQueueRepository>();
builder.Services.AddScoped<IUserSettingAssetNotificationQueueRepository, UserSettingAssetNotificationQueueRepository>();

builder.Services.AddSingleton<IAssetCategoryService, AssetCategoryService>();
builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddSingleton<IAssetControlService, AssetControlService>();
builder.Services.AddSingleton<IAssetHistoricalService, AssetHistoricalService>();
builder.Services.AddSingleton<IAssetHistoricalDataService, AssetHistoricalDataService>();
builder.Services.AddSingleton<INotificationChangedAssetsService, NotificationChangedAssetsService>();
builder.Services.AddSingleton<IHtmlTemplateService, HtmlTemplateService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IUpdateAssetHistoricalDataService, UpdateAssetHistoricalDataService>();
builder.Services.AddSingleton<IOptimizeAssetHistoricalDataService, OptimizeAssetHistoricalDataService>();
builder.Services.AddSingleton<IAssetHistoricalDataHelper, AssetHistoricalDataHelper>();



builder.Services.AddHostedService<Worker>();


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(logger);


IHost host = builder.Build();

logger.Information("Application MarketDataService started");

host.Run();