using ProjectX.Enums;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;
using ProjectX.Data.Entities;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class AssetControlService : IAssetControlService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AssetControlService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void CheckAsset(List<AssetJsonModel> assetJsonModels, AssetCategoryEnum assetCategory)
    {
        var assets = GetAssetsByAssetCategory(assetCategory);

        var newAssetJsonModels = GetNewAssetJsonModels(assetJsonModels, assets);
        var newRemovedJsonModels = GetRemovedAssets(assetJsonModels, assets);

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var newAssetRepository = scope.ServiceProvider.GetService<INewAssetRepository>();
            var excludedAssetRepository = scope.ServiceProvider.GetService<IExcludedAssetRepository>();
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>();

            if (newAssetRepository is null || excludedAssetRepository is null || assetRepository is null)  
            {
                throw new Exception("newAssetRepository, excludedAssetRepository or assetRepository is null");
            }
            
            newAssetRepository.SaveNewAsset(newAssetJsonModels,assetCategory);
            excludedAssetRepository.SaveExcludedAsset(newRemovedJsonModels, assetCategory);
            
            assetRepository.DeleteExcludedAssets(newRemovedJsonModels, assetCategory);
        }
    }

    private List<Assets> GetAssetsByAssetCategory(AssetCategoryEnum assetCategory)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>();
            if (assetRepository == null)
            {
                throw new NullReferenceException("Asset repository is null");
            }

            List<Assets> assets = new List<Assets>();
            switch (assetCategory)
            {
                case AssetCategoryEnum.Nasdaq100:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
                case AssetCategoryEnum.DowJones:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
                case AssetCategoryEnum.Sp500:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
            }

            return assets;
        }
    }

    private List<AssetJsonModel> GetNewAssetJsonModels(
        List<AssetJsonModel> assetJsonModels,
        List<Assets> assets)
    {
        var existingSymbols = assets
            .Select(asset => asset.Symbol)
            .ToHashSet();

        return assetJsonModels
            .Where(assetJsonModel => !existingSymbols.Contains(assetJsonModel.symbol))
            .ToList();
    }
    
    private List<Assets> GetRemovedAssets(
        List<AssetJsonModel> assetJsonModels,
        List<Assets> assets)
    {
        var incomingSymbols = assetJsonModels
            .Select(assetJsonModel => assetJsonModel.symbol)
            .ToHashSet();

        return assets
            .Where(asset => !incomingSymbols.Contains(asset.Symbol))
            .ToList();
    }
}