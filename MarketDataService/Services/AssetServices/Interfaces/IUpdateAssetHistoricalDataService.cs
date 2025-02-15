using MarketDataService.Models.Asset;

namespace MarketDataService.Services.AssetServices.Interfaces;

public interface IUpdateAssetHistoricalDataService
{
    Task UpdateHistoricalDataAsync();
}