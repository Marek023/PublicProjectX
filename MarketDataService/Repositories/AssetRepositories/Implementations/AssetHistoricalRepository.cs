using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class AssetHistoricalRepository : IAssetHistoricalRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetHistoricalRepository(IApplicationDbContext context)
    {
        _dbContext = context;
    }

    public void SaveAssetHistorical(AssetHistoricalModel assetHistoricalModel)
    {
        var assetHistoricalIsExist = _dbContext.AssetHistorical.FirstOrDefault(assetHistorical =>
            assetHistorical.AssetId == assetHistoricalModel.AssetId);
        
        if (assetHistoricalIsExist is null)
        {
            var assetHistorical = new AssetHistorical()
            {
                DateAdded = assetHistoricalModel.DateAdded,
                AddedSecurity = assetHistoricalModel.AddedSecurity,
                RemovedTicker = assetHistoricalModel.RemovedTicker,
                RemovedSecurity = assetHistoricalModel.RemovedSecurity,
                Date = assetHistoricalModel.Date,
                Symbol = assetHistoricalModel.Symbol,
                Reason = assetHistoricalModel.Reason,
                DateSave = assetHistoricalModel.DateSave,
                AssetId = assetHistoricalModel.AssetId,
            };
            
            _dbContext.AssetHistorical.Add(assetHistorical);
            _dbContext.SaveChanges();
        }
    }
}