using System.Runtime.CompilerServices;

using Xeyth.Result.Exceptions;

namespace Xeyth.Result.Tests;

public class Configuration
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        UseSourceFileRelativeDirectory(".Snapshots");
        VerifierSettings.IgnoreMembersThatThrow<FailedResultValueAccessException>();
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.IgnoreStackTrace();
    }

    [Fact]
    public Task Run() => VerifyChecks.Run();
}