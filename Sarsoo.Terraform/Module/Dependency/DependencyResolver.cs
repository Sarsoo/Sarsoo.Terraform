using Sarsoo.Terraform.Source.FileSystem;
using TerraformDotnet.Hcl.Nodes;
using TerraformDotnet.Module;

namespace Sarsoo.Terraform.Module.Dependency;

public class DependencyResolver
{
    private readonly bool _localModuleAbsolutePaths;

    public record struct ChildModuleSource(string Path, TerraformModuleCall Source);
    public record struct ReverseModuleSource(string Path, TerraformModule Source);

    /// <summary>
    /// Map from module to it's dependencies
    /// </summary>
    public Dictionary<string, List<ChildModuleSource>> ForwardDependencies { get; } = new();

    /// <summary>
    /// Map from a module source to it's dependents
    /// </summary>
    public Dictionary<string, List<ReverseModuleSource>> ReverseDependencies { get; } = new();

    public DependencyResolver(bool localModuleAbsolutePaths = true)
    {
        _localModuleAbsolutePaths = localModuleAbsolutePaths;
    }

    public void Resolve(string root)
    {
        foreach (var r in SourceResolver.FindSourceDirectories(root))
        {
            if (!ForwardDependencies.TryGetValue(r, out var dependencies))
            {
                dependencies = new();
                ForwardDependencies.Add(r, dependencies);
            }

            var source = TerraformModule.LoadFromDirectory(r);
            foreach (var cm in source.ModuleCalls)
            {
                // source value, path the child module was found
                var childModuleInvocationReference = new ChildModuleSource(r, cm);

                if (cm.Source is HclLiteralExpression { Value: not null } le)
                {
                    childModuleInvocationReference.Path = _localModuleAbsolutePaths ? childModuleInvocationReference.Source.SourceReference().AbsoluteModulePath(r) : le.Value;

                    if (!ReverseDependencies.TryGetValue(childModuleInvocationReference.Path, out var dependents))
                    {
                        dependents = new();
                        ReverseDependencies.Add(childModuleInvocationReference.Path, dependents);
                    }

                    dependencies.Add(childModuleInvocationReference);
                    dependents.Add(new ReverseModuleSource(r, source));
                }
                else
                {
                    throw new InvalidOperationException("Null module source found");
                }

            }
        }
    }
}