using Nuke.Common;

using Serilog;

namespace _build;

internal partial class Build
{
    Target Tests => _ => _
        .DependsOn(UnitTests)
        .DependsOn(IntegrationTests)
        .Executes(() =>
        {
            Log.Information("Tests Started...");
        });
}
