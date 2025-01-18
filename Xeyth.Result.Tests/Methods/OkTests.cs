namespace Xeyth.Result.Tests.Methods;

public class OkTests : SnapshotTestBase
{
    [Fact]
    public Task ShouldCreateNonGenericSuccessResult() =>
        Verify(Result.Ok(), Settings);

    [Fact]
    public Task ShouldCreateGenericSuccessResultWithValue() =>
        Verify(Result.Ok(420), Settings);
}
