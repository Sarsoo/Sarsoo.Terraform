namespace Sarsoo.Terraform.IO;

public static class ExeResolution
{
    /// <summary>
    /// http://csharptest.net/526/how-to-search-the-environments-path-for-an-exe-or-dll/index.html
    /// </summary>
    /// <param name="exe"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string FindExePath(string exe)
    {
        exe = Environment.ExpandEnvironmentVariables(exe);
        if (!File.Exists(exe))
        {
            if (Path.GetDirectoryName(exe) == String.Empty)
            {
                foreach (string test in (Environment.GetEnvironmentVariable("PATH") ?? "").Split(';'))
                {
                    string path = test.Trim();
                    if (!String.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, exe)))
                        return Path.GetFullPath(path);
                }
            }
            throw new FileNotFoundException(new FileNotFoundException().Message, exe);
        }
        return Path.GetFullPath(exe);
    }

    public static string FindTerraformExePath(string? exeOverride)
    {
        if (!string.IsNullOrWhiteSpace(exeOverride) && File.Exists(exeOverride))
        {
            return Path.GetFullPath(exeOverride);
        }
        else
        {
            return FindExePath("terraform");
        }
    }
}