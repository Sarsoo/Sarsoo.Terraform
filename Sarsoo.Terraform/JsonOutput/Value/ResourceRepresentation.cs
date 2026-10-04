#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using System.Text.Json;
using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.JsonOutput.Value;

public class ResourceRepresentation
{
    public string Address { get; set; }
    public ResourceMode Mode { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(StringOrNumberConverter))]
    public string? Index { get; set; }
    [JsonPropertyName("provider_name")]
    public string Provider { get; set; }
    public int SchemaVersion { get; set; }

    // commented out while serilialisation doesn't work
    public JsonElement Values { get; set; }
    public JsonElement SensitiveValues { get; set; }
}