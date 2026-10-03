using Sarsoo.Terraform.MachineReadableUI.Json;
using Sarsoo.Terraform.MachineReadableUI.Validate;

namespace Sarsoo.Terraform.Command;

public class Validate: ITerraformCommandSingle<ValidationOutput>
{
    private readonly TerraformCommand _command;

    public Validate(string workingDirectory, string? tfExecutable = null)
    {
        _command = new TerraformCommand(tfExecutable)
            .Configure(x =>
                x.WithArguments(["validate", "-json"])
                    .WithWorkingDirectory(workingDirectory));
    }

    public Task<string> Run(CancellationToken ct = default) => _command.Run(ct);
    public Task<ValidationOutput?> RunParsed(CancellationToken ct = default) => _command.Run<ValidationOutput>(MruiContext.Default.ValidationOutput, ct);
    
    public bool Errored => _command.Errored;
    public int ExitCode =>  _command.ExitCode;
}