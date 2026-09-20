namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ApplyCompleteMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}