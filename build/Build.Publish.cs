using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Publish => _ => _
        .DependsOn(Tests)
        .DependsOn(UpdateChangelog)
        .DependsOn(Pack)
        .Executes(() =>
        {
            Log.Information("Publish Started...");
        });
}
