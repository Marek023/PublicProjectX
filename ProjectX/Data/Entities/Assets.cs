using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class Assets
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Symbol { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Sector { get; set; } = string.Empty;
    [MaxLength(100)]
    public string SubSector { get; set; } = string.Empty;
    [MaxLength(100)]
    public string HeadQuarter { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? DateFirstAdded { get; set; }
    [MaxLength(100)]
    public string Cik { get; set; } = string.Empty; 
    [MaxLength(100)]
    public string Founded { get; set; } = string.Empty; 
    public DateTime DateSave { get; set; }
    public int AssetCategoryId { get; set; }
    [Required]
    [ForeignKey(nameof(AssetCategoryId))]
    public virtual AssetCategories? AssetCategory { get; set; }
    public bool IsDeleted { get; set; }
    public virtual List<AssetHistoricalData> AssetHistoricalDatas { get; set; } = new();
    public virtual AssetHistorical? AssetHistorical { get; set; }
    
}