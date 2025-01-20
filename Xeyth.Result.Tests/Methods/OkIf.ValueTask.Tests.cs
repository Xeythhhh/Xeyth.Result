using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfValueTask : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenValueTaskPredicateIsTrue() =>
        Verify(Result.OkIf(ValueTask.FromResult(true), "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenValueTaskPredicateIsFalse() =>
        Verify(Result.OkIf(ValueTask.FromResult(false), "Error"), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrue_AndValueTaskErrorMessage() =>
        Verify(Result.OkIf(true, ValueTask.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalse_AndValueTaskErrorMessage() =>
        Verify(Result.OkIf(false, ValueTask.FromResult("Error")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenInvokedWithValueTaskError() =>
        Verify(Result.OkIf(ValueTask.FromResult(true), ValueTask.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenInvokedWithValueTaskError() =>
        Verify(Result.OkIf(ValueTask.FromResult(false), ValueTask.FromResult(Error.DefaultFactory("Task error"))), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenValueTaskPredicateIsTrueWithGenericError() =>
        Verify(Result.OkIf(ValueTask.FromResult(true), new Error("Task error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenValueTaskPredicateIsFalseWithGenericError() =>
        Verify(Result.OkIf(ValueTask.FromResult(false), new Error("Task error")), Settings);
}
