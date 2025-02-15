using MarketDataService.Models.Json;

namespace MarketDataService.Models.Json;

public class HistoricalStockRootJsonModel
{
    public List<HistoricalStockListJsonModel> historicalStockList { get; set; } = new();
}