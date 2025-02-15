using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketIndexAnalyzer.Data.Entities;

public class IndexData
{
    [Key]
    public int Id { get; set; }
    public double Open { get; set; }
    public double Close { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    [Required]
    public DateTime Time { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
    public int IndexTypeId { get; set; }
    [ForeignKey(nameof(IndexTypeId))] 
    public virtual IndexType IndexType { get; set; }
}