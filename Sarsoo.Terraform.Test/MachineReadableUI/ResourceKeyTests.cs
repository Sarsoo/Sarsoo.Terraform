using System.Text.Json;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.Test.MachineReadableUI;

public class ResourceKeyTests
{
    [Fact]
    public void DeserializesIntegerKeyAsString()
    {
        var resource = JsonSerializer.Deserialize("""{"resource_key": 0}""", MruiContext.Default.Resource);

        Assert.Equal("0", resource.Key);
    }

    [Fact]
    public void DeserializesStringKey()
    {
        var resource = JsonSerializer.Deserialize("""{"resource_key": "web-a"}""", MruiContext.Default.Resource);

        Assert.Equal("web-a", resource.Key);
    }

    [Fact]
    public void DeserializesNullKey()
    {
        var resource = JsonSerializer.Deserialize("""{"resource_key": null}""", MruiContext.Default.Resource);

        Assert.Null(resource.Key);
    }

    [Fact]
    public void DeserializesMissingKey()
    {
        var resource = JsonSerializer.Deserialize("""{}""", MruiContext.Default.Resource);

        Assert.Null(resource.Key);
    }

    [Fact]
    public void SerializesKeyAsString()
    {
        var resource = new Resource { Key = "0" };

        var json = JsonSerializer.Serialize(resource, MruiContext.Default.Resource);

        Assert.Contains("\"resource_key\":\"0\"", json);
    }
}
