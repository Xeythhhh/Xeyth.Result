using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfAsyncGeneric : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrue() =>
        Verify(Result.OkIf(420, Task.FromResult(true), "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalse() =>
        Verify(Result.OkIf(420, Task.FromResult(false), "Error"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithAsyncErrorMessage() =>
        Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithAsyncErrorMessage() =>
        Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult("Error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithError() =>
        Verify(Result.OkIf(420, Task.FromResult(true), new Error("Custom error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithError() =>
        Verify(Result.OkIf(420, Task.FromResult(false), new Error("Custom error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithAsyncErrorFactory() =>
        Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult(new Error("Custom error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithAsyncErrorFactory() =>
        Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult(new Error("Custom error"))), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithErrorFactory() =>
        Verify(Result.OkIf(420, Task.FromResult(true), new Error("Generic error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(420, Task.FromResult(false), new Error("Generic error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithAsyncError() =>
        Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult(new Error("Predicate error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithAsyncError() =>
        Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult(new Error("Predicate error"))), Settings)
            .ScrubMember<Result<int>>(r => r.Value);
}
