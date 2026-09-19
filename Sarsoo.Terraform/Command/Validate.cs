using Sarsoo.Terraform.MachineReadableUI.Json;
using Sarsoo.Terraform.MachineReadableUI.Validate;

namespace Sarsoo.Terraform.Command;

public class Validate
{
    private readonly TerraformCommand<ValidationOutput> _command;

    public Validate(string executable, string workingDirectory)
    {
        _command = new TerraformCommand<ValidationOutput>(executable, MruiContext.Default.ValidationOutput)
            .Configure(x =>
                x.WithArguments(["validate", "-json"])
                    .WithWorkingDirectory(workingDirectory));
    }

    public Task<ValidationOutput?> Run(CancellationToken ct = default) => _command.Run(ct);
}