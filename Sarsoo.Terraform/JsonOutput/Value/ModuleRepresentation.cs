#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Sarsoo.Terraform.JsonOutput.Value;

public class ModuleRepresentation
{
    public string? Address { get; set; }
    public List<ResourceRepresentation> Resources { get; set; }
    public List<ModuleRepresentation>? ChildModules { get; set; }
}