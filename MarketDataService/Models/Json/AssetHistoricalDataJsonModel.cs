namespace MarketDataService.Models.Json;

public class AssetHistoricalDataJsonModel
{
    public string date { get; set; } = string.Empty;
    public decimal open { get; set; }
    public decimal high { get; set; }
    public decimal low { get; set; }
    public decimal close { get; set; }
    public decimal adjClose { get; set; }
    public long volume { get; set; }
    public long unadjustedVolume { get; set; }
    public decimal change { get; set; }
    public decimal changePercent { get; set; }
    public decimal vwap { get; set; }
    public string label { get; set; } = string.Empty;
    public decimal changeOverTime { get; set; }
}
