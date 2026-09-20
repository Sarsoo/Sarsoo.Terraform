using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Action;

public struct ActionAddress
{
    [JsonPropertyName("addr")]
    public string Address { get; set; }
    public string Module { get; set; }
    public string Action { get; set; }
    public string ImpliedProvider { get; set; }
    public string ActionType { get; set; }
    public string ActionName { get; set; }
    public string ActionKey { get; set; }
}