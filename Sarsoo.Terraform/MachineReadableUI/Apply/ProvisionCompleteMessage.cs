namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ProvisionCompleteMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}