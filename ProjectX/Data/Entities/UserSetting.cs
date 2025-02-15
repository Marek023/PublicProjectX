using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class UserSetting
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }
    public bool? SendAssetChanges { get; set; }
    
    public virtual required User User { get; set; }

    public virtual List<UserSettingAssetNotificationQueue>? UserSettingAssetNotificationQueue { get; set; } 
    public bool IsDeleted { get; set; }
}