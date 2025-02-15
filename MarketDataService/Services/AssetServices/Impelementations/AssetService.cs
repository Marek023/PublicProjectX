using System.Text.Json;
using HtmlAgilityPack;
using ProjectX.Enums;
using MarketDataService.Models.AppSettings;
using MarketDataService.Models.Json;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Services.AssetServices.Interfaces;
using Microsoft.Extensions.Options;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class AssetService : IAssetService
{
    private readonly IAssetCategoryService _assetCategoryService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IAssetControlService _assetControlService;
    private readonly IOptions<ApiSettings> _apiSettings;

    public AssetService(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<ApiSettings> apiSettings,
        IAssetCategoryService assetCategoryService,
        IAssetControlService assetControlService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _apiSettings = apiSettings;
        _assetCategoryService = assetCategoryService;
        _assetControlService = assetControlService;
    }

    public async Task SaveAssetsAsync()
    {
        var nasdaqAssetCategoryEnum = _assetCategoryService.GetNasdaqAssetCategoryEnum();
        var dowJonesCategoryEnum = _assetCategoryService.GetDowJonesCategoryEnum();
        var sp500AssetCategoryEnum = _assetCategoryService.GetSp500AssetCategoryEnum();

        var nasdaqAssets = await GetAssetsFromEndpointAsync(nasdaqAssetCategoryEnum);
        var dowJonesAssets = await GetAssetsFromEndpointAsync(dowJonesCategoryEnum);
        var sp500Assets = await GetSp500Async();

        _assetControlService.CheckAsset(nasdaqAssets, nasdaqAssetCategoryEnum);
        _assetControlService.CheckAsset(dowJonesAssets, dowJonesCategoryEnum);
        _assetControlService.CheckAsset(sp500Assets, sp500AssetCategoryEnum);
        
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetRepository = scope.ServiceProvider.GetService<IAssetRepository>()
                ?? throw new NullReferenceException("assetRepository is null");
            var assetCategoryRepository = scope.ServiceProvider.GetService<IAssetCategoryRepository>()
                ?? throw new NullReferenceException("assetCategoryRepository is null");
           
            var nasdaqAssetCategory = assetCategoryRepository.GetAssetCategory((int)nasdaqAssetCategoryEnum);
            var dowJonesAssetCategory = assetCategoryRepository.GetAssetCategory((int)dowJonesCategoryEnum);
            var sp500AssetCategory = assetCategoryRepository.GetAssetCategory((int)sp500AssetCategoryEnum);

            assetRepository.SaveAssets(nasdaqAssets, nasdaqAssetCategory);
            assetRepository.SaveAssets(dowJonesAssets, dowJonesAssetCategory);
            assetRepository.SaveAssets(sp500Assets, sp500AssetCategory);
        }
    }

    private async Task<List<AssetJsonModel>> GetAssetsFromEndpointAsync(AssetCategoryEnum assetCategoryEnum)
    {
        string address = string.Empty;
        switch (assetCategoryEnum)
        {
            case AssetCategoryEnum.Nasdaq100: address = GetNasdaqApiEndpoint(); break;
            case AssetCategoryEnum.DowJones: address = GetDowJonesApiEndpoint(); break;
        }

        var assets = new List<AssetJsonModel>();

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                assets = JsonSerializer.Deserialize<List<AssetJsonModel>>(content);
            }
        }

        return assets ?? new List<AssetJsonModel>();
    }

    private string GetNasdaqApiEndpoint()
    {
        var assetNasdaqBaseUrl = _apiSettings.Value.AssetNasdaqBaseUrl;
        return assetNasdaqBaseUrl + GetMyApiKey();
    }

    private string GetDowJonesApiEndpoint()
    {
        var assetNasdaqBaseUrl = _apiSettings.Value.AssetDowJonesBaseUrl;
        return assetNasdaqBaseUrl + GetMyApiKey();
    }

    private string GetMyApiKey()
    {
        var myApiKey = _apiSettings.Value.ApiKey;
        var apiKeyWithQuestionMark = _apiSettings.Value.ApiKeyWithQuestionMark;
        return apiKeyWithQuestionMark + myApiKey;
    }

    private async Task<List<AssetJsonModel>> GetSp500Async()
    {
        var rows = await GetSp500dataFromWikiTableAsync();

        var headers = rows[0].SelectNodes(".//th").Select(th => th.InnerText.Trim()).ToList();

        var renameHeaders = GetRenameHeadersForJsonModel(headers);

        var sp500Companies = GetSp500CompaniesFromTableRows(rows, renameHeaders);

        var assetJsonModels = GetAssetJsonModelFromSp500Companies(sp500Companies);

        return assetJsonModels;
    }

    private async Task<HtmlNodeCollection> GetSp500dataFromWikiTableAsync()
    {
        var url = "https://en.wikipedia.org/wiki/List_of_S%26P_500_companies";
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(url);

        var table = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'wikitable')]");
        var rows = table.SelectNodes(".//tr");

        return rows;
    }

    private List<string> GetRenameHeadersForJsonModel(List<string> headers)
    {
        var renameHeaders = new List<string>();
        foreach (var header in headers)
        {
            switch (header)
            {
                case "Symbol": renameHeaders.Add("symbol"); break;
                case "Security": renameHeaders.Add("name"); break;
                case "GICS Sector": renameHeaders.Add("sector"); break;
                case "GICS Sub-Industry": renameHeaders.Add("subSector"); break;
                case "Headquarters Location": renameHeaders.Add("headQuarter"); break;
                case "Date added": renameHeaders.Add("dateFirstAdded"); break;
                case "CIK": renameHeaders.Add("cik"); break;
                case "Founded": renameHeaders.Add("founded"); break;
            }
        }

        return renameHeaders;
    }

    private List<Dictionary<string, string>> GetSp500CompaniesFromTableRows(
        HtmlNodeCollection rows,
        List<string> renameHeaders)
    {
        var sp500Companies = new List<Dictionary<string, string>>();
        foreach (var row in rows.Skip(1))
        {
            var cells = row.SelectNodes(".//td");
            if (cells != null && cells.Count == renameHeaders.Count)
            {
                var companyData = new Dictionary<string, string>();
                for (int i = 0; i < renameHeaders.Count; i++)
                {
                    companyData[renameHeaders[i]] = cells[i].InnerText.Trim();
                }

                sp500Companies.Add(companyData);
            }
        }

        return sp500Companies;
    }

    private List<AssetJsonModel> GetAssetJsonModelFromSp500Companies(List<Dictionary<string, string>> sp500Companies)
    {
        var assetJsonModels =
            JsonSerializer.Deserialize<List<AssetJsonModel>>(JsonSerializer.Serialize(sp500Companies));
        
        if (assetJsonModels == null) throw new NullReferenceException("assetJsonModels is null");

        return assetJsonModels;
    }
}