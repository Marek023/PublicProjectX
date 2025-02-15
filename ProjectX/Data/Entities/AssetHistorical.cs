using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class AssetHistorical
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? DateAdded { get; set; } 
    [MaxLength(100)]
    public string? AddedSecurity { get; set; } 
    [MaxLength(100)]
    public string? RemovedTicker { get; set; } 
    [MaxLength(100)]
    public string? RemovedSecurity { get; set; } 
    [MaxLength(100)]
    public string? Date { get; set; } 
    [MaxLength(100)]
    public string? Symbol { get; set; }
    [MaxLength(100)]
    public string? Reason { get; set; } 
    public DateTime DateSave { get; set; }
    public bool IsDeleted { get; set; }
    public int AssetId { get; set; }
    [Required]
    [ForeignKey(nameof(AssetId))]
    public virtual Assets? Asset { get; set; }
}