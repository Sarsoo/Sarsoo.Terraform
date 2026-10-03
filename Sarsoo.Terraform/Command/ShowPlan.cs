using Sarsoo.Terraform.Exceptions;
using Sarsoo.Terraform.JsonOutput;
using Sarsoo.Terraform.JsonOutput.Plan;
using JsonOutputContext = Sarsoo.Terraform.JsonOutput.JsonOutputContext;

namespace Sarsoo.Terraform.Command;

public class ShowPlan: ITerraformCommandSingle<PlanRepresentation>
{
    private readonly TerraformCommand _command;

    private string? filePath = null;

    public ShowPlan(string workingDirectory, string? tfExecutable = null)
    {
        _command = new TerraformCommand(tfExecutable)
            .Configure(x => x.WithWorkingDirectory(workingDirectory));
    }

    public ShowPlan WithPlanFile(string path)
    {
        filePath = path;
        return this;
    }

    private TerraformCommand GetCommand()
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new FilePathMissingException();
        }

        return _command.Configure(x => x.WithArguments(["show", "-json", filePath]));
    }

    public Task<string> Run(CancellationToken ct = default) => GetCommand().Run(ct);
    public Task<PlanRepresentation?> RunParsed(CancellationToken ct = default)  => GetCommand().Run<PlanRepresentation>(JsonOutputContext.Default.PlanRepresentation, ct);
    
    public bool Errored => _command.Errored;
    public int ExitCode =>  _command.ExitCode;
}