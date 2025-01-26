using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target IntegrationTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Log.Information("IntegrationTests Started...");

        });
}
