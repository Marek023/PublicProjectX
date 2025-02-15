using MarketDataService.Models.Asset;

namespace MarketDataService.Helpers.AssetHistoricalDataHelper;

public interface IAssetHistoricalDataHelper
{
    List<AssetHistoricalDataModel> GetLastRecordInHistoricalData();
    List<AssetHistoricalDataModel> GetTopValuesInHistoricalData();
    List<AssetHistoricalDataModel> GetLastWeekRecordInHistoricalData();
}