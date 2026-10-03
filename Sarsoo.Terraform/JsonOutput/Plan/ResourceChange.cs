#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using Sarsoo.Terraform.JsonOutput.Change;
using Sarsoo.Terraform.JsonOutput.Value;

namespace Sarsoo.Terraform.JsonOutput.Plan;

public class ResourceChange
{
    public string Address { get; set; }
    public string? PreviousAddress { get; set; }
    public string? ModuleAddress { get; set; }
    public ResourceMode Mode { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string? Index { get; set; }
    public string? Deposed { get; set; }
    public ChangeRepresentation? Change { get; set; }

    public ActionReason ActionReason { get; set; }
}