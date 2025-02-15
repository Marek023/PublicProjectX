using MarketDataService.Models.Asset;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.Assets;

public static class AssetNotificationQueueMappingExtensions
{
    public static List<AssetNotificationQueueModel> ToModelList(
        this List<AssetNotificationQueue> assetNotificationQueues)
    {
        return assetNotificationQueues.Select(assetNotificationQueue => new AssetNotificationQueueModel
        {
            Id = assetNotificationQueue.Id,
            NotificationDate = assetNotificationQueue.NotificationDate,
            NotificationContent = assetNotificationQueue.NotificationContent,
        }).ToList();
    }

    public static AssetNotificationQueueModel ToModel(this AssetNotificationQueue assetNotificationQueue)
    {
        return new AssetNotificationQueueModel
        {
            Id = assetNotificationQueue.Id,
            NotificationDate = assetNotificationQueue.NotificationDate,
            NotificationContent = assetNotificationQueue.NotificationContent,
        };
    }
}