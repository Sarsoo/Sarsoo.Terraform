namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ProvisionErroredMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}