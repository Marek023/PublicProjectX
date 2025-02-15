namespace ProjectX.Models.Assets;

public class AssetWithHistoricalDataModel
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;  
    public string SubSector { get; set; } = string.Empty;
    public string HeadQuarter { get; set; } = string.Empty; 
    public string DateFirstAdded { get; set; } = string.Empty;
    public string Cik { get; set; } = string.Empty; 
    public string Founded	 { get; set; } = string.Empty; 
    public DateTime? DateSave { get; set; }
    public int AssetCategoryId { get; set; }
    public string? AssetCategoryName { get; set; } = string.Empty;
    public List<HistoricalDataModel> HistoricalData { get; set; } = new();
}