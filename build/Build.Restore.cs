using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Restore => _ => _
        .Executes(() =>
        {
            Log.Information("Restore Started...");
            //DotNetRestore();
        });
}
