namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class EphemeralErroredMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}