using System.Text.Json.Serialization;
using Sarsoo.Terraform.MachineReadableUI.Apply;
using Sarsoo.Terraform.MachineReadableUI.Drift;
using Sarsoo.Terraform.MachineReadableUI.Validate;

namespace Sarsoo.Terraform.MachineReadableUI.Json;

[JsonSourceGenerationOptions(
    PropertyNameCaseInsensitive = true, 
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower, 
    AllowOutOfOrderMetadataProperties = true
)]
[JsonSerializable(typeof(BaseMessage))]
[JsonSerializable(typeof(FullMessage))]

[JsonSerializable(typeof(Resource))]
[JsonSerializable(typeof(ResourceDriftChange))]
[JsonSerializable(typeof(ResourceAction))]

[JsonSerializable(typeof(ChangeSummary))]
[JsonSerializable(typeof(ChangeOperation))]
[JsonSerializable(typeof(ChangeReason))]

[JsonSerializable(typeof(Hook))]

[JsonSerializable(typeof(ValidationOutput))]
[JsonSerializable(typeof(ValidationDiagnostic))]
[JsonSerializable(typeof(ValidationDiagnosticRange))]
[JsonSerializable(typeof(ValidationOutput))]
[JsonSerializable(typeof(ValidationSnippet))]
[JsonSerializable(typeof(ValidationValueContext))]
[JsonSerializable(typeof(SourcePosition))]
public partial class MruiContext : JsonSerializerContext
{ }