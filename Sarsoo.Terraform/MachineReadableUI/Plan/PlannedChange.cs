using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Plan;

/// <summary>
/// At the end of a plan or before an apply, Terraform will emit a planned_change message for each resource which has changes to apply. This message has an embedded change object with the following keys:
/// </summary>
public struct PlannedChange
{
    /// <summary>
    /// object describing the address of the resource to be changed; see resource object below for details
    /// </summary>
    public Resource Resource { get; set; }
    /// <summary>
    /// object describing the previous address of the resource, if this change includes a configuration-driven move
    /// </summary>
    [JsonPropertyName("previous_resource")]
    public Resource? PreviousResource { get; set; }
    /// <summary>
    /// the action planned to be taken for the resource. Values: update, delete.
    /// </summary>
    public ResourceAction Action { get; set; }
    /// <summary>
    /// an optional reason for the change, only used when the action is replace or delete.
    /// </summary>
    public Reason? Reason { get; set; }
}