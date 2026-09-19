using Sarsoo.Terraform.Exceptions;
using Sarsoo.Terraform.JsonOutput;
using Sarsoo.Terraform.JsonOutput.Plan;
using JsonOutputContext = Sarsoo.Terraform.JsonOutput.JsonOutputContext;

namespace Sarsoo.Terraform.Command;

public class ShowPlan
{
    private readonly TerraformCommand<PlanRepresentation> _command;

    private string? filePath = null;

    public ShowPlan(string executable, string workingDirectory)
    {
        _command = new TerraformCommand<PlanRepresentation>(executable, JsonOutputContext.Default.PlanRepresentation)
            .Configure(x => x.WithWorkingDirectory(workingDirectory));
    }

    public ShowPlan WithPlanFile(string path)
    {
        filePath = path;
        return this;
    }

    public Task<PlanRepresentation?> Run(CancellationToken ct = default) {

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new FilePathMissingException();
        }

        return _command.Configure(x => x.WithArguments(["show", "-json", filePath]))
            .Run(ct);
    }
}