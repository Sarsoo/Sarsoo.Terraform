using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Channels;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;

namespace Sarsoo.Terraform.Command;

public class TerraformStreamCommand<T> where T : notnull
{
    private readonly ILogger<TerraformStreamCommand<T>>? _logger;

    private readonly JsonTypeInfo<T> _serialiserInfo;
    private readonly Channel<T> Messages = Channel.CreateUnbounded<T>();
    private CliWrap.Command Command { get; set; }

    public ChannelReader<T> Output => Messages.Reader;

    public TerraformStreamCommand(string executable, JsonTypeInfo<T> serialiserInfo,
        ILogger<TerraformStreamCommand<T>>? logger = null)
    {
        _serialiserInfo = serialiserInfo;
        _logger = logger;
        Command = Cli.Wrap(executable)
            .WithEnvironmentVariables(e => e.Set("TF_IN_AUTOMATION", "true"))
            .WithValidation(CommandResultValidation.None);
    }

    public TerraformStreamCommand<T> Configure(Func<CliWrap.Command, CliWrap.Command> configure)
    {
        Command = configure.Invoke(Command);

        return this;
    }

    public async Task Run(CancellationToken ct = default)
    {
        Exception? exception = null;
        try
        {
            await foreach (var cmdEvent in Command.ListenAsync(cancellationToken: ct))
            {
                try
                {
                    switch (cmdEvent)
                    {
                        case StartedCommandEvent started:
                            _logger?.LogInformation("Process started; ID: {ProcessId}", started.ProcessId);
                            break;
                        case StandardOutputCommandEvent stdOut:

                            var message = JsonSerializer.Deserialize(stdOut.Text, _serialiserInfo);

                            await Messages.Writer.WriteAsync(message, ct);

                            break;
                        case StandardErrorCommandEvent stdErr:
                            _logger?.LogError(stdErr.Text);
                            break;
                        case ExitedCommandEvent exited:
                            _logger?.LogInformation("Process exited; Code: {ExitCode}", exited.ExitCode);
                            break;
                    }
                }
                catch (Exception e)
                {
                    exception = e;
                    _logger?.LogError(e, "Exception occured while running Terraform command");
                }
            }
        }
        catch (Exception e)
        {
            exception = e;
            _logger?.LogError(e, "Exception occured while running Terraform command");
        }
        finally
        {
            Messages.Writer.Complete(exception);
        }
    }
}