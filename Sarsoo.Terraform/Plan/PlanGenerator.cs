using System.Globalization;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using Sarsoo.Terraform.Command;
using Sarsoo.Terraform.JsonOutput.Plan;
using Sarsoo.Terraform.MachineReadableUI;
using Sarsoo.Terraform.Observability;

namespace Sarsoo.Terraform.Plan;

public class PlanGenerator: ITerraformCommandStreaming, ITerraformCommandSingle
{
    private readonly ILogger<PlanGenerator>? _logger;
    private Sarsoo.Terraform.Command.Plan _generate;
    private ShowPlan _parse;
    
    public bool Errored => _generate.Errored;
    public int ExitCode { get; }

    public ChannelReader<TerraformMessage>? Output => _generate.Output;
    public ChannelReader<string>? JsonOutput =>  _generate.JsonOutput;
    Task ITerraformCommandStreaming.Run(CancellationToken ct) => Run(ct);
    
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
        using var trace = Tracing.Source.StartActivity("PlanGenerator::Run");
        Baggage.SetBaggage(ObservabilityConstants.BinaryPlanPath, _filePath);
        _logger?.LogInformation("Generating plan...");
        _generate.WithOutputFile(_filePath);
        await _generate.Run(ct).ConfigureAwait(false);
        trace?.AddEvent(new("Binary Plan Generated"));

        _logger?.LogInformation("Generating json from plan bin...");
        _parse.WithPlanFile(_filePath);
        var json = await _parse.Run(ct).ConfigureAwait(false);
        trace?.AddEvent(new("JSON Plan Generated"));
        
        return json;
    }
}