using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class ExcludedAssets
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Symbol { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(100)]
    public string? Sector { get; set; }
    [MaxLength(100)]
    public string? SubSector { get; set; }
    [MaxLength(100)]
    public string? HeadQuarter { get; set; } 
    [MaxLength(100)]
    public string? DateFirstAdded { get; set; }
    [MaxLength(100)]
    public string? Cik { get; set; } 
    [MaxLength(100)]
    public string? Founded	 { get; set; } 
    public DateTime? DateSave { get; set; }
    public bool NotificationCreated { get; set; }
    public int AssetCategoryId { get; set; }
    [ForeignKey(nameof(AssetCategoryId))]
    public virtual AssetCategories? AssetCategory { get; set; }
    public bool IsDeleted { get; set; }
    
}