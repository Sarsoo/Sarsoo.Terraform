using System.Text.Json;

namespace Sarsoo.Terraform.MachineReadableUI.Query;

public struct ListStartBlock
{
    public string Address { get; set; }
    public string ResourceType { get; set; }
    public JsonElement InputConfig { get; set; }
    public List<string> SensitiveAttributePaths { get; set; }
}