using System.ComponentModel.DataAnnotations;

namespace MarketIndexAnalyzer.Data.Entities;

public class IndexType
{
    [Key] 
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Symbol { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Currency { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;
    [Required]
    public bool IsDeleted { get; set; }
    public virtual List<IndexData> IndexData { get; set; } = new ();
}