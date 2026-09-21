using Sarsoo.Terraform.MachineReadableUI.Json;
using Sarsoo.Terraform.MachineReadableUI.Validate;

namespace Sarsoo.Terraform.Command;

public class Validate
{
    private readonly TerraformCommand _command;

    public Validate(string executable, string workingDirectory)
    {
        _command = new TerraformCommand(executable)
            .Configure(x =>
                x.WithArguments(["validate", "-json"])
                    .WithWorkingDirectory(workingDirectory));
    }

    public Task<string> Run(CancellationToken ct = default) => _command.Run(ct);
    public Task<ValidationOutput?> RunParsed(CancellationToken ct = default) => _command.Run<ValidationOutput>(MruiContext.Default.ValidationOutput, ct);
}