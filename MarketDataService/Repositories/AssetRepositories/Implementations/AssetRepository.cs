using ProjectX.Enums;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class AssetRepository : IAssetRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetRepository(IApplicationDbContext context)
    {
        _dbContext = context;
    }

    public void SaveAssets(List<AssetJsonModel> assetsModels, AssetCategories assetCategory)
    {
        foreach (var assetsModel in assetsModels)
        {
            var assetIsExist = _dbContext.Assets
                .FirstOrDefault(asset => asset.Symbol == assetsModel.symbol 
                                         && asset.AssetCategoryId == assetCategory.Id);

            if (assetIsExist is null)
            {
                var asset = new Assets()
                {
                    Symbol = assetsModel.symbol,
                    Name = assetsModel.name,
                    Sector = assetsModel.sector,
                    SubSector = assetsModel.subSector,
                    HeadQuarter = assetsModel.headQuarter,
                    DateFirstAdded = assetsModel.dateFirstAdded,
                    Cik = assetsModel.cik,
                    Founded = assetsModel.founded,
                    DateSave = DateTime.UtcNow,
                    AssetCategoryId = assetCategory.Id
                };

                _dbContext.Assets.Add(asset);
            }
        }
        _dbContext.SaveChanges();
    }

    public List<Assets> GetAssetsByAssetCategory(AssetCategoryEnum assetCategoryEnum)
    {
        return _dbContext.Assets.Where(asset => asset.AssetCategoryId == (int)assetCategoryEnum)
            .ToList();
    }
    
    public void DeleteExcludedAssets(List<Assets> excludedAssets, AssetCategoryEnum assetCategory)
    {
        foreach (var excludedAsset in excludedAssets)
        {
            var asset = _dbContext.Assets.FirstOrDefault(asset => asset.Symbol == excludedAsset.Symbol
                                                                  && asset.AssetCategoryId == (int)assetCategory);

            if (asset is not null)
            {
                asset.IsDeleted = true;
            }
        }

        _dbContext.SaveChanges();
    }

    public List<int> GetAssetIds()
    {
        return _dbContext.Assets.Select(asset => asset.Id).ToList();
    }
}