using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Channels;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;

namespace Sarsoo.Terraform.Command;

public class TerraformStreamCommand<T>  where T: notnull
{
    private readonly ILogger<TerraformStreamCommand<T>>? _logger;
    
    private readonly JsonTypeInfo<T> _serialiserInfo;
    private readonly Channel<T> Messages = Channel.CreateUnbounded<T>();
    private CliWrap.Command Command { get; set; }

    public ChannelReader<T> Output => Messages.Reader;

    public TerraformStreamCommand(string executable, JsonTypeInfo<T> serialiserInfo, ILogger<TerraformStreamCommand<T>>? logger = null)
    {
        _serialiserInfo = serialiserInfo;
        _logger = logger;
        Command = Cli.Wrap(executable)
            .WithValidation(CommandResultValidation.None);
    }

    public TerraformStreamCommand<T> Configure(Func<CliWrap.Command, CliWrap.Command> configure)
    {
        Command = configure.Invoke(Command);

        return this;
    }

    public async Task Run()
    {
        await foreach (var cmdEvent in Command.ListenAsync())
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

                        await Messages.Writer.WriteAsync(message);

                        break;
                    case StandardErrorCommandEvent stdErr:
                        _logger?.LogError(stdErr.Text);
                        break;
                    case ExitedCommandEvent exited:
                        _logger?.LogInformation("Process exited; Code: {ExitCode}", exited.ExitCode);
                        Messages.Writer.Complete();
                        break;
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Exception occured while running Terraform command");
                Messages.Writer.Complete(e);
            }
        }
    }
}