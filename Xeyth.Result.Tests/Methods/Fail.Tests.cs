using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class Fail : SnapshotTestBase
{
    [Fact]
    public Task ShouldReturnFailureWhenErrorMessageIsProvided() =>
        Verify(Result.Fail("Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenErrorIsProvided() =>
        Verify(Result.Fail(Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenErrorMessagesAreProvided() =>
        Verify(Result.Fail(["Error 1", "Error 2"]), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenErrorsAreProvided() =>
        Verify(Result.Fail(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings);

    [Fact]
    public Task ShouldReturnGenericFailureWhenErrorMessageIsProvided() =>
        Verify(Result.Fail<int>("Test error message"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailureWhenErrorIsProvided() =>
        Verify(Result.Fail<int>(Error.DefaultFactory("Test error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailureWhenErrorMessagesAreProvided() =>
        Verify(Result.Fail<int>(["Error 1", "Error 2"]), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailureWhenErrorsAreProvided() =>
        Verify(Result.Fail<int>(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings)
            .ScrubMember<Result<int>>(r => r.Value);
}
