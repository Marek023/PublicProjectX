using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IAssetCategoryRepository
{
    AssetCategories GetAssetCategory(int assetCategoryId);
}