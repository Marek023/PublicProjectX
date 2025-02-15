using MarketDataService.Extensions.Assets;
using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class UserSettingAssetNotificationQueueRepository : IUserSettingAssetNotificationQueueRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserSettingAssetNotificationQueueRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateRelation(int userSubscribedId, int assetNotificationQueueId)
    {
        var userSetting = _dbContext.UserSetting.Find(userSubscribedId)
                          ?? throw new NullReferenceException("UserSetting is null");
        
        var assetNotificationQueue = _dbContext.AssetNotificationQueue.Find(assetNotificationQueueId)
                                     ?? throw new NullReferenceException("AssetNotificationQueue is null");

        var existingRelation = _dbContext.UserSettingAssetNotificationQueue
            .FirstOrDefault(relation => relation.UserSettingId == userSetting.Id
                                        && relation.AssetNotificationQueueId == assetNotificationQueue.Id);

        if (existingRelation is null)
        {
            var relation = new UserSettingAssetNotificationQueue
            {
                NotificationSent = false,
                UserSetting = userSetting,
                AssetNotificationQueue = assetNotificationQueue
            };

            _dbContext.UserSettingAssetNotificationQueue.Add(relation);
            _dbContext.SaveChanges();
        }
    }

    public List<UserSettingAssetNotificationQueueModel> GetRelations()
    {

        return _dbContext.UserSettingAssetNotificationQueue
            .Where(relation => relation.NotificationSent == false)
            .ToList()
            .ToModelList();

    }

    public void SetRelation(int relationId)
    {
        var existingRelation = _dbContext.UserSettingAssetNotificationQueue.Find(relationId);
        if (existingRelation is not null)
        {
            existingRelation.NotificationSent = true;
            existingRelation.SentDateTime = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }
    }
}