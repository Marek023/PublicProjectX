using System.ComponentModel.DataAnnotations;

namespace ProjectX.Data.Entities;

public class AssetCategories
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public virtual List<Assets>? Assets { get; set; } = new();
    public virtual List<NewAssets>? NewAssets { get; set; } = new();
    public virtual List<ExcludedAssets>? ExcludedAssets { get; set; } = new();
    
}