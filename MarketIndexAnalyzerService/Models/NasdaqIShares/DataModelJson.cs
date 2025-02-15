using System.Text.Json.Serialization;

namespace MarketIndexAnalyzer.Models.NasdaqIShares;

public class DataModelJson
{
    [JsonPropertyName("asset")]
    public string? Asset { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; }

    [JsonPropertyName("records")]
    public List<object[]> Records { get; set; } = new();

    public List<RecordModelJson> RecordsData {get; set; } = new();
}