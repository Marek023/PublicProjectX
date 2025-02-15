using System.Globalization;
using MarketDataService.Extensions.Assets;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class AssetHistoricalDataRepository : IAssetHistoricalDataRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetHistoricalDataRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveHistoricalData(HistoricalStockListJsonModel historicalStockListJsonModel, int assetId)
    {
        var date = new HashSet<string>(GetHistoricalDataByAssetsId(assetId)
            .Select(historicalData => historicalData.Date));

        foreach (var assetHistoricalDataJsonModels in historicalStockListJsonModel.historical)
        {
            if (!date.Contains(assetHistoricalDataJsonModels.date))
            {
                var assetHistoricalData = new AssetHistoricalData()
                {
                    Date = assetHistoricalDataJsonModels.date,
                    Open = assetHistoricalDataJsonModels.open,
                    Low = assetHistoricalDataJsonModels.low,
                    High = assetHistoricalDataJsonModels.high,
                    Close = assetHistoricalDataJsonModels.close,
                    Volume = assetHistoricalDataJsonModels.volume,
                    UnadjustedVolume = assetHistoricalDataJsonModels.unadjustedVolume,
                    Change = assetHistoricalDataJsonModels.change,
                    ChangePercent = assetHistoricalDataJsonModels.changePercent,
                    Vwap = assetHistoricalDataJsonModels.vwap,
                    ChangeOverTime = assetHistoricalDataJsonModels.changeOverTime,
                    AdjClose = assetHistoricalDataJsonModels.adjClose,
                    DateSave = DateTime.UtcNow,
                    AssetsId = assetId,
                };

                _dbContext.AssetHistoricalData.Add(assetHistoricalData);
            }
        }

        _dbContext.SaveChanges();
    }

    public List<int> GetAssetsIds()
    {
        return _dbContext.AssetHistoricalData.Select(assetHistoricalData => assetHistoricalData.AssetsId)
            .Distinct()
            .ToList();
    }

    public List<int> GetAllIds()
    {
        return _dbContext.AssetHistoricalData.Select(assetHistoricalData => assetHistoricalData.Id)
            .ToList();
        
    }

    public List<AssetHistoricalDataModel> GetLastRecords(List<int> assetsIds)
    {
        var lastRecords = _dbContext.AssetHistoricalData
            .Where(historicalData => assetsIds.Contains(historicalData.AssetsId))
            .GroupBy(historicalData => historicalData.AssetsId)
            .Select(grouping => grouping.OrderByDescending(historicalData => historicalData.Date).FirstOrDefault())
            .ToList();

        return lastRecords!.ToModelList();
    }
    
    public List<AssetHistoricalDataModel> GetLastWeekRecords(List<int> assetsIds)
    {
        var fromDate = DateTime.Today.AddDays(-8);
        
        var lastRecords = _dbContext.AssetHistoricalData
            .Where(historicalData => assetsIds.Contains(historicalData.AssetsId))
            .AsEnumerable()
            .GroupBy(historicalData => historicalData.AssetsId)
            .SelectMany(grouping => grouping.OrderByDescending(historicalData => historicalData.Date).Take(5))
            .ToList();

        return lastRecords!.ToModelList();
    }

    public List<AssetHistoricalDataModel> GetHistoricalDataByAssetsId(int assetId)
    {
        return _dbContext.AssetHistoricalData.Where(historicalData => historicalData.AssetsId == assetId)
            .ToList()
            .ToModelList();
    }

    public List<AssetHistoricalDataModel> GetTopValuesByAssetId(List<int> assetsIds)
    {
        var highValues = _dbContext.AssetHistoricalData
            .Where(historicalData => assetsIds.Contains(historicalData.AssetsId))
            .GroupBy(historicalData => historicalData.AssetsId)
            .Select(grouping => grouping.OrderByDescending(historicalData => historicalData.High).FirstOrDefault())
            .ToList();

        return highValues!.ToModelList();
    }

    public void DeleteAssetHistoricalData(List<int> historicalDataIds)
    {
        var historicalData = _dbContext.AssetHistoricalData
            .Where(historicalData => historicalDataIds.Contains(historicalData.Id) )
            .ToList();

        foreach (var data in historicalData)
        {
            data.IsDeleted = true;
        }
        
        _dbContext.SaveChanges();


        // _dbContext.AssetHistoricalData
        //     .Where(data => historicalDataIds.Contains(data.Id))
        //     .ExecuteUpdate(update => update.SetProperty(data => data.IsDeleted, true));
    }
}