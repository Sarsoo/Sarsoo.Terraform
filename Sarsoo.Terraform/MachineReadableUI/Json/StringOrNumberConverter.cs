using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sarsoo.Terraform.MachineReadableUI.Json;

/// <summary>
/// Reads a JSON value that may be either a string or a number into a string.
/// Numbers are converted using their invariant raw JSON representation
/// (for example, the number 0 becomes "0"). Values are written back as JSON
/// strings.
/// </summary>
public sealed class StringOrNumberConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => Encoding.UTF8.GetString(reader.ValueSpan),
            _ => throw new JsonException($"Cannot convert JSON token '{reader.TokenType}' to a string.")
        };

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value);
    }
}
