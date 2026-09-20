using Sarsoo.Terraform.MachineReadableUI.Apply;

namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public class RefreshCompleteMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}