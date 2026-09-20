using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Command;

public class Apply
{
    private readonly TerraformStreamCommand _command;

    public ChannelReader<TerraformMessage> Output => _command.Output;

    private string? _planFilePath = null;

    public Apply(string executable, string workingDirectory, ILogger<TerraformStreamCommand>? logger = null)
    {
        _command = new TerraformStreamCommand(executable, logger)
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