namespace MarketDataService.Models.Asset;

public class UserSettingAssetNotificationQueueModel
{
    public int Id { get; set; }
    public bool  NotificationSent { get; set; }
    public DateTime? SentDataTime { get; set; }
    public int UserSettingId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public int AssetNotificationQueueId { get; set; }
}