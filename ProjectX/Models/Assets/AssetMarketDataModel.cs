namespace ProjectX.Models.Assets;

public class AssetMarketDataModel
{
    public int AssetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string ChangePerWeek { get; set; } = string.Empty;
    public string ChangePerDay { get; set; } = string.Empty;
    public string CurrentChangeFromTop { get; set; } = string.Empty;
    
    
}