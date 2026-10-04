using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonConverter(typeof(JsonStringEnumConverter<ResourceAction>))]
public enum ResourceAction
{
    [JsonStringEnumMemberName("no-op")]
    NoOp,
    [JsonStringEnumMemberName("create")]
    Create,
    [JsonStringEnumMemberName("read")]
    Read,
    [JsonStringEnumMemberName("start")]
    Start,
    [JsonStringEnumMemberName("open")]
    Open,
    [JsonStringEnumMemberName("close")]
    Close,
    [JsonStringEnumMemberName("update")]
    Update,
    [JsonStringEnumMemberName("replace")]
    Replace,
    [JsonStringEnumMemberName("delete")]
    Delete,
    [JsonStringEnumMemberName("move")]
    Move,
    [JsonStringEnumMemberName("import")]
    Import
}