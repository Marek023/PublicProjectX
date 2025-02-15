using System.Reflection.Metadata.Ecma335;

namespace ProjectX.Models.Statistic;

public class TotalYearStatisticModel
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int MonthNumber { get; set; }
    public int TotalDeposit { get; set; }
    public decimal TotalDivident { get; set; }
    public decimal TotalProfitForYear { get; set; }
    public string ProfitInPercentForYear { get; set; } = string.Empty;
}