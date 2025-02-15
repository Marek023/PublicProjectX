namespace MarketIndexAnalyzer.Models.AppSettings;

public class ApiSettings
{
    public string NasdaqISharesBaseUrl { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string DateFrom { get; set; } = string.Empty;
    public string DateTo { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
}