using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests;

public class ResultTests : SnapshotTestBase
{
    [Fact]
    public Task ShouldConvertSingleErrorToResult() =>
        Verify((Result)new Error("Test error"), Settings);

    [Fact]
    public Task ShouldConvertErrorListToResult() =>
        Verify((Result)new List<Error>()
                {
                    new("Error 1"),
                    new("Error 2")
                }, Settings);
}
