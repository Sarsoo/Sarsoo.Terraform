using System.Diagnostics;

namespace Sarsoo.Terraform.Observability;

public static class ObservabilityConstants
{
    public const string ExitCode = "cmd.exit_code";
    public const string WorkingDirectory = "cmd.working_directory";
    public const string BinaryPlanPath = "plan.bin.path";
}