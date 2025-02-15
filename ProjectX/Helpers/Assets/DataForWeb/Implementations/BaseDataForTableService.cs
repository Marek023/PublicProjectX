using System.Globalization;
using ProjectX.Helpers.Assets.DataForWeb.Interfaces;
using ProjectX.Models.Assets;

namespace ProjectX.Helpers.Assets.DataForWeb.Implementations;

public class BaseDataForTableService : IBaseDataForTableService
{
    public List<AssetMarketDataModel> GetDataForTable(List<AssetWithHistoricalDataModel> assetsWithHistoricalData)
    {
        var assetMarketDataModels = new List<AssetMarketDataModel>();
        
        foreach (var asset in assetsWithHistoricalData.OrderBy(asset => asset.Symbol))
        {
            if(asset.HistoricalData.Count < 5)
                continue;
            
            var assetMarketDataModel = new AssetMarketDataModel
            {
                AssetId = asset.Id,
                Name = asset.Name,
                Symbol = asset.Symbol,
                ChangePerDay = Math.Round(asset.HistoricalData[0].ChangePercent, 2).ToString(CultureInfo.InvariantCulture) + " %",
                ChangePerWeek = CalculateWeeklyChange(asset.HistoricalData).ToString(CultureInfo.InvariantCulture) + " %",
                CurrentChangeFromTop = CalculateChangeFromTop(asset.HistoricalData).ToString(CultureInfo.InvariantCulture) + " %",
            };
            assetMarketDataModels.Add(assetMarketDataModel);
        }
        
        return assetMarketDataModels;
    }
    
    private decimal CalculateWeeklyChange(List<HistoricalDataModel> historicalDataModels)
    {
        return Math.Round(((historicalDataModels[0].Close - historicalDataModels[4].Close) / historicalDataModels[4].Close) * 100, 2);
    }

    private decimal CalculateChangeFromTop(List<HistoricalDataModel> historicalDataModels)
    {
        var maxValue = historicalDataModels.Max(data => data.High);
        var lastValue = historicalDataModels.First().Close;
        
        return  Math.Round(((lastValue - maxValue) / maxValue) * 100, 2);
    }
}