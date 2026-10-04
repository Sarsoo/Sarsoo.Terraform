using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.JsonOutput;

public struct Variable
{
    [JsonConverter(typeof(StringOrNumberConverter))]
    public string Value { get; set; }
    public string Type { get; set; }
    public bool Sensitive { get; set; }
}