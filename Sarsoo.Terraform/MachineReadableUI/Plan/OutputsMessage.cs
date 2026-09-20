namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public class OutputsMessage: TerraformMessage
{
    public Dictionary<string, Outputs> Outputs { get; set; }
}