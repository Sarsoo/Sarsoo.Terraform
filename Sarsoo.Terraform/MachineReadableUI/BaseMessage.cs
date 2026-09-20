using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Drift;
using Sarsoo.Terraform.MachineReadableUI.Plan;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor
)]
[JsonDerivedType(typeof(Version), typeDiscriminator: "version")]
[JsonDerivedType(typeof(ResourceDriftMessage), typeDiscriminator: "resource_drift")]
[JsonDerivedType(typeof(PlannedChangeMessage), typeDiscriminator: "planned_change")]
[JsonDerivedType(typeof(ChangeSummaryMessage), typeDiscriminator: "change_summary")]
[JsonDerivedType(typeof(OutputsMessage), typeDiscriminator: "outputs")]
public class BaseMessage
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
    /// <summary>
    /// defines which kind of message this is and determines how to interpret other keys which may be present
    /// </summary>
    // [JsonPropertyName("type")]
    // public string Type { get; set; }
}