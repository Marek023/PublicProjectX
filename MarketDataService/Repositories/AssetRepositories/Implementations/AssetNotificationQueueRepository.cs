using MarketDataService.Extensions.Assets;
using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class AssetNotificationQueueRepository : IAssetNotificationQueueRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetNotificationQueueRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveNotificationContent(string content)
    {
        var notificationQueue = new AssetNotificationQueue()
        {
            NotificationDate = DateTime.UtcNow,
            NotificationContent = content
        };

        _dbContext.AssetNotificationQueue.Add(notificationQueue);
        _dbContext.SaveChanges();
    }

    public AssetNotificationQueueModel GetLastNotificationContent()
    {
        return _dbContext.AssetNotificationQueue
            .OrderByDescending(notificationQueue => notificationQueue.NotificationDate)
            .FirstOrDefault()?
            .ToModel() ?? throw new NullReferenceException("Asset notification queue not found");
    }

    public AssetNotificationQueueModel GetNotificationContentById(int id)
    {
        return _dbContext.AssetNotificationQueue.FirstOrDefault(notificationQueue => notificationQueue.Id == id)?
            .ToModel() ?? throw new NullReferenceException("Asset notification queue not found");
    }
}