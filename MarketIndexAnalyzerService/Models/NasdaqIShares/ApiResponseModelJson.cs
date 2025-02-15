using System.Text.Json.Serialization;

namespace MarketIndexAnalyzer.Models.NasdaqIShares;

public class ApiResponseModelJson
{
    [JsonPropertyName("success")] 
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public DataModelJson DataModelJson { get; set; } = new();
}