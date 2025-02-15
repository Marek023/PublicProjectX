using ProjectX.Enums;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IExcludedAssetRepository
{
    void SaveExcludedAsset(List<Assets> assetsModels, AssetCategoryEnum assetCategory);
    List<AssetModel> GetExcludedAssets();
    void SetNotificationCreated(List<AssetModel> excludedAssets);
}