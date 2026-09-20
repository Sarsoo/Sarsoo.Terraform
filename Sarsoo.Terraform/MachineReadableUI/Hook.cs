using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI;

public struct Hook
{
    public Resource Resource { get; set; }
    public ResourceAction Action { get; set; }

    /// <summary>
    /// time elapsed since the apply operation started, expressed as an integer number of seconds
    /// </summary>
    [JsonPropertyName("elapsed_seconds")]
    public int? Elapsed { get; set; }

    /// <summary>
    /// a key/value pair used to identify this instance of the resource, omitted when unknown
    /// </summary>
    [JsonPropertyName("id_key")]
    public string? IdKey { get; set; }
    /// <summary>
    /// a key/value pair used to identify this instance of the resource, omitted when unknown
    /// </summary>
    [JsonPropertyName("id_value")]
    public string? IdValue { get; set; }

    /// <summary>
    /// the type of provisioner
    /// </summary>
    public string? Provisioner { get; set; }

    /// <summary>
    /// the output log from the provisioner
    /// </summary>
    [JsonPropertyName("output")]
    public string? ProvisionerOutput { get; set; }


}