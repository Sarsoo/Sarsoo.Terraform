namespace Sarsoo.Terraform.MachineReadableUI.Apply;

public class ApplyProgressMessage: TerraformMessage
{
    public Hook Hook { get; set; }
}