namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ProvisionProgressMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}