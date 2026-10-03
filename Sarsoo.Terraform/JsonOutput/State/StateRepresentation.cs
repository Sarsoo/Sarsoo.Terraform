#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
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