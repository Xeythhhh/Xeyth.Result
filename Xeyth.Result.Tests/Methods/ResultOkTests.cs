namespace Xeyth.Result.Tests.Methods;

public class ResultOkTests : SnapshotTestBase
{
    [Fact]
    public Task ShouldCreateNonGenericSuccessResult() =>
        Verify(Result.Ok(), Settings);

    [Fact]
    public Task ShouldCreateGenericSuccessResultWithValue() =>
        Verify(Result.Ok(420), Settings);
}
