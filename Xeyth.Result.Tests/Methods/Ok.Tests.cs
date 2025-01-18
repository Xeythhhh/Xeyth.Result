namespace Xeyth.Result.Tests.Methods;

public class Ok : SnapshotTestBase
{
    [Fact]
    public Task ShouldReturnSuccess() =>
        Verify(Result.Ok(), Settings);

    [Fact]
    public Task ShouldReturnSuccessWithValue() =>
        Verify(Result.Ok(420), Settings);
}
