using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfAsync : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenAsyncPredicateIsTrue() =>
        Verify(Result.OkIf(Task.FromResult(true), "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenAsyncPredicateIsFalse() =>
        Verify(Result.OkIf(Task.FromResult(false), "Error"), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrueWithAsyncErrorMessage() =>
        Verify(Result.OkIf(true, Task.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithAsyncErrorMessage() =>
        Verify(Result.OkIf(false, Task.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenInvokedWithAsyncError() =>
        Verify(Result.OkIf(Task.FromResult(true), Task.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithAsyncError() =>
        Verify(Result.OkIf(Task.FromResult(false), Task.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenAsyncPredicateIsTrueWithGenericError() =>
        Verify(Result.OkIf(Task.FromResult(true), new Error("Task error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenAsyncPredicateIsFalseWithGenericError() =>
        Verify(Result.OkIf(Task.FromResult(false), new Error("Task error")), Settings);
}
