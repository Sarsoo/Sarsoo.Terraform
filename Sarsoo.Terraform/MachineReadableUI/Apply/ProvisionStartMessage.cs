namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ProvisionStartMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}