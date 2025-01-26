using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target UpdateChangelog => _ => _
        .Before(Pack)
        .Executes(() =>
        {
            Log.Information("UpdateChangelog Started...");
        });
}
