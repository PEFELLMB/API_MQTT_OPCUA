using System.Text.Json.Serialization;
using core.Entities.Core;

namespace core.Entities;

public class ResponseDevice : Entity
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }

    public bool ValidateParams()
    {
        return !string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(Type);
    }
}