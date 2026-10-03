#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using Sarsoo.Terraform.JsonOutput.Value;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.JsonOutput.Change;

public class ChangeRepresentation
{
    /// <summary>
    /// "actions" are the actions that will be taken on the object selected by the
    /// properties below.
    /// Valid actions values are:
    ///    ["no-op"]
    ///    ["create"]
    ///    ["read"]
    ///    ["update"]
    ///    ["delete", "create"]
    ///    ["create", "delete"]
    ///    ["delete"]
    /// The two "replace" actions are represented in this way to allow callers to
    /// e.g. just scan the list for "delete" to recognize all three situations
    /// where the object will be deleted, allowing for any new deletion
    /// combinations that might be added in future.
    /// </summary>
    public List<ResourceAction> Actions { get; set; }

    public ValuesRepresentation? Before { get; set; }
    public ValuesRepresentation? After { get; set; }
}