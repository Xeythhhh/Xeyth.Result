using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            Log.Information("Compile Started...");
        });
}
