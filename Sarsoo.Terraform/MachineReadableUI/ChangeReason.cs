using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonConverter(typeof(JsonStringEnumConverter<ChangeReason>))]
public enum ChangeReason
{
    [JsonStringEnumMemberName("tainted")]
    Tainted,
    [JsonStringEnumMemberName("requested")]
    Requested,
    [JsonStringEnumMemberName("cannot_update")]
    CannotUpdate,
    [JsonStringEnumMemberName("delete_because_no_resource_config")]
    MissingResourceConfig,
    [JsonStringEnumMemberName("delete_because_wrong_repetition")]
    WrongRepetition,
    [JsonStringEnumMemberName("delete_because_count_index")]
    CountIndex,
    [JsonStringEnumMemberName("delete_because_each_key")]
    EachKey,
    [JsonStringEnumMemberName("delete_because_no_module")]
    MissingModule,
}