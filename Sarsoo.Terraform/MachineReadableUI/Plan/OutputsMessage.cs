namespace Sarsoo.Terraform.MachineReadableUI.Plan;

public class OutputsMessage: BaseMessage
{
    public Dictionary<string, Outputs> Outputs { get; set; }
}