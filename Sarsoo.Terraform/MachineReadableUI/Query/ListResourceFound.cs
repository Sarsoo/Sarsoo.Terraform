using System.Text.Json;

namespace Sarsoo.Terraform.MachineReadableUI.Query;

public struct ListResourceFound
{
    public string Address { get; set; }
    public string DisplayName { get; set; }
    public JsonElement Identity { get; set; }
    public int IdentityVersion { get; set; }
    public string ResourceType { get; set; }
}