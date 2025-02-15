using MarketDataService.Models.Asset;
using ProjectX.Data.Contexts;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IUserSettingAssetNotificationQueueRepository
{
    void CreateRelation(int userSubscribedId, int assetNotificationQueueId);
    List<UserSettingAssetNotificationQueueModel> GetRelations();
    void SetRelation(int relationId);
}