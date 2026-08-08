using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Apply;
using Sarsoo.Terraform.MachineReadableUI.Drift;
using Sarsoo.Terraform.MachineReadableUI.Plan;

namespace Sarsoo.Terraform.MachineReadableUI;
 
public struct FullMessage
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
    [JsonPropertyName("type")]
    public string Type { get; set; }

    ////////////////
    //  VERSION
    ////////////////

    /// <summary>
    /// the Terraform version which emitted this message
    /// </summary>
    [JsonPropertyName("terraform")]
    public string Version { get; set; }
    /// <summary>
    /// the machine-readable UI schema version defining the meaning of the following messages
    /// </summary>
    public string? Ui { get; set; }

    ////////////////////////
    //   RESOURCE DRIFT
    ////////////////////////

    [JsonPropertyName("change")]
    public PlannedChange? Change { get; set; }

    ////////////////////////
    //   CHANGE SUMMARY
    ////////////////////////

    [JsonPropertyName("changes")]
    public ChangeSummary? ChangeSummary { get; set; }

    ///////////////
    //   APPLY
    ///////////////

    public Hook? Hook { get; set; }

    ///////////////
    //   OUTPUT
    ///////////////

    public Dictionary<string, string>? Outputs { get; set; }

    public override string ToString() => Message;
}