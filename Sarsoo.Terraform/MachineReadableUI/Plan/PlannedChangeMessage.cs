using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Plan;

/// <summary>
/// At the end of a plan or before an apply, Terraform will emit a planned_change message for each resource which has changes to apply. This message has an embedded change object with the following keys:
/// </summary>
public class PlannedChangeMessage: TerraformMessage
{
    public PlannedChange Change { get; set; }
}