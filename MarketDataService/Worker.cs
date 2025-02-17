using MarketDataService.Services.AssetServices.Interfaces;
using MarketDataService.Services.NotificationServices.Interfaces;
using Exception = System.Exception;

namespace MarketDataService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IAssetService _assetService;
    private readonly IAssetHistoricalService _assetHistoricalService;
    private readonly IAssetHistoricalDataService _assetHistoricalDataService;
    private readonly INotificationChangedAssetsService _notificationChangedAssetsService;

    public Worker(
        ILogger<Worker> logger,
        IAssetService assetService,
        IAssetHistoricalService assetHistoricalService,
        IAssetHistoricalDataService assetHistoricalDataService,
        INotificationChangedAssetsService notificationChangedAssetsService)

    {
        _logger = logger;
        _assetService = assetService;
        _assetHistoricalService = assetHistoricalService;
        _assetHistoricalDataService = assetHistoricalDataService;
        _notificationChangedAssetsService = notificationChangedAssetsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await _assetService.SaveAssetsAsync();
                await _assetHistoricalService.SaveAssetHistoricalAsync();
                await _assetHistoricalDataService.SaveAssetHistoricalDataAsync();
                await _notificationChangedAssetsService.SendMailWithChangedAssetsAsync();
               
               
                await Task.Delay(1000000, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ExecuteAsync");
           
        }
    }
}
