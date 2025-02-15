using MarketDataService.Models.Asset;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.Assets;

public static class UserSettingAssetNotificationQueueMappingExtension
{
    public static List<UserSettingAssetNotificationQueueModel> ToModelList(
        this List<UserSettingAssetNotificationQueue> userSettingAssetNotificationQueues)
    {
        return userSettingAssetNotificationQueues.Select(userSettingAssetNotificationQueue => new UserSettingAssetNotificationQueueModel
        {
            Id = userSettingAssetNotificationQueue.Id,
            NotificationSent = userSettingAssetNotificationQueue.NotificationSent ?? false,
            SentDataTime = userSettingAssetNotificationQueue.SentDateTime ?? DateTime.MinValue,
            UserSettingId = userSettingAssetNotificationQueue.UserSettingId,
            UserEmail = userSettingAssetNotificationQueue.UserSetting.User.Email,
            AssetNotificationQueueId = userSettingAssetNotificationQueue.AssetNotificationQueueId
            
        }).ToList();
    }
}