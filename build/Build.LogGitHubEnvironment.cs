using Nuke.Common;

using Serilog;

namespace _build;
internal partial class Build
{
    Target LogGitHubEnvironment => _ => _
        .Executes(() =>
        {
            Log.Information("Branch = {Branch}", GitHubActions.Ref);
            Log.Information("Commit = {Commit}", GitHubActions.Sha);
        });
}
