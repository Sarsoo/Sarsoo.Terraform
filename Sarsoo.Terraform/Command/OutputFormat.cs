namespace Sarsoo.Terraform.Command;

[Flags]
public enum OutputFormat
{
    None = 0,
    Parsed = 1 << 0,
    Json =  1 << 1,
}