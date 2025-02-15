using MarketDataService.Models.Asset;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.Assets;

public static class AssetHistoricalDataMappingExtensions
{
    public static List<AssetHistoricalDataModel> ToModelList(this List<AssetHistoricalData> assetHistoricalDatas)
    {
        return assetHistoricalDatas.Select(assetHistoricalData => new AssetHistoricalDataModel
        {
            Id = assetHistoricalData!.Id,
            Date = assetHistoricalData.Date,
            Open = assetHistoricalData.Open,
            Low = assetHistoricalData.Low,
            High = assetHistoricalData.High,
            Close = assetHistoricalData.Close,
            AdjClose = assetHistoricalData.AdjClose,
            Volume = assetHistoricalData.Volume,
            UnadjustedVolume = assetHistoricalData.UnadjustedVolume,
            Change = assetHistoricalData.Change,
            ChangePercent = assetHistoricalData.ChangePercent,
            Vwap = assetHistoricalData.Vwap,
            ChangeOverTime = assetHistoricalData.ChangeOverTime,
            DateSave = assetHistoricalData.DateSave,
            AssetsId = assetHistoricalData.AssetsId,
            AssetSymbol = assetHistoricalData.Assets?.Symbol ?? string.Empty,
        }).ToList();
    }
}