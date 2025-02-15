using MarketDataService.Models.Json;

namespace MarketDataService.Services.AssetServices.Interfaces;

public interface IAssetService
{
    Task SaveAssetsAsync();
}