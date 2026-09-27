using System.Diagnostics;

namespace Sarsoo.Terraform.Observability;

public static class Tracing
{
    public static readonly ActivitySource Source = new("Sarsoo.Terraform");
}