
using ProjectX.Enums;
using MarketDataService.Models.Json;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IAssetRepository
{
    void SaveAssets(List<AssetJsonModel> assetsModels, AssetCategories assetCategories);
    List<Assets> GetAssetsByAssetCategory(AssetCategoryEnum assetCategoryEnum);
    void DeleteExcludedAssets(List<Assets> excludedAssets, AssetCategoryEnum assetCategory);
    List<int> GetAssetIds();
}