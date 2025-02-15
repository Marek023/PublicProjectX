using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class YearStatistic
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(TotalYearStatistic))]
    public int TotalYearStatisticId { get; set; }
    public virtual TotalYearStatistic TotalYearStatistic { get; set; }
    
    [Required]
    public int Year { get; set; }
    [Required]
    public int MonthNumber { get; set; }
    [StringLength(20)]
    public string MonthName { get; set; } = string.Empty;
    [Required]
    public int TotalDeposit { get; set; }
    [Required]
    public decimal Profit { get; set; }
    public decimal Dividend { get; set; }
    [Required]
    public decimal ProfitInPercent { get; set; }
    public virtual List<MonthStatistic> MonthStatistics { get; set; } = new();
    public bool IsDeleted { get; set; }
    
}