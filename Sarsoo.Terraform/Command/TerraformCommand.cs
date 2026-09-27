using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using CliWrap;
using CliWrap.Buffered;
using Sarsoo.Terraform.Observability;

namespace Sarsoo.Terraform.Command;

public class TerraformCommand
{
    private CliWrap.Command Command { get; set; }
    
    private int? _exitCode = null;
    public int ExitCode => _exitCode ?? throw new InvalidOperationException("Command has not finished");
    public bool Errored => ((_exitCode ?? 0) > 0);

    public TerraformCommand(string executable)
    {
        Command = Cli.Wrap(executable)
            .WithEnvironmentVariables(e => e.Set("TF_IN_AUTOMATION", "true"))
            .WithValidation(CommandResultValidation.None);
    }

    public TerraformCommand Configure(Func<CliWrap.Command, CliWrap.Command> configure)
    {
        Command = configure.Invoke(Command);

        return this;
    }

    public async Task<string> Run(CancellationToken ct = default)
    {
        using var trace = Tracing.Source.StartActivity();
        trace?.AddTag(ObservabilityConstants.WorkingDirectory, Command.WorkingDirPath);
        var result = await Command.ExecuteBufferedAsync(cancellationToken: ct).ConfigureAwait(false);
        
        _exitCode = result.ExitCode;
        trace?.AddTag(ObservabilityConstants.ExitCode, _exitCode);
        trace?.SetStatus(Errored ? ActivityStatusCode.Error : ActivityStatusCode.Ok);

        return result.StandardOutput;
    }

    public async Task<T?> Run<T>(JsonTypeInfo<T> serialiserInfo, CancellationToken ct = default)
    {
        using var trace = Tracing.Source.StartActivity();
        trace?.AddTag(ObservabilityConstants.WorkingDirectory, Command.WorkingDirPath);
        var result = await Command.ExecuteBufferedAsync(cancellationToken: ct).ConfigureAwait(false);

        _exitCode = result.ExitCode;
        trace?.AddTag(ObservabilityConstants.ExitCode, _exitCode);
        trace?.SetStatus(Errored ? ActivityStatusCode.Error : ActivityStatusCode.Ok);
        
        return JsonSerializer.Deserialize(result, serialiserInfo);
    }
}