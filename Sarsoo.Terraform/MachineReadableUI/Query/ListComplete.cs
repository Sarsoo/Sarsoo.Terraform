using System.Text.Json;

namespace Sarsoo.Terraform.MachineReadableUI.Query;

public struct ListComplete
{
    public string Address { get; set; }
    public string ResourceType { get; set; }
    public int Total { get; set; }
}