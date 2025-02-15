using ProjectX.Enums;
using MarketDataService.Extensions.Assets;
using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class ExcludedAssetRepository : IExcludedAssetRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ExcludedAssetRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveExcludedAsset(List<Assets> assets, AssetCategoryEnum assetCategory)
    {
        foreach (var asset in assets)
        {
            var assetIsExist = _dbContext.ExcludedAssets
                .FirstOrDefault(excludedAsset =>
                    excludedAsset.Symbol == asset.Symbol && excludedAsset.AssetCategoryId == (int)assetCategory);

            if (assetIsExist is null)
            {
                var excludedAsset = new ExcludedAssets()
                {
                    Symbol = asset.Symbol,
                    Name = asset.Name,
                    Sector = asset.Sector,
                    SubSector = asset.SubSector,
                    HeadQuarter = asset.HeadQuarter,
                    DateFirstAdded = asset.DateFirstAdded,
                    Cik = asset.Cik,
                    Founded = asset.Founded,
                    DateSave = DateTime.UtcNow,
                    AssetCategoryId = (int)assetCategory
                };

                _dbContext.ExcludedAssets.Add(excludedAsset);
            }
        }

        _dbContext.SaveChanges();
    }

    public List<AssetModel> GetExcludedAssets()
    {
        return _dbContext.ExcludedAssets.Where(newAssets => newAssets.NotificationCreated == false )
            .ToList()
            .ToModelList();;
    }
    
    public void SetNotificationCreated(List<AssetModel> excludedAssets)
    {
        foreach (var excludedAsset in excludedAssets)
        {
            var existingAsset = _dbContext.ExcludedAssets.Find(excludedAsset.Id);
            if (existingAsset is not null)
            {
                existingAsset.NotificationCreated = true;
            }
        }
        _dbContext.SaveChanges();
    }
}