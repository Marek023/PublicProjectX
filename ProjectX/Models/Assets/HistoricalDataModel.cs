namespace ProjectX.Models.Assets;

public class HistoricalDataModel
{
    public int Id { get; set; }
    public string Date { get; set; } = string.Empty;
    public decimal Open { get; set; }
    public decimal Low { get; set; }
    public decimal High { get; set; }
    public decimal Close { get; set; }
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