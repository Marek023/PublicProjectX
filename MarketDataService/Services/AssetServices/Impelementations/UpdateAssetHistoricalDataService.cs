using System.Text.Json;
using MarketDataService.Helpers.AssetHistoricalDataHelper;
using MarketDataService.Models.AppSettings;
using MarketDataService.Models.Asset;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;
using Microsoft.Extensions.Options;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class UpdateAssetHistoricalDataService : IUpdateAssetHistoricalDataService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptions<ApiSettings> _apiSettings;
    private readonly IAssetHistoricalDataHelper _assetHistoricalDataHelper;

    public UpdateAssetHistoricalDataService(
        IServiceScopeFactory scopeFactory,
        IOptions<ApiSettings> apiSettings,
        IAssetHistoricalDataHelper assetHistoricalDataHelper)
    {
        _scopeFactory = scopeFactory;
        _apiSettings = apiSettings;
        _assetHistoricalDataHelper = assetHistoricalDataHelper;
    }

    public async Task UpdateHistoricalDataAsync()
    {
        var lastRecordInHistoricalDatas = _assetHistoricalDataHelper.GetLastRecordInHistoricalData();
        await SaveHistoricalDataAsync(lastRecordInHistoricalDatas);
    }
  

    private async Task SaveHistoricalDataAsync(List<AssetHistoricalDataModel> lastRecordInHistoricalDatas)
    {
        int batchSize = 5;

        for (int i = 0; i < lastRecordInHistoricalDatas.Count; i += batchSize)
        {
            var currentBatchHistoricalDataModels = lastRecordInHistoricalDatas.Skip(i)
                .Take(batchSize)
                .Where(model => model.Date != DateTime.Today.ToString("yyyy-M-d"))
                .ToList();

            if (!currentBatchHistoricalDataModels.Any())
            {
                continue;
            }

            (string address, int countSymbols) = GetApiEndpoint(currentBatchHistoricalDataModels);
            var historicalData = await GetAssetHistoricalDataAsync(address, countSymbols);
            var historicalDataWithAssetId =
                AddedAssetIdToHistoricalData(currentBatchHistoricalDataModels, historicalData);

            SaveHistoricalData(historicalDataWithAssetId);
        }
    }


    private (string, int) GetApiEndpoint(List<AssetHistoricalDataModel> currentBatchHistoricalDataModels)
    {
        var oldestDate = GetOldestDate(currentBatchHistoricalDataModels);
        (string symbols, int countSymbols) = GetSymbols(currentBatchHistoricalDataModels);

        //https://financialmodelingprep.com/api/v3/historical-price-full/AAPL,MMM?from=2024-12-06&to=2024-12-08&apikey=PANfUVwI9xpJc7QyW2UedfVhqDswQRMX

        var baseUrl = _apiSettings.Value.AssetHistoricalDataBaseUrl;
        var from = _apiSettings.Value.From;
        var to = _apiSettings.Value.To;
        var dateTo = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var apiKeyWithAmpersand = _apiSettings.Value.ApiKeyWithAmpersand;
        var myApiKey = _apiSettings.Value.ApiKey;

        return ($"{baseUrl}{symbols}{from}{oldestDate}{to}{dateTo}{apiKeyWithAmpersand}{myApiKey}", countSymbols);
    }

    private string GetOldestDate(List<AssetHistoricalDataModel> currentBatchHistoricalDataModels)
    {
        return currentBatchHistoricalDataModels.OrderBy(historicalData => historicalData.Date)
            .First().Date;
    }

    private (string, int) GetSymbols(List<AssetHistoricalDataModel> currentBatchHistoricalDataModels)
    {
        string symbols = string.Empty;
        int countSymbols = 0;

        foreach (var assetHistoricalDataModel in currentBatchHistoricalDataModels)
        {
            symbols += assetHistoricalDataModel.AssetSymbol + ",";
            countSymbols++;
        }

        return (symbols.TrimEnd(','), countSymbols);
    }

    private async Task<HistoricalStockRootJsonModel> GetAssetHistoricalDataAsync(string address, int countSymbols)
    {
        var assetHistoricalData = new HistoricalStockRootJsonModel();
        

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (countSymbols > 1)
                {
                    assetHistoricalData = JsonSerializer.Deserialize<HistoricalStockRootJsonModel>(content);
                }
                else
                {
                    var historicalData = JsonSerializer.Deserialize<HistoricalStockListJsonModel>(content);
                    if (historicalData != null) assetHistoricalData.historicalStockList.Add(historicalData);
                }
            }
        }


        return assetHistoricalData ?? throw new NullReferenceException("Asset historical data is null");
    }

    private HistoricalStockRootJsonModel AddedAssetIdToHistoricalData(
        List<AssetHistoricalDataModel> currentBatchHistoricalDataModels,
        HistoricalStockRootJsonModel historicalData)
    {
        foreach (var historicalStock in historicalData.historicalStockList)
        {
            var assetId = currentBatchHistoricalDataModels.FirstOrDefault(currentBatch =>
                currentBatch.AssetSymbol == historicalStock.symbol)?.AssetsId;

            historicalStock.AssetId = assetId ?? throw new NullReferenceException("Asset ID is null");
        }

        return historicalData;
    }

    private void SaveHistoricalData(HistoricalStockRootJsonModel historicalStockRootJsonModel)
    {
        // stáhnout z repository datumy podle assetId porovnat jestli je shoda když shoda není a neexistuje tak uložit

        foreach (var historicalStock in historicalStockRootJsonModel.historicalStockList)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var assetHistoricalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                                    ?? throw new NullReferenceException(
                                                        "Asset historical data repository is null");

                assetHistoricalDataRepository.SaveHistoricalData(historicalStock, historicalStock.AssetId);
            }
        }
    }
}