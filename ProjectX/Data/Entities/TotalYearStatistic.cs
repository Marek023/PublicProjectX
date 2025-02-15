using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class TotalYearStatistic
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Month { get; set; }
    [Required]
    public int TotalDeposit { get; set; }
    [Required]
    public decimal TotalDivident { get; set; }
    [Required]
    public decimal TotalProfitForYear { get; set; }
    [Required]
    public decimal ProfitInPercentForYear { get; set; }
    public virtual List<YearStatistic> YearStatistics { get; set; } = new();
    public bool IsDeleted { get; set; }
}