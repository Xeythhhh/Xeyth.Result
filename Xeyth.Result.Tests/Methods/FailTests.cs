using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class FailTests : SnapshotTestBase
{
    [Fact]
    public Task ShouldCreateFailureResultWithErrorMessage() =>
        Verify(Result.Fail("Test error message"), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWithError() =>
        Verify(Result.Fail(Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWithErrorMessages() =>
        Verify(Result.Fail(["Error 1", "Error 2"]), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWithErrors() =>
        Verify(Result.Fail(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings);

    [Fact]
    public Task ShouldCreateGenericFailureResultWithErrorMessage() =>
        Verify(Result.Fail<int>("Test error message"), Settings)
            .IgnoreMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldCreateGenericFailureResultWithError() =>
        Verify(Result.Fail<int>(Error.DefaultFactory("Test error")), Settings)
            .IgnoreMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldCreateGenericFailureResultWithErrorMessages() =>
        Verify(Result.Fail<int>(["Error 1", "Error 2"]), Settings)
            .IgnoreMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldCreateGenericFailureResultWithErrors() =>
        Verify(Result.Fail<int>(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings)
            .IgnoreMember<Result<int>>(r => r.Value);
}
