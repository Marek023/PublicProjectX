using System.Text.Json;
using ProjectX.Enums;
using MarketDataService.Models.AppSettings;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;
using Microsoft.Extensions.Options;
using ProjectX.Data.Entities;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class AssetHistoricalService : IAssetHistoricalService
{
    private readonly IAssetCategoryService _assetCategoryService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IOptions<ApiSettings> _apiSettings;

    public AssetHistoricalService(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<ApiSettings> apiSettings,
        IAssetCategoryService assetCategoryService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _apiSettings = apiSettings;
        _assetCategoryService = assetCategoryService;
    }

    public async Task SaveAssetHistoricalAsync()
    {
        var nasdaqAssetCategoryEnum = _assetCategoryService.GetNasdaqAssetCategoryEnum();
        var dowJonesAssetCategoryEnum = _assetCategoryService.GetDowJonesCategoryEnum();
        var sp500AssetCategoryEnum = _assetCategoryService.GetSp500AssetCategoryEnum();

        var nasdaqAssetHistorical = await GetAssetHistoricalFromEndpointAsync(nasdaqAssetCategoryEnum);
        var dowJonesAssetHistorical = await GetAssetHistoricalFromEndpointAsync(dowJonesAssetCategoryEnum);

        SaveAssetHistorical(nasdaqAssetHistorical, nasdaqAssetCategoryEnum);
        SaveAssetHistorical(dowJonesAssetHistorical, dowJonesAssetCategoryEnum);

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>();

            if (assetRepository == null)
            {
                throw new Exception("Asset historical repository is null");
            }

            var sp500Asset = assetRepository.GetAssetsByAssetCategory(sp500AssetCategoryEnum);
            SaveNasdaqAssetHistorical(sp500Asset, nasdaqAssetCategoryEnum);
        }
    }

    private async Task<List<AssetHistoricalJsonModel>> GetAssetHistoricalFromEndpointAsync(
        AssetCategoryEnum assetCategory)
    {
        string adress = string.Empty;
        switch (assetCategory)
        {
            case AssetCategoryEnum.Nasdaq100: adress = GetNasdaqApiEndpoint(); break;
            case AssetCategoryEnum.DowJones: adress = GetDowJonesApiEndpoint(); break;
        }

        var assetHistoricalJsonModels = new List<AssetHistoricalJsonModel>();
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(adress);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                assetHistoricalJsonModels = JsonSerializer.Deserialize<List<AssetHistoricalJsonModel>>(content);
            }
        }

        return assetHistoricalJsonModels ?? new List<AssetHistoricalJsonModel>();
    }

    private string GetNasdaqApiEndpoint()
    {
        var historicalNasdaq = _apiSettings.Value.AssetHistoricalNasdaqBaseUrl;
        return historicalNasdaq + GetMyApiKey();
    }

    private string GetDowJonesApiEndpoint()
    {
        var historicalDowJones = _apiSettings.Value.AssetHistoricalDowJonesBaseUrl;
        return historicalDowJones + GetMyApiKey();
    }

    private string GetMyApiKey()
    {
        var myApiKey = _apiSettings.Value.ApiKey;
        var apiKeyWithQuestionMark = _apiSettings.Value.ApiKeyWithQuestionMark;
        return apiKeyWithQuestionMark + myApiKey;
    }

    private void SaveAssetHistorical(
        List<AssetHistoricalJsonModel> assetHistoricalJsonModels,
        AssetCategoryEnum assetCategoryEnum)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>();
            var assetHistoricalRepository = scope.ServiceProvider.GetService<IAssetHistoricalRepository>();

            if (assetRepository == null || assetHistoricalRepository == null)
            {
                throw new Exception("Asset repository is null");
            }

            var assets = assetRepository.GetAssetsByAssetCategory(assetCategoryEnum);

            foreach (var asset in assets)
            {
                var assetHistorical =
                    assetHistoricalJsonModels.FirstOrDefault(assetHistorical => assetHistorical.symbol == asset.Symbol);
                if (assetHistorical is null) continue;

                var assetHistoricalModel = new AssetHistoricalModel()
                {
                    DateAdded = assetHistorical.dateAdded,
                    AddedSecurity = assetHistorical.addedSecurity,
                    RemovedTicker = assetHistorical.removedTicker,
                    RemovedSecurity = assetHistorical.removedSecurity,
                    Date = assetHistorical.date,
                    Symbol = assetHistorical.symbol,
                    Reason = assetHistorical.reason,
                    DateSave = DateTime.UtcNow,
                    AssetId = asset.Id
                };

                assetHistoricalRepository.SaveAssetHistorical(assetHistoricalModel);
            }
        }
    }

    private void SaveNasdaqAssetHistorical(List<Assets> nasdaqAssets, AssetCategoryEnum assetCategoryEnum)
    {
        foreach (var asset in nasdaqAssets)
        {
            var assetHistoricalModel = new AssetHistoricalModel()
            {
                DateAdded = asset.DateFirstAdded,
                AddedSecurity = asset.Name,
                Date = asset.DateFirstAdded,
                Symbol = asset.Symbol,
                DateSave = DateTime.UtcNow,
                AssetId = asset.Id
            };

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var assetHistoricalRepository = scope.ServiceProvider.GetService<IAssetHistoricalRepository>();
                if (assetHistoricalRepository is null)
                {
                    throw new Exception("Asset historical repository is null");
                }
                assetHistoricalRepository.SaveAssetHistorical(assetHistoricalModel);
            }
        }
    }
}