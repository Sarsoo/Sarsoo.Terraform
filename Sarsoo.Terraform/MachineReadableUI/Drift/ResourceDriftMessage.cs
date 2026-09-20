namespace Sarsoo.Terraform.MachineReadableUI.Drift;

/// <summary>
/// If drift is detected during planning, Terraform will emit a resource_drift message for each resource which has changed outside of Terraform. This message has an embedded change object with the following keys:
/// </summary>
public class ResourceDriftMessage: TerraformMessage
{
    /// <summary>
    /// object describing the address of the resource to be changed; see resource object below for details
    /// </summary>
    public ResourceDriftChange Change { get; set; }
}