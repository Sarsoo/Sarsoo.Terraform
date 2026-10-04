using System.Text.Json;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Test.MachineReadableUI;

public class ResourceActionConverterTests
{
    [Theory]
    [InlineData("\"no-op\"", ResourceAction.NoOp)]
    [InlineData("\"noop\"", ResourceAction.NoOp)]
    [InlineData("\"NOOP\"", ResourceAction.NoOp)]
    [InlineData("\"replace\"", ResourceAction.Replace)]
    [InlineData("\"delete\"", ResourceAction.Delete)]
    [InlineData("\"import\"", ResourceAction.Import)]
    [InlineData("\"Replace\"", ResourceAction.Replace)]
    public void DeserializesKnownNames(string json, ResourceAction expected)
    {
        Assert.Equal(expected, JsonSerializer.Deserialize<ResourceAction>(json));
    }

    [Theory]
    [InlineData(ResourceAction.NoOp, "\"no-op\"")]
    [InlineData(ResourceAction.Replace, "\"replace\"")]
    [InlineData(ResourceAction.Import, "\"import\"")]
    public void SerializesAttributeNames(ResourceAction value, string expected)
    {
        Assert.Equal(expected, JsonSerializer.Serialize(value));
    }

    [Fact]
    public void UnknownNameThrows()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ResourceAction>("\"bogus\""));
    }
}
