using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonConverter(typeof(JsonStringEnumConverter<Reason>))]
public enum Reason
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
    [JsonStringEnumMemberName("delete_because_no_move_target")]
    NoMoveTarget,
    [JsonStringEnumMemberName("delete_because_each_key")]
    EachKey,
    [JsonStringEnumMemberName("delete_because_no_module")]
    MissingModule,
    
    // Action Reason
    [JsonStringEnumMemberName("replace_because_tainted")]
    ReplaceTainted,
    [JsonStringEnumMemberName("replace_because_cannot_update")]
    ReplaceCannotUpdate,
    [JsonStringEnumMemberName("replace_by_request")]
    ReplaceRequest,
    [JsonStringEnumMemberName("delete_because_no_resource_config")]
    DeleteMissingConfig,
    [JsonStringEnumMemberName("delete_because_no_module")]
    DeleteMissingModule,
    [JsonStringEnumMemberName("delete_because_wrong_repetition")]
    DeleteMissingRepetition,
    [JsonStringEnumMemberName("delete_because_count_index")]
    DeleteCountIndex,
    [JsonStringEnumMemberName("delete_because_each_key")]
    DeleteForEachKey,
    [JsonStringEnumMemberName("read_because_config_unknown")]
    ReadConfigUnknown,
    [JsonStringEnumMemberName("read_because_dependency_pending")]
    ReadDependencyPending,
}