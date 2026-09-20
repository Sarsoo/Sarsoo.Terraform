namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ApplyStartMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}