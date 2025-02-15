using ProjectX.Data.Entities;
using ProjectX.Models.Assets;

namespace ProjectX.Extensions.Assets;

public static class AssetHistoricalDataMappingExtensions
{
    public static List<AssetHistoricalDataModel> ToModelList(this List<AssetHistoricalData> assetHistoricalDatas)
    {
        return assetHistoricalDatas.Select(assetHistoricalData => new AssetHistoricalDataModel
        {
            Id = assetHistoricalData!.Id,
            Date = DateTime.Parse(assetHistoricalData.Date),
            Open = (double)assetHistoricalData.Open,
            Low = (double)assetHistoricalData.Low,
            High = (double)assetHistoricalData.High,
            Close = (double)assetHistoricalData.Close,
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