namespace MarketDataService.Models.Asset;

public class AssetHistoricalModel
{
    public int Id { get; set; }
    public string? DateAdded { get; set; } 
    public string? AddedSecurity { get; set; } 
    public string? RemovedTicker { get; set; } 
    public string? RemovedSecurity { get; set; } 
    public string? Date { get; set; } 
    public string? Symbol { get; set; } 
    public string? Reason { get; set; } 
    public DateTime DateSave { get; set; }
    public int AssetId { get; set; }
}