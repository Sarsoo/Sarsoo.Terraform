using System.Collections.Frozen;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI;

[JsonConverter(typeof(ResourceActionJsonConverter))]
public enum ResourceAction
{
    NoOp,
    Create,
    Read,
    Start,
    Open,
    Close,
    Update,
    Replace,
    Delete,
    Move,
    Import,
    Remove
}

/// <summary>
/// Behaves like <c>JsonStringEnumConverter&lt;ResourceAction&gt;</c>, while additionally accepting
/// "noop" as an alias for "no-op" when reading. Uses explicit static mappings so it is reflection-free
/// and AOT/trim friendly.
/// </summary>
public sealed class ResourceActionJsonConverter : JsonConverter<ResourceAction>
{
    // Reading is case-insensitive, matching the default JsonStringEnumConverter behaviour.
    private static readonly FrozenDictionary<string, ResourceAction> ReadNames =
        new Dictionary<string, ResourceAction>(StringComparer.OrdinalIgnoreCase)
        {
            ["no-op"] = ResourceAction.NoOp,
            ["noop"] = ResourceAction.NoOp,
            ["create"] = ResourceAction.Create,
            ["read"] = ResourceAction.Read,
            ["start"] = ResourceAction.Start,
            ["open"] = ResourceAction.Open,
            ["close"] = ResourceAction.Close,
            ["update"] = ResourceAction.Update,
            ["replace"] = ResourceAction.Replace,
            ["delete"] = ResourceAction.Delete,
            ["move"] = ResourceAction.Move,
            ["import"] = ResourceAction.Import,
            ["remove"] = ResourceAction.Remove,
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private static readonly FrozenDictionary<ResourceAction, string> WriteNames =
        new Dictionary<ResourceAction, string>
        {
            [ResourceAction.NoOp] = "no-op",
            [ResourceAction.Create] = "create",
            [ResourceAction.Read] = "read",
            [ResourceAction.Start] = "start",
            [ResourceAction.Open] = "open",
            [ResourceAction.Close] = "close",
            [ResourceAction.Update] = "update",
            [ResourceAction.Replace] = "replace",
            [ResourceAction.Delete] = "delete",
            [ResourceAction.Move] = "move",
            [ResourceAction.Import] = "import",
            [ResourceAction.Remove] = "remove",
        }.ToFrozenDictionary();

    public override ResourceAction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var numeric))
            return (ResourceAction)numeric;

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Unexpected token {reader.TokenType} when reading {nameof(ResourceAction)}.");

        var value = reader.GetString()!;
        if (ReadNames.TryGetValue(value, out var action))
            return action;

        throw new JsonException($"Unknown {nameof(ResourceAction)} value '{value}'.");
    }

    public override void Write(Utf8JsonWriter writer, ResourceAction value, JsonSerializerOptions options)
    {
        if (WriteNames.TryGetValue(value, out var name))
            writer.WriteStringValue(name);
        else
            writer.WriteNumberValue((int)value);
    }
}
