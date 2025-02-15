using ProjectX.Enums;
using MarketDataService.Models.Json;

namespace MarketDataService.Services.AssetServices.Interfaces;

public interface IAssetControlService
{
    void CheckAsset(List<AssetJsonModel> assetJsonModels, AssetCategoryEnum assetCategory);
}