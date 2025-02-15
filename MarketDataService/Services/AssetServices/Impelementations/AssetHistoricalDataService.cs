using System.Text.Json;
using ProjectX.Enums;
using MarketDataService.Models.AppSettings;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;
using Microsoft.Extensions.Options;
using ProjectX.Data.Entities;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class AssetHistoricalDataService : IAssetHistoricalDataService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IAssetCategoryService _assetCategoryService;
    private readonly IOptions<ApiSettings> _apiSettings;
    private readonly IUpdateAssetHistoricalDataService _updateAssetHistoricalDataService;
    private readonly IOptimizeAssetHistoricalDataService _optimizeAssetHistoricalDataService;

    public AssetHistoricalDataService(
        IServiceScopeFactory serviceScopeFactory,
        IAssetCategoryService assetCategoryService,
        IOptions<ApiSettings> apiSettings,
        IUpdateAssetHistoricalDataService updateAssetHistoricalDataService,
        IOptimizeAssetHistoricalDataService optimizeAssetHistoricalDataService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _assetCategoryService = assetCategoryService;
        _apiSettings = apiSettings;
        _updateAssetHistoricalDataService = updateAssetHistoricalDataService;
        _optimizeAssetHistoricalDataService = optimizeAssetHistoricalDataService;
    }

    public async Task SaveAssetHistoricalDataAsync()
    {
        var nasdaqAssetCategoryEnum = _assetCategoryService.GetNasdaqAssetCategoryEnum();
        var dowJonesCategoryEnum = _assetCategoryService.GetDowJonesCategoryEnum();
        var sp500AssetCategoryEnum = _assetCategoryService.GetSp500AssetCategoryEnum();

        var nasdaqAssets = GetAssetsByAssetCategory(nasdaqAssetCategoryEnum);
        var dowJonesAssets = GetAssetsByAssetCategory(dowJonesCategoryEnum);
        var sp500Assets = GetAssetsByAssetCategory(sp500AssetCategoryEnum);
        
        await SaveHistoricalDataAsync(nasdaqAssets);
        await SaveHistoricalDataAsync(dowJonesAssets);
        await SaveHistoricalDataAsync(sp500Assets);
        
         await _updateAssetHistoricalDataService.UpdateHistoricalDataAsync();
        _optimizeAssetHistoricalDataService.OptimizeHistoricalData();
    }


    private List<Assets> GetAssetsByAssetCategory(AssetCategoryEnum assetCategory)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>()
                ?? throw new NullReferenceException("Asset repository is null");
            

            List<Assets> assets = new List<Assets>();
            switch (assetCategory)
            {
                case AssetCategoryEnum.Nasdaq100:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
                case AssetCategoryEnum.DowJones:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
                case AssetCategoryEnum.Sp500:
                    assets = assetRepository.GetAssetsByAssetCategory(assetCategory);
                    break;
            }

            return assets;
        }
    }

    private async Task SaveHistoricalDataAsync(List<Assets> assets)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var asssetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                ?? throw new NullReferenceException("Asset historical data repository is null");

            (HistoricalStockListJsonModel historicalDataJson, int assetId) =
                await VerificationHistoricalDataAsync(assets, asssetHistoricalDataRepository);
            
            if (assetId > 0)
            {
                asssetHistoricalDataRepository.SaveHistoricalData(historicalDataJson, assetId);
            }
        }
    }

    private async Task<(HistoricalStockListJsonModel, int)> VerificationHistoricalDataAsync(
        List<Assets> assets,
        IAssetHistoricalDataRepository asssetHistoricalDataRepository)
    {
        var assetIds = new HashSet<int>(asssetHistoricalDataRepository.GetAssetsIds());

        foreach (var asset in assets)
        {
            if (!assetIds.Contains(asset.Id))
            {
                var historicalDataJson = await GetAssetHistoricalDataFromApiEndpointAsync(asset);
                return (historicalDataJson, asset.Id);
            }
        }

        return (new HistoricalStockListJsonModel(), 0);
    }

    private async Task<HistoricalStockListJsonModel> GetAssetHistoricalDataFromApiEndpointAsync(Assets asset)
    {
        var address = GetApiEndpoint(asset);

        var assetHistoricalData = new HistoricalStockListJsonModel();

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                assetHistoricalData = JsonSerializer.Deserialize<HistoricalStockListJsonModel>(content);
            }
        }

        return assetHistoricalData ?? throw new NullReferenceException("Asset historical data is null");
    }

    private string GetApiEndpoint(Assets asset)
    {
        var baseUrl = _apiSettings.Value.AssetHistoricalDataBaseUrl;
        var from = _apiSettings.Value.From;
        var to = _apiSettings.Value.To;
        var dateFrom = _apiSettings.Value.DateFrom;
        var dateTo = _apiSettings.Value.DateTo;
        var apiKeyWithAmpersand = _apiSettings.Value.ApiKeyWithAmpersand;
        var myApiKey = _apiSettings.Value.ApiKey;

        return baseUrl + asset.Symbol + from + dateFrom + to + dateTo + apiKeyWithAmpersand + myApiKey;
    }

    
}