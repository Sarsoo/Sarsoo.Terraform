#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Sarsoo.Terraform.MachineReadableUI.Init;

public class InitOutputMessage: TerraformMessage
{
    public string MessageCode { get; set; }
}