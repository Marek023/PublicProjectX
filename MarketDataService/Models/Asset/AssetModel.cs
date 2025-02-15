namespace MarketDataService.Models.Asset;

public class AssetModel
{
    public int Id { get; set; }
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Sector { get; set; }
    public string? SubSector { get; set; }
    public string? HeadQuarter { get; set; } 
    public string? DateFirstAdded { get; set; }
    public string? Cik { get; set; }
    public string? Founded	 { get; set; } 
    public DateTime? DateSave { get; set; }
    public int AssetCategoryId { get; set; }
    public string? AssetCategoryName { get; set; } = string.Empty;
}