using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Log.Information("Pack Started...");
        });
}
