using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            Log.Information("Clean Started...");
        });
}
