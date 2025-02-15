using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Data.Entities;

public class UserSettingAssetNotificationQueue
{
    [Key]
    public int Id { get; set; }
    public bool? NotificationSent { get; set; }
    public DateTime? SentDateTime { get; set; }
    public int UserSettingId { get; set; }
    public int AssetNotificationQueueId { get; set; }
    public bool IsDeleted { get; set; }
    
    [ForeignKey(nameof(UserSettingId))]
    public virtual required UserSetting UserSetting { get; set; }
    [ForeignKey(nameof(AssetNotificationQueueId))]
    public virtual required AssetNotificationQueue AssetNotificationQueue { get; set; }
}