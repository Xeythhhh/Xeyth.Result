using Nuke.Common.CI.GitHubActions;

namespace _build;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    //On = [GitHubActionsTrigger.Push],
    OnPushBranches = ["develop"],
    InvokedTargets = [nameof(Tests)])]

internal partial class Build
{
    GitHubActions GitHubActions => GitHubActions.Instance;
}
