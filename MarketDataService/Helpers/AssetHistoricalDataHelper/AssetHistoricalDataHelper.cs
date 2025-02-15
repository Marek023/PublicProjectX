using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;

namespace MarketDataService.Helpers.AssetHistoricalDataHelper;

public class AssetHistoricalDataHelper : IAssetHistoricalDataHelper
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AssetHistoricalDataHelper(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public List<AssetHistoricalDataModel> GetLastRecordInHistoricalData()
    {
        var assetIds = GetAllAssetIds();

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                ?? throw new NullReferenceException(
                                                    "Asset historical data repository is null");

            return assetHistoricalDataRepository.GetLastRecords(assetIds);
        }
    }

    public List<AssetHistoricalDataModel> GetTopValuesInHistoricalData()
    {
        var assetIds = GetAllAssetIds();
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                ?? throw new NullReferenceException(
                                                    "Asset historical data repository is null");
            
            return assetHistoricalDataRepository.GetTopValuesByAssetId(assetIds);
        }
    }
    
    public List<AssetHistoricalDataModel> GetLastWeekRecordInHistoricalData()
    {
        var assetIds = GetAllAssetIds();

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                ?? throw new NullReferenceException(
                                                    "Asset historical data repository is null");

            return assetHistoricalDataRepository.GetLastWeekRecords(assetIds);
        }
    }
    private List<int> GetAllAssetIds()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>()
                                  ?? throw new NullReferenceException("assetRepository is null");

            return assetRepository.GetAssetIds();
        }
    }
}