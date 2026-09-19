using System.Text.RegularExpressions;
using TerraformDotnet.Hcl.Nodes;
using TerraformDotnet.Module;

namespace Sarsoo.Terraform.Module;

public static partial class VersionRegex
{
    /// <summary>
    /// ? or & followed by ref=<VERSION> where VERSION ends at the end of the string or a '&'
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"(\?|&)ref=(?<version>[^&]*)(&|$)", RegexOptions.IgnoreCase)]
    public static partial Regex GitVersion();
}

public static class ModuleExtensions
{
    private static string? GitVersion(string source)
    {
        var matches = VersionRegex.GitVersion().Matches(source);
        if (matches.Any())
        {
            return matches.First().Groups["version"].Value;
        }

        return null;
    }

    public static ModuleReference SourceReference(this TerraformModuleCall mod)
    {
        ModuleReference reference = default;

        if (mod.Source is HclLiteralExpression { Value: not null } le)
        {
            reference.Source = le.Value;

            reference.SourceType = le.Value switch
            {
                {} when le.Value.StartsWith("./")
                    || le.Value.StartsWith("../")
                    || le.Value.StartsWith("/") => ModuleSourceType.Local,
                {} when le.Value.StartsWith("bitbucket", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.BitBucket,
                {} when le.Value.StartsWith("github", StringComparison.OrdinalIgnoreCase)
                    || le.Value.StartsWith("git:github", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.GitHub,
                {} when le.Value.StartsWith("git::", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.Git,
                {} when le.Value.StartsWith("hg::", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.Mercurial,
                {} when le.Value.StartsWith("s3::", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.S3,
                {} when le.Value.StartsWith("gcs::", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.GCS,
                {} when le.Value.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                        || le.Value.StartsWith("https://", StringComparison.OrdinalIgnoreCase) => ModuleSourceType.HTTP,
                _ => ModuleSourceType.Registry
            };
        }

        reference.Version = (reference.SourceType, mod.Version) switch
        {
            (ModuleSourceType.Local, _) => null,
            (ModuleSourceType.Registry, HclLiteralExpression { Value: not null } vle) => vle.Value,
            (ModuleSourceType.Git or ModuleSourceType.GitHub or ModuleSourceType.BitBucket, _) => GitVersion(reference.Source),
            ({IsVersionControl: true}, _) => "",
            _ => null
        };

        return reference;
    }

    extension(ModuleReference reference)
    {
        public string AbsoluteModulePath(string rootLocation) => reference switch
        {
             ({SourceType: ModuleSourceType.Local}) => Path.GetFullPath(reference.Source, rootLocation),
             _ => reference.Source
        };
    }

    extension(ModuleSourceType sourceType)
    {
        public bool IsVersionControl => sourceType switch
        {
            (ModuleSourceType.BitBucket
                or ModuleSourceType.Git
                or ModuleSourceType.GitHub
                or ModuleSourceType.Mercurial) => true,
            _ => false
        };
    }
}