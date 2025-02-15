using MarketDataService.Helpers.AssetHistoricalDataHelper;
using MarketDataService.Models.Asset;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class OptimizeAssetHistoricalDataService : IOptimizeAssetHistoricalDataService
{
    private readonly IAssetHistoricalDataHelper _assetHistoricalDataHelper;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OptimizeAssetHistoricalDataService(
        IAssetHistoricalDataHelper assetHistoricalDataHelper,
        IServiceScopeFactory serviceScopeFactory)
    {
        _assetHistoricalDataHelper = assetHistoricalDataHelper;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void OptimizeHistoricalData()
    {
        var unnecessaryIds = GetUnnecessaryIds();

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                ?? throw new NullReferenceException(
                                                    "Asset historical data repository is null");

            assetHistoricalDataRepository.DeleteAssetHistoricalData(unnecessaryIds);

        }
    }

   
    private List<int> GetUnnecessaryIds()
    {
        var allIds = new HashSet<int>(GetAllIdsFromHistoricalData()) ;
        var necessaryIds = new HashSet<int>(GetNecessaryIds()) ;
        
        allIds.ExceptWith(necessaryIds);

        return allIds.ToList();

    }

    private List<int> GetAllIdsFromHistoricalData()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                ?? throw new NullReferenceException(
                                                    "Asset historical data repository is null");

            return assetHistoricalDataRepository.GetAllIds();
        }
    }

    private List<int> GetNecessaryIds()
    {
        var lastWeekIds = new HashSet<int>(_assetHistoricalDataHelper.GetLastWeekRecordInHistoricalData()
            .Select(historicalData => historicalData.Id));
        var topValueIds = new HashSet<int>(_assetHistoricalDataHelper.GetTopValuesInHistoricalData()
            .Select(historicalData => historicalData.Id));

        lastWeekIds.UnionWith(topValueIds);
        
        return lastWeekIds.ToList();
    }
}