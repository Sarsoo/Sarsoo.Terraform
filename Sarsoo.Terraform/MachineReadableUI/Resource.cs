using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.MachineReadableUI;

public struct Resource
{
    /// <summary>
    /// the full unique address of the resource as a string
    /// </summary>
    [JsonPropertyName("addr")]
    public string Address { get; set; }
    /// <summary>
    /// the address of the module containing the resource, in the form module.foo.module.bar, or an empty string for a root module resource
    /// </summary>
    [JsonPropertyName("module")]
    public string Module { get; set; }
    /// <summary>
    /// the module-relative address, which is identical to addr for root module resources
    /// </summary>
    [JsonPropertyName("resource")]
    public string RelativeAddress { get; set; }
    /// <summary>
    /// the type of resource being addressed
    /// </summary>
    [JsonPropertyName("resource_type")]
    public string Type { get; set; }
    /// <summary>
    /// the name label for the resource
    /// </summary>
    [JsonPropertyName("resource_name")]
    public string Name { get; set; }
    /// <summary>
    /// the address key (count or for_each value), or null if the neither are used
    /// </summary>
    [JsonPropertyName("resource_key")]
    [JsonConverter(typeof(StringOrNumberConverter))]
    public string? Key { get; set; }
    /// <summary>
    /// the provider type implied by the resource type; this may not reflect the resource's provider if provider aliases are used
    /// </summary>
    [JsonPropertyName("implied_provider")]
    public string ImpliedProvider { get; set; }
}