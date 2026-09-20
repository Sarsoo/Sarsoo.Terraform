using System.Globalization;
using System.Threading.Channels;
using Sarsoo.Terraform.Command;
using Sarsoo.Terraform.JsonOutput.Plan;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Plan;

public class PlanGenerator
{
    private Sarsoo.Terraform.Command.Plan _generate;
    private ShowPlan _parse;

    public ChannelReader<TerraformMessage> PlanOutput => _generate.Output;

    public PlanGenerator(string executable, string workingDirectory)
    {
        _generate = new Sarsoo.Terraform.Command.Plan(executable, workingDirectory);
        _parse = new ShowPlan(executable, workingDirectory);
    }

    public async Task<PlanRepresentation?> Run()
    {
        var tempPath = $"plan-{DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmssfffzzz", DateTimeFormatInfo.InvariantInfo)}.tfplan";

        _generate.WithOutputFile(tempPath);
        await _generate.Run();

        _parse.WithPlanFile(tempPath);
        return await _parse.Run();
    }
}