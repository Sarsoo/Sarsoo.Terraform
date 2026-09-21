using System.Globalization;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Sarsoo.Terraform.Command;
using Sarsoo.Terraform.JsonOutput.Plan;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Plan;

public class PlanGenerator
{
    private readonly ILogger<PlanGenerator>? _logger;
    private Sarsoo.Terraform.Command.Plan _generate;
    private ShowPlan _parse;

    public ChannelReader<TerraformMessage>? PlanOutput => _generate.Output;
    public ChannelReader<string>? PlanJsonOutput => _generate.JsonOutput;
    
    private string _filePath;

    public PlanGenerator(string executable, string workingDirectory, string? planFileName = null, OutputFormat outputFormat = OutputFormat.Parsed, ILogger<PlanGenerator>? logger = null, ILogger<TerraformStreamCommand>? subLogger = null)
    {
        _logger = logger;
        _generate = new Sarsoo.Terraform.Command.Plan(executable, workingDirectory, outputFormat, logger: subLogger);
        _parse = new ShowPlan(executable, workingDirectory);
        
        _filePath = planFileName ?? $"plan-{DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmssfffzzz", DateTimeFormatInfo.InvariantInfo)}.tfplan";
    }

    public async Task<string> Run(CancellationToken ct = default)
    {
        _logger?.LogInformation("Generating plan...");
        _generate.WithOutputFile(_filePath);
        await _generate.Run(ct).ConfigureAwait(false);

        _logger?.LogInformation("Generating json from plan bin...");
        _parse.WithPlanFile(_filePath);
        return await _parse.Run(ct).ConfigureAwait(false);
    }
}