namespace Sarsoo.Terraform.JsonOutput.Plan;

public struct ResourceAttributeReference
{
    public string Resource { get; set; }
    public List<string> Attribute { get; set; }
}