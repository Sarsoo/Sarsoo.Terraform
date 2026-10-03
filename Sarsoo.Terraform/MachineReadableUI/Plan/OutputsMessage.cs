#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public class OutputsMessage: TerraformMessage
{
    public Dictionary<string, Outputs> Outputs { get; set; }
}