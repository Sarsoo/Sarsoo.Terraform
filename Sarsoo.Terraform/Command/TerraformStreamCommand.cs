using System.Diagnostics;
using System.Text.Json;
using System.Threading.Channels;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;
using Sarsoo.Terraform.Observability;

namespace Sarsoo.Terraform.Command;

public class TerraformStreamCommand
{
    private readonly OutputFormat _outputFormat;
    private readonly ILogger<TerraformStreamCommand>? _logger;

    private readonly Channel<TerraformMessage>? _messages;
    private readonly Channel<string>? _jsonMessages;
    private CliWrap.Command Command { get; set; }

    private int? _exitCode = null;
    public int ExitCode => _exitCode ?? throw new InvalidOperationException("Command has not finished");
    public bool Errored => ((_exitCode ?? 0) > 0) || _errorLogFound;
    private bool _errorLogFound = false;

    public ChannelReader<TerraformMessage>? MessageOutput => _messages?.Reader;
    public ChannelReader<string>? JsonOutput => _jsonMessages?.Reader;

    public TerraformStreamCommand(string executable, OutputFormat outputFormat = OutputFormat.Parsed, ILogger<TerraformStreamCommand>? logger = null)
    {
        _outputFormat = outputFormat;

        if (outputFormat.HasFlag(OutputFormat.Parsed))
        {
            _messages = Channel.CreateUnbounded<TerraformMessage>();
        }
        if (outputFormat.HasFlag(OutputFormat.Json))
        {
            _jsonMessages = Channel.CreateUnbounded<string>();
        }
        
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
        using var trace = Tracing.Source.StartActivity("TerraformStreamCommand::Run");
        Exception? exception = null;
        try
        {
            await foreach (var cmdEvent in Command.ListenAsync(cancellationToken: ct).ConfigureAwait(false))
            {
                try
                {
                    switch (cmdEvent)
                    {
                        case StartedCommandEvent started:
                            trace?.AddEvent(new("Process Started"));
                            _logger?.LogInformation("Process started; ID: {ProcessId}", started.ProcessId);
                            break;
                        case StandardOutputCommandEvent stdOut:

                            _jsonMessages?.Writer.TryWrite(stdOut.Text);

                            if (_messages is not null)
                            {
                                var message = JsonSerializer.Deserialize(stdOut.Text, MruiContext.Default.TerraformMessage);
                            
                                if (message is not null)
                                {
                                    if (message.Level.Equals("error", StringComparison.OrdinalIgnoreCase))
                                    {
                                        _errorLogFound = true;
                                    }
                                    _messages?.Writer.TryWrite(message);
                                }
                                else
                                {
                                    _logger?.LogWarning("JSON deserialisation returned null");
                                }
                            }
                            
                            break;
                        case StandardErrorCommandEvent stdErr:
                            _logger?.LogError(stdErr.Text);
                            break;
                        case ExitedCommandEvent exited:
                            trace?.AddEvent(new("Process Ended"));
                            _logger?.LogInformation("Process exited; Code: {ExitCode}", exited.ExitCode);
                            _exitCode = exited.ExitCode; 
                            trace?.AddTag(ObservabilityConstants.ExitCode, _exitCode);
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
            trace?.SetStatus(Errored ? ActivityStatusCode.Error : ActivityStatusCode.Ok);
        
            _messages?.Writer.Complete(exception);
            _jsonMessages?.Writer.Complete(exception);
        }
    }
}