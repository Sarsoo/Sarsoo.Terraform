namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ApplyErroredMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}