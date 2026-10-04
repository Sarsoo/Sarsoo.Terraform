using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public struct Outputs
{
    public ResourceAction Action { get; set; }
    
    [JsonConverter(typeof(StringOrNumberConverter))]
    public string Value { get; set; }
    public string Type { get; set; }
    public bool Sensitive { get; set; }
}