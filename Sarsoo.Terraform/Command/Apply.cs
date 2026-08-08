using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.MachineReadableUI.Json;

namespace Sarsoo.Terraform.Command;

public class Apply
{
    private readonly TerraformStreamCommand<FullMessage> _command;

    public ChannelReader<FullMessage> Output => _command.Output;

    private string? _planFilePath = null;

    public Apply(string executable, string workingDirectory, ILogger<TerraformStreamCommand<FullMessage>>? logger = null)
    {
        _command = new TerraformStreamCommand<FullMessage>(executable, MruiContext.Default.FullMessage, logger)
            .Configure(x => x.WithWorkingDirectory(workingDirectory));
    }

    public Apply WithPlanFile(string path)
    {
        _planFilePath = path;
        return this;
    }

    public Task Run() => _command
        .Configure(x =>
            x.WithArguments(b =>
            {
                b.Add("apply").Add("-json");

                if (!string.IsNullOrEmpty(_planFilePath))
                {
                    b.Add(_planFilePath);
                }
            })).Run();
}