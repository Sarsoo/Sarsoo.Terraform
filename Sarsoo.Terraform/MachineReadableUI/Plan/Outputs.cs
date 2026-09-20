namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public struct Outputs
{
    public ResourceAction Action { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
    public bool Sensitive { get; set; }
}