using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IAssetHistoricalDataRepository
{
    public List<int> GetAssetsIds();
    List<int> GetAllIds();
    void SaveHistoricalData(HistoricalStockListJsonModel historicalStockListJsonModel, int assetId);
    List<AssetHistoricalDataModel> GetLastRecords(List<int> assetsIds);
    List<AssetHistoricalDataModel> GetHistoricalDataByAssetsId(int assetId);
    List<AssetHistoricalDataModel> GetTopValuesByAssetId(List<int> assetsIds);
    List<AssetHistoricalDataModel> GetLastWeekRecords(List<int> assetsIds);
    void DeleteAssetHistoricalData(List<int> historicalDataIds);
}