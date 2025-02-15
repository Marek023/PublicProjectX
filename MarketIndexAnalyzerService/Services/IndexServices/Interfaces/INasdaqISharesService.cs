namespace MarketIndexAnalyzer.Services.IndexServices.Interfaces;

public interface INasdaqISharesService
{
    Task SaveHistoricalDataFromFinexAsync();
}