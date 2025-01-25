using System.Runtime.CompilerServices;

using Xeyth.Result.Exceptions;

namespace Xeyth.Result.Tests;

public sealed class Configuration
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        UseSourceFileRelativeDirectory(".Snapshots");
        VerifierSettings.IgnoreMembersThatThrow<FailedResultValueAccessException>();
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.AddExtraSettings(settings =>
        {
            settings.DefaultValueHandling = Argon.DefaultValueHandling.Include;
            settings.NullValueHandling = Argon.NullValueHandling.Include;
            settings.TypeNameHandling = Argon.TypeNameHandling.All;
        });
        VerifierSettings.IgnoreStackTrace();
    }

    [Fact]
    public Task Run() => VerifyChecks.Run();
}