using Sarsoo.Terraform.MachineReadableUI.Apply;

namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public class RefreshStartMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}