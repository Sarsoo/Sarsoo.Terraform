using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.Command;

public class Plan: ITerraformCommandStreaming
{
    private readonly TerraformStreamCommand _command;

    public ChannelReader<TerraformMessage>? Output => _command.MessageOutput;
    public ChannelReader<string>? JsonOutput => _command.JsonOutput;

    private string? _outputPath = null;
    
    public bool Errored => _command.Errored;
    public int ExitCode =>  _command.ExitCode;

    public Plan(string workingDirectory, OutputFormat outputFormat = OutputFormat.Parsed, string? tfExecutable = null, ILogger<TerraformStreamCommand>? logger = null)
    {
        _command = new TerraformStreamCommand(tfExecutable, outputFormat, logger: logger)
            .Configure(x =>
                x.WithWorkingDirectory(workingDirectory));
    }

    public Plan WithOutputFile(string path)
    {
        _outputPath = path;
        return this;
    }

    public Task Run(CancellationToken ct = default) => _command
        .Configure(x =>
            x.WithArguments(b =>
            {
                b.Add("plan").Add("-json").Add("-detailed-exitcode");

                if (_outputPath is not null)
                {
                    b.Add("-out").Add(_outputPath);
                }
            })).Run(ct);
}