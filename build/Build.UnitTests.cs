using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target UnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Log.Information("UnitTests Started...");
        });
}
