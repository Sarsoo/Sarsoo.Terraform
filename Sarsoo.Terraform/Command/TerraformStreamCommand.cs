using System.Text.Json;
using System.Threading.Channels;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.Command;

public class TerraformStreamCommand
{
    private readonly ILogger<TerraformStreamCommand>? _logger;
    
    private readonly Channel<TerraformMessage> _messages = Channel.CreateUnbounded<TerraformMessage>();
    private CliWrap.Command Command { get; set; }

    public ChannelReader<TerraformMessage> Output => _messages.Reader;

    public TerraformStreamCommand(string executable, ILogger<TerraformStreamCommand>? logger = null)
    {
        _logger = logger;
        Command = Cli.Wrap(executable)
            .WithEnvironmentVariables(e => e.Set("TF_IN_AUTOMATION", "true"))
            .WithValidation(CommandResultValidation.None);
    }

    public TerraformStreamCommand Configure(Func<CliWrap.Command, CliWrap.Command> configure)
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

                            var message = JsonSerializer.Deserialize(stdOut.Text, MruiContext.Default.TerraformMessage);
                            
                            if (message is not null)
                            { 
                                await _messages.Writer.WriteAsync(message, ct);
                            }
                            else
                            {
                                _logger?.LogWarning("JSON deserialisation returned null");
                            }

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
            _messages.Writer.Complete(exception);
        }
    }
}