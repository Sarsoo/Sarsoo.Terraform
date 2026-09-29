using System.Text.Json.Serialization;
using Sarsoo.Terraform.JsonOutput.Value;

namespace Sarsoo.Terraform.JsonOutput.State;

public class StateRepresentation
{
    [JsonPropertyName("terraform_version")]
    public string TerraformVersion { get; set; }
    public string FormatVersion { get; set; }
    public ValuesRepresentation Values { get; set; }
}