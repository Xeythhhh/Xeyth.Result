using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIf : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrue() =>
        Verify(Result.OkIf(() => true, "Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithErrorMessage() =>
        Verify(Result.OkIf(() => false, "Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(() => false, () => Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithError() =>
        Verify(Result.OkIf(() => false, new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrueWithCustomErrorFactory() =>
        Verify(Result.OkIf(() => true, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithCustomErrorFactory() =>
        Verify(Result.OkIf(() => false, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithErrorMessageFactory() =>
        Verify(Result.OkIf(() => false, () => "Test error message"), Settings);

    [Fact]
    public void ShouldThrow_WhenPredicateIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.OkIf((Func<bool>)null!, "Error"));
}
