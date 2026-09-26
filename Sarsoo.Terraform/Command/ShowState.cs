using Sarsoo.Terraform.JsonOutput;
using Sarsoo.Terraform.JsonOutput.State;
using JsonOutputContext = Sarsoo.Terraform.JsonOutput.JsonOutputContext;

namespace Sarsoo.Terraform.Command;

public class ShowState: ITerraformCommandSingle<StateRepresentation>
{
    private readonly TerraformCommand _command;

    public ShowState(string executable, string workingDirectory)
    {
        _command = new TerraformCommand(executable)
            .Configure(x =>
                x.WithArguments(["show", "-json"])
                    .WithWorkingDirectory(workingDirectory));
    }

    public Task<string> Run(CancellationToken ct = default) => _command.Run(ct);
    public Task<StateRepresentation?> RunParsed(CancellationToken ct = default) => _command.Run<StateRepresentation>(JsonOutputContext.Default.StateRepresentation, ct);
    
    public bool Errored => _command.Errored;
    public int ExitCode =>  _command.ExitCode;
}