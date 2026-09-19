using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using CliWrap;
using CliWrap.Buffered;

namespace Sarsoo.Terraform.Command;

public class TerraformCommand<T> where T: notnull
{
    private readonly JsonTypeInfo<T> _serialiserInfo;
    private CliWrap.Command Command { get; set; }

    public TerraformCommand(string executable, JsonTypeInfo<T> serialiserInfo)
    {
        _serialiserInfo = serialiserInfo;
        Command = Cli.Wrap(executable)
            .WithEnvironmentVariables(e => e.Set("TF_IN_AUTOMATION", "true"))
            .WithValidation(CommandResultValidation.None);
    }

    public TerraformCommand<T> Configure(Func<CliWrap.Command, CliWrap.Command> configure)
    {
        Command = configure.Invoke(Command);

        return this;
    }

    public async Task<T?> Run(CancellationToken ct = default)
    {
        var result = await Command.ExecuteBufferedAsync(cancellationToken: ct);

        return JsonSerializer.Deserialize(result, _serialiserInfo);
    }
}