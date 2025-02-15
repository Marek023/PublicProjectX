namespace MarketDataService.Models.Json;

public class HistoricalStockListJsonModel
{
    public string symbol { get; set; } = string.Empty;
    public int AssetId { get; set; }
    public List<AssetHistoricalDataJsonModel> historical { get; set; } = new();
}