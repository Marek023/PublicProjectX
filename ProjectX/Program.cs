using ProjectX.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using ProjectX.Helpers.Assets.DataForWeb.Implementations;
using ProjectX.Helpers.Assets.DataForWeb.Interfaces;
using ProjectX.Models.Options;
using ProjectX.Repositories.Assets.Implementations;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Repositories.Statistics.Implementations;
using ProjectX.Repositories.Statistics.Interfaces;
using ProjectX.Services.Calculators.Implementations;
using ProjectX.Services.Calculators.Interfaces;
using ProjectX.Services.DowJones.Implementations;
using ProjectX.Services.DowJones.Interfaces;
using ProjectX.Services.EmailService.Implementation;
using ProjectX.Services.EmailService.Interface;
using ProjectX.Services.Nasdaq.Implementations;
using ProjectX.Services.Nasdaq.Interfaces;
using ProjectX.Services.Sp500.Implementations;
using ProjectX.Services.Sp500.Interfaces;
using ProjectX.Services.Statistics.Implementations;
using ProjectX.Services.Statistics.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddOptions<EmailOptions>()
    .BindConfiguration(nameof(EmailOptions))
    .ValidateDataAnnotations();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
        .EnableSensitiveDataLogging(false)
        .EnableDetailedErrors();
    options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());



builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICompoundInterestCalculatorService, CompoundInterestCalculatorService>();
builder.Services.AddScoped<ICompoundInterestFromHistoricalDataService, CompoundInterestFromHistoricalDataService>();
builder.Services.AddScoped<ICalculationOfSlumpsFromHistoricalDataService, CalculationOfSlumpsFromHistoricalDataService>();
builder.Services.AddScoped<ISp500Service, Sp500Service>();
builder.Services.AddScoped<INasdaqService, NasdaqService>();
builder.Services.AddScoped<IDowJonesService, DowJonesService>();
builder.Services.AddScoped<IBaseDataForTableService, BaseDataForTableService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<IImportCsvService, ImportCsvService>();

builder.Services.AddScoped<IAssetHistoricalDataRepository, AssetHistoricalDataRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();


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

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Sp500}/{action=GetSp500View}/{id?}")
    .WithStaticAssets();


app.Run();