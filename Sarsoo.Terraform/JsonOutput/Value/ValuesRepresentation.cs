using Sarsoo.Terraform.JsonOutput;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Sarsoo.Terraform.JsonOutput.Value;

public class ValuesRepresentation
{
    public Dictionary<string, Variable> Outputs { get; set; }
    public ModuleRepresentation RootModule { get; set; }
}