using ProjectX.Data.Contexts;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;

namespace ProjectX.Repositories.Assets.Implementations;

public class AssetRepository : IAssetRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<AssetWithHistoricalDataModel> GetAssetWithHistoricalData(int assetCategoryId)
    {
        var assets = _dbContext.Assets.Where(asset => asset.AssetCategoryId == assetCategoryId)
                                                  .ToList();
        
        var assetsModels = new List<AssetWithHistoricalDataModel>();
        foreach (var asset in assets)
        {
            var historicalDataModels = new List<HistoricalDataModel>();
            
            var assetsModel = new AssetWithHistoricalDataModel
            {
                Id = asset.Id,
                Name = asset.Name,
                Symbol = asset.Symbol,
                Sector = asset.Sector,
                SubSector = asset.SubSector,
                HeadQuarter = asset.HeadQuarter,
                Cik = asset.Cik,
                Founded = asset.Founded,
                HistoricalData = historicalDataModels  
            };
            assetsModels.Add(assetsModel);
            
            foreach (var historicalData in asset.AssetHistoricalDatas.OrderByDescending(data => data.Date))
            {
                var historicalDataModel = new HistoricalDataModel
                {
                    Id = historicalData.Id,
                    Date = historicalData.Date,
                    Open = historicalData.Open,
                    Close = historicalData.Close,
                    High = historicalData.High,
                    Low = historicalData.Low,
                    AdjClose = historicalData.AdjClose,
                    Volume = historicalData.Volume,
                    UnadjustedVolume = historicalData.UnadjustedVolume,
                    Change = historicalData.Change,
                    ChangePercent = historicalData.ChangePercent,
                    Vwap = historicalData.Vwap,
                    ChangeOverTime = historicalData.ChangeOverTime,
                    AssetsId = historicalData.AssetsId,
                };
                historicalDataModels.Add(historicalDataModel);
            }
            
        }
        return assetsModels;
    }
}