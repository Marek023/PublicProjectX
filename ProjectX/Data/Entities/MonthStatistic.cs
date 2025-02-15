using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class MonthStatistic
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(YearStatistic))]
    public int YearStatisticId { get; set; }
    public virtual YearStatistic YearStatistic { get; set; }
    [StringLength(30)]
    [Required]
    public string XtbId { get; set; }
    [StringLength(100)]
    [Required]
    public string Symbol { get; set; } = string.Empty;
    [StringLength(20)]
    [Required]
    public string Type { get; set; } = string.Empty; 
    [Required]
    public int Volume { get; set; }
    [Required]
    public DateTime OpenTime { get; set; }
    [Required]
    public decimal OpenPrice { get; set; }
    [Required]
    public DateTime CloseTime { get; set; }
    [Required]
    public decimal ClosePrice { get; set; }
    [Required]
    public decimal Profit { get; set; }
    [Required]
    public decimal ProfitInPercent { get; set; }
    [Required]
    public decimal TotalProfitInPercent { get; set; }
    public bool IsDeleted { get; set; }
}