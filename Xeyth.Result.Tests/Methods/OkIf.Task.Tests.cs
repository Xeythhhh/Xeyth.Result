using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfTask : SnapshotTestBase
{
    [Fact]
    public Task ShouldReturnSuccessWhenTaskPredicateIsTrue() =>
        Verify(Result.OkIf(Task.FromResult(true), "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenTaskPredicateIsFalse() =>
        Verify(Result.OkIf(Task.FromResult(false), "Error"), Settings);

    [Fact]
    public Task ShouldReturnSuccessWhenPredicateIsTrueAndTaskErrorMessage() =>
        Verify(Result.OkIf(true, Task.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenPredicateIsFalseAndTaskErrorMessage() =>
        Verify(Result.OkIf(false, Task.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnSuccessWithTaskError() =>
        Verify(Result.OkIf(Task.FromResult(true), Task.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnFailureWithTaskError() =>
        Verify(Result.OkIf(Task.FromResult(false), Task.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnSuccessWhenTaskPredicateIsTrueWithGenericError() =>
        Verify(Result.OkIf(Task.FromResult(true), new Error("Task error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenTaskPredicateIsFalseWithGenericError() =>
        Verify(Result.OkIf(Task.FromResult(false), new Error("Task error")), Settings);
}
