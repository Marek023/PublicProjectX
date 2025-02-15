using MarketDataService.Models.Asset;

namespace MarketDataService.Repositories.AssetRepositories.Interfaces;

public interface IAssetHistoricalRepository
{
    void SaveAssetHistorical(AssetHistoricalModel assetHistoricalModel);
}