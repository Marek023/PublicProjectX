namespace ProjectX.Models.Assets;

public class AssetHistoricalDataModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Open { get; set; }
    public double Low { get; set; }
    public double High { get; set; }
    public double Close { get; set; }
    public decimal AdjClose { get; set; }
    public long Volume { get; set; }
    public long UnadjustedVolume { get; set; }
    public decimal Change { get; set; }
    public decimal ChangePercent { get; set; }
    public decimal Vwap{ get; set; }
    public decimal ChangeOverTime { get; set; }
    public DateTime DateSave { get; set; }
    public int AssetsId { get; set; }
    public string AssetSymbol { get; set; } = string.Empty;
}