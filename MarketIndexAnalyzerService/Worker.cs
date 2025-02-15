using MarketIndexAnalyzer.Services.Calculations.Interfaces;
using MarketIndexAnalyzer.Services.IndexServices.Interfaces;

namespace MarketIndexAnalyzer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly INasdaqISharesService _nasdaqISharesService;
    private readonly ICalculationOfSlumps _calculationOfSlumps;

    public Worker(ILogger<Worker> logger,
        INasdaqISharesService nasdaqISharesService,
        ICalculationOfSlumps calculationOfSlumps)
    {
        _logger = logger;
        _nasdaqISharesService = nasdaqISharesService;
        _calculationOfSlumps = calculationOfSlumps;
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

                await _nasdaqISharesService.SaveHistoricalDataFromFinexAsync();
                _calculationOfSlumps.GetResult();

                await Task.Delay(1000000, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ExecuteAsync");
            throw new Exception(ex.Message);
        }
    }
}