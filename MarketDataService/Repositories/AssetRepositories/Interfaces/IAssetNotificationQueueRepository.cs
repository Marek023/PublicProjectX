using MarketDataService.Models.Asset;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IAssetNotificationQueueRepository
{
    void SaveNotificationContent(string content);
    AssetNotificationQueueModel GetLastNotificationContent();
    AssetNotificationQueueModel GetNotificationContentById(int id);
}