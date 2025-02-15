namespace MarketDataService.Models.Asset;

public class AssetNotificationQueueModel
{
    public int  Id { get; set; }
    public DateTime? NotificationDate { get; set; }
    public string NotificationContent { get; set; } = string.Empty;
}