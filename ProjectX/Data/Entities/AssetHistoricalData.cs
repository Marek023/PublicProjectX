using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectX.Data.Entities;

public class AssetHistoricalData
{
    [Key]
    public int Id { get; set; } 
    public string Date { get; set; } 
    public decimal Open { get; set; } 
    public decimal Low { get; set; } 
    public decimal High { get; set; } 
    public decimal Close { get; set; } 
    public decimal AdjClose { get; set; } 
    public long Volume { get; set; } 
    public long UnadjustedVolume { get; set; } 
    public decimal Change { get; set; } 
    public decimal ChangePercent { get; set; } 
    public decimal Vwap { get; set; } 
    public decimal ChangeOverTime{ get; set; } 
    public DateTime DateSave { get; set; }
    [ForeignKey(nameof(AssetsId))]
    public int AssetsId { get; set; } 
    [Required]
    public virtual Assets? Assets { get; set; } 
    public bool IsDeleted { get; set; }
}