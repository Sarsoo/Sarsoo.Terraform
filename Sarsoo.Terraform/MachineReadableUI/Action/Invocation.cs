using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Action;

public struct Invocation
{
    [JsonPropertyName("action_addr")]
    public string ActionAddress { get; set; }
}