namespace ProjectX.Models.Statistic;

public class YearStatisticModel
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int MonthNumber { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public int TotalDeposit { get; set; }
    public decimal Profit { get; set; }
    public decimal Dividend { get; set; }
    public string PofitInPercent { get; set; } = string.Empty;
    public TotalYearStatisticModel TotalYearStatisticModel { get; set; }
    
}