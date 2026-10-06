using System.Threading.Channels;
using Sarsoo.Terraform.MachineReadableUI;

namespace Sarsoo.Terraform.Command;

public interface ITerraformCommand
{
    bool Errored { get; }
    int ExitCode { get; }
}

public interface ITerraformCommandStreaming : ITerraformCommand
{
    bool ErrorLogFound { get; }
    ChannelReader<TerraformMessage>? Output { get; }
    ChannelReader<string>? JsonOutput { get; }
    Task Run(CancellationToken ct);
}

public interface ITerraformCommandSingle : ITerraformCommand
{
    Task<string> Run(CancellationToken ct);
}

public interface ITerraformCommandSingle<T> : ITerraformCommandSingle
{
    Task<T?> RunParsed(CancellationToken ct);
}