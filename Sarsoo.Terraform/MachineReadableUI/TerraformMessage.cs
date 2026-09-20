using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Action;
using Sarsoo.Terraform.MachineReadableUI.Apply;
using Sarsoo.Terraform.MachineReadableUI.Constant;
using Sarsoo.Terraform.MachineReadableUI.Drift;
using Sarsoo.Terraform.MachineReadableUI.Init;
using Sarsoo.Terraform.MachineReadableUI.Plan;
using Sarsoo.Terraform.MachineReadableUI.Query;
using Sarsoo.Terraform.MachineReadableUI.Util;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor
)]
[JsonDerivedType(typeof(VersionMessage), typeDiscriminator:  MruiConst.Type.Version)]
[JsonDerivedType(typeof(InitOutputMessage), typeDiscriminator: MruiConst.Type.Init.Output)]
[JsonDerivedType(typeof(LogMessage), typeDiscriminator: MruiConst.Type.Init.Log)]

[JsonDerivedType(typeof(ResourceDriftMessage), typeDiscriminator: MruiConst.MessageCode.OperationResults.Drift)]
[JsonDerivedType(typeof(PlannedChangeMessage), typeDiscriminator: MruiConst.MessageCode.OperationResults.PlannedChange)]
[JsonDerivedType(typeof(ChangeSummaryMessage), typeDiscriminator: MruiConst.MessageCode.OperationResults.ChangeSummary)]
[JsonDerivedType(typeof(OutputsMessage), typeDiscriminator: MruiConst.MessageCode.OperationResults.Outputs)]


[JsonDerivedType(typeof(ApplyStartMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Apply.Start)]
[JsonDerivedType(typeof(ApplyProgressMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Apply.Progress)]
[JsonDerivedType(typeof(ApplyCompleteMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Apply.Complete)]
[JsonDerivedType(typeof(ApplyErroredMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Apply.Errored)]

[JsonDerivedType(typeof(ProvisionStartMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Provision.Start)]
[JsonDerivedType(typeof(ProvisionProgressMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Provision.Progress)]
[JsonDerivedType(typeof(ProvisionCompleteMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Provision.Complete)]
[JsonDerivedType(typeof(ProvisionErroredMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Provision.Errored)]

[JsonDerivedType(typeof(EphemeralStartMessage), typeDiscriminator: "ephemeral_op_start")]
[JsonDerivedType(typeof(EphemeralProgressMessage), typeDiscriminator: "ephemeral_op_progress")]
[JsonDerivedType(typeof(EphemeralCompleteMessage), typeDiscriminator: "ephemeral_op_complete")]
[JsonDerivedType(typeof(EphemeralErroredMessage), typeDiscriminator: "ephemeral_op_errored")]

[JsonDerivedType(typeof(RefreshStartMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Refresh.Start)]
[JsonDerivedType(typeof(RefreshCompleteMessage), typeDiscriminator: MruiConst.MessageCode.ResourceProgress.Refresh.Complete)]

[JsonDerivedType(typeof(ActionInvocationMessage), typeDiscriminator: "planned_action_invocation")]

[JsonDerivedType(typeof(ListStartMessage), typeDiscriminator: "list_start")]
[JsonDerivedType(typeof(ListResourceFoundMessage), typeDiscriminator: "list_resource_found")]
[JsonDerivedType(typeof(ListCompleteMessage), typeDiscriminator: "list_complete")]
public class TerraformMessage
{
    /// <summary>
    /// this is normally "info", but can be "error" or "warn" when showing diagnostics
    /// </summary>
    [JsonPropertyName("@level")]
    public string Level { get; set; }
    /// <summary>
    /// a human-readable summary of the contents of this message
    /// </summary>
    [JsonPropertyName("@message")]
    public string Message { get; set; }
    /// <summary>
    /// always "terraform.ui" when rendering UI output
    /// </summary>
    [JsonPropertyName("@module")]
    public string Module { get; set; }
    /// <summary>
    /// an RFC3339 timestamp of when the message was output
    /// </summary>
    [JsonPropertyName("@timestamp")]
    public DateTime Timestamp { get; set; }
    
    public override string ToString() => $"{Timestamp}: {Level}: {Message}";
}