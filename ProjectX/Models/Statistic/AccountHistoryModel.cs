namespace ProjectX.Models.Statistic;

public class AccountHistoryModel
{
    public string Symbol { get; set; } = string.Empty;
    public string XtbId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty; 
    public int Volume { get; set; }
    public DateTime OpenTime { get; set; }
    public decimal OpenPrice { get; set; }
    public DateTime CloseTime { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal Profit { get; set; }
    public decimal ProfitInPercent { get; set; }
    public decimal TotalProfitInPercent { get; set; }
}