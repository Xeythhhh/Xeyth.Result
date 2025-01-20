using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class Fail : TestBase
{
    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithErrorMessage() =>
        Verify(Result.Fail("Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithError() =>
        Verify(Result.Fail(Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithErrorMessages() =>
        Verify(Result.Fail(["Error 1", "Error 2"]), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithErrors() =>
        Verify(Result.Fail(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings);

    [Fact]
    public Task ShouldReturnGenericFailure_WhenInvokedWithErrorMessage() =>
        Verify(Result.Fail<int>("Test error message"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailure_WhenInvokedWithError() =>
        Verify(Result.Fail<int>(Error.DefaultFactory("Test error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailure_WhenInvokedWithErrorMessages() =>
        Verify(Result.Fail<int>(["Error 1", "Error 2"]), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnGenericFailure_WhenInvokedWithErrors() =>
        Verify(Result.Fail<int>(
        [
            Error.DefaultFactory("Error 1"),
            Error.DefaultFactory("Error 2")
        ]), Settings)
            .ScrubMember<Result<int>>(r => r.Value);
}
