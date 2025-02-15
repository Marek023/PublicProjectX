using System.ComponentModel.DataAnnotations;

namespace ProjectX.Data.Entities;

public class AssetNotificationQueue
{
    [Key]
    public int Id { get; set; }
    public DateTime? NotificationDate{ get; set; }
    public string? NotificationContent { get; set; }
    public bool IsDeleted { get; set; }
    public virtual List<UserSettingAssetNotificationQueue>? UserSettingAssetNotificationQueue { get; set; }
}