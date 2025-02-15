namespace MarketDataService.Models.Json;

public class AssetHistoricalJsonModel
{
    public string? dateAdded { get; set; } 
    public string? addedSecurity { get; set; } 
    public string? removedTicker { get; set; } 
    public string? removedSecurity { get; set; } 
    public string? date { get; set; } 
    public string? symbol { get; set; } 
    public string? reason { get; set; } 
}