using System.Text.Json;
using MarketIndexAnalyzer.Enums;
using MarketIndexAnalyzer.Models.AppSettings;
using MarketIndexAnalyzer.Models.NasdaqIShares;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;
using MarketIndexAnalyzer.Services.IndexServices.Interfaces;
using Microsoft.Extensions.Options;

namespace MarketIndexAnalyzer.Services.IndexServices.Implementations;

public class NasdaqISharesService : INasdaqISharesService
{
    private readonly IOptions<ApiSettings> _apiSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public NasdaqISharesService(IOptions<ApiSettings> apiSettings, IServiceScopeFactory serviceScopeFactory)
    {
        _apiSettings = apiSettings;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task SaveHistoricalDataFromFinexAsync()
    {
        var historicalData = await GetHistoricalDataFromFinexAsync();
        SaveData(historicalData);
    }

    private async Task<ApiResponseModelJson> GetHistoricalDataFromFinexAsync()
    {
        var addres = GetHistoricalApi();

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(addres)
                                           ?? throw new HttpRequestException("No response");

            string content = await response.Content.ReadAsStringAsync();

            var deserializedData = JsonSerializer.Deserialize<ApiResponseModelJson>(content)
                                   ?? throw new NullReferenceException("Response is null");

            var records = deserializedData.DataModelJson.Records.Select(record => new RecordModelJson
            {
                Time = DateTime.Parse(record[0]?.ToString() ?? string.Empty),
                Open = record[1] is JsonElement openElement ? openElement.GetDouble() : Convert.ToDouble(record[1]),
                High = record[2] is JsonElement highElement ? highElement.GetDouble() : Convert.ToDouble(record[2]),
                Low = record[3] is JsonElement lowElement ? lowElement.GetDouble() : Convert.ToDouble(record[3]),
                Close = record[4] is JsonElement closeElement ? closeElement.GetDouble() : Convert.ToDouble(record[4])
            }).ToList();

            deserializedData.DataModelJson.RecordsData = records;

            return deserializedData;
        }
    }

    private string GetHistoricalApi()
    {
        var baseUrl = _apiSettings.Value.NasdaqISharesBaseUrl;
        var from = _apiSettings.Value.From;
        var to = _apiSettings.Value.To;
        var dateFrom = _apiSettings.Value.DateFrom;
        var dateTo = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var currency = _apiSettings.Value.Currency;
        var period = _apiSettings.Value.Period;

        return $"{baseUrl}{from}{dateFrom}{to}{dateTo}{currency}{period}";
    }

    private void SaveData(ApiResponseModelJson historicalData)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {

            var indexTypeRepository = scope.ServiceProvider.GetService<IIndexTypeRepository>()
                                      ?? throw new NullReferenceException("indexTypeRepository is null");

            var indexDataRepository = scope.ServiceProvider.GetService<IIndexDataRepository>()
                                      ?? throw new NullReferenceException("indexDataRepository is null");

            var indexType = indexTypeRepository.GetIndexTypeById((int)IndexTypeEnum.Nasdaq);
            indexDataRepository.SaveData(historicalData.DataModelJson.RecordsData, indexType);
            
        }
    }
}