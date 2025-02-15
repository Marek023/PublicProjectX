using ProjectX.Enums;
using MarketDataService.Extensions;
using MarketDataService.Extensions.Assets;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class NewAssetRepository : INewAssetRepository
{

    private readonly IApplicationDbContext _dbContext;

    public NewAssetRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveNewAsset(List<AssetJsonModel> assetsModels, AssetCategoryEnum assetCategory)
    {
        foreach (var assetsModel in assetsModels)
        {
            var assetIsExist = _dbContext.NewAssets
                .FirstOrDefault(newAsset =>
                    newAsset.Symbol == assetsModel.symbol && newAsset.AssetCategoryId == (int)assetCategory);
            
            if (assetIsExist is null)
            {
                var asset = new NewAssets()
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
                    AssetCategoryId = (int)assetCategory
                };

                _dbContext.NewAssets.Add(asset);
            }
        }
        _dbContext.SaveChanges();
    }

    public List<AssetModel> GetNewAssets()
    {
        return _dbContext.NewAssets.Where(newAssets => newAssets.NotificationCreated == false )
            .ToList()
            .ToModelList();;
    }

    public void SetNotificationCreated(List<AssetModel> newAssets)
    {
        foreach (var newAsset in newAssets)
        {
           var existingAsset = _dbContext.NewAssets.Find(newAsset.Id);
           if (existingAsset is not null)
           {
               existingAsset.NotificationCreated = true;
           }
        }
        _dbContext.SaveChanges();
    }
}