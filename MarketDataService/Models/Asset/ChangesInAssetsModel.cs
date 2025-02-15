namespace MarketDataService.Models.Asset;

public class ChangesInAssetsModel
{
    public bool IsChange { get; set; }
    public List<AssetModel> NewAssets { get; set; } = new();
    public List<AssetModel> ExcludedAssets { get; set; } = new();
}