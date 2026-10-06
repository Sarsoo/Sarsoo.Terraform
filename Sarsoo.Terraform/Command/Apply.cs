using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Command;

public class Apply: ITerraformCommandStreaming
{
    private readonly TerraformStreamCommand _command;

    public ChannelReader<TerraformMessage>? Output => _command.MessageOutput;
    public ChannelReader<string>? JsonOutput => _command.JsonOutput;

    private string? _planFilePath = null;
    
    public bool Errored => _command.Errored;
    public int ExitCode =>  _command.ExitCode;
    public bool ErrorLogFound => _command.ErrorLogFound;

    public Apply(string workingDirectory, OutputFormat outputFormat = OutputFormat.Parsed, string? tfExecutable = null, ILogger<TerraformStreamCommand>? logger = null)
    {
        _command = new TerraformStreamCommand(tfExecutable, outputFormat, logger: logger)
            .Configure(x => x.WithWorkingDirectory(workingDirectory));
    }

    public Apply WithPlanFile(string path)
    {
        _planFilePath = path;
        return this;
    }

    public Task Run(CancellationToken ct = default) => _command
        .Configure(x =>
            x.WithArguments(b =>
            {
                b.Add("apply").Add("-json");

                if (!string.IsNullOrEmpty(_planFilePath))
                {
                    b.Add(_planFilePath);
                }
            })).Run(ct);
}