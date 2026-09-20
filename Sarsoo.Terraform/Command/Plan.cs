using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.Command;

public class Plan
{
    private readonly TerraformStreamCommand _command;

    public ChannelReader<TerraformMessage> Output => _command.Output;

    private string? _outputPath = null;

    public Plan(string executable, string workingDirectory, ILogger<TerraformStreamCommand>? logger = null)
    {
        _command = new TerraformStreamCommand(executable, logger)
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