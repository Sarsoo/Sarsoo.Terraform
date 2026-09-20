using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Drift;

/// <summary>
/// Terraform outputs a change summary when a plan or apply operation completes. Both message types include a changes object, which has the following keys:
/// </summary>
public class ChangeSummaryMessage: BaseMessage
{
    public ChangeSummary Changes { get; set; }
}