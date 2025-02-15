namespace MarketDataService.Models.AppSettings;

public class ApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string ApiKeyWithQuestionMark { get; set; } = string.Empty;
    public string ApiKeyWithAmpersand { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string DateFrom { get; set; } = string.Empty;
    public string DateTo { get; set; } = string.Empty;
    public string AssetNasdaqBaseUrl { get; set; } = string.Empty;
    public string AssetDowJonesBaseUrl { get; set; } = string.Empty;
    public string AssetHistoricalNasdaqBaseUrl { get; set; } = string.Empty;
    public string AssetHistoricalDowJonesBaseUrl { get; set; } = string.Empty;
    public string AssetHistoricalDataBaseUrl { get; set; } = string.Empty;
}