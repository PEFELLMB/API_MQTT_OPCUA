using System.Text.Json.Serialization;

namespace api.ViewModels;

public class ResponseDevice
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
}