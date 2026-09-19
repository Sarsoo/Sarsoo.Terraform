using Sarsoo.Terraform.JsonOutput;
using Sarsoo.Terraform.JsonOutput.State;
using JsonOutputContext = Sarsoo.Terraform.JsonOutput.JsonOutputContext;

namespace Sarsoo.Terraform.Command;

public class ShowState
{
    private readonly TerraformCommand<StateRepresentation> _command;

    public ShowState(string executable, string workingDirectory)
    {
        _command = new TerraformCommand<StateRepresentation>(executable, JsonOutputContext.Default.StateRepresentation)
            .Configure(x =>
                x.WithArguments(["show", "-json"])
                    .WithWorkingDirectory(workingDirectory));
    }

    public Task<StateRepresentation?> Run(CancellationToken ct = default) => _command.Run(ct);
}