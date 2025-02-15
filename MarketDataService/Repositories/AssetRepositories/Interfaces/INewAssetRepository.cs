using ProjectX.Enums;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface INewAssetRepository
{
    void SaveNewAsset(List<AssetJsonModel> assetsModels, AssetCategoryEnum assetCategory);
    List<AssetModel> GetNewAssets();
    void SetNotificationCreated(List<AssetModel> newAssets);
}