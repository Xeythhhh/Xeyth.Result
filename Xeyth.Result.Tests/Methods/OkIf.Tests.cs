using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIf : TestBase
{
    [Fact]
    public Task ShouldReturnSuccessWhenPredicateIsTrue() =>
        Verify(Result.OkIf(() => true, "Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenPredicateIsFalseWithErrorMessage() =>
        Verify(Result.OkIf(() => false, "Test error message"), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenPredicateIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(() => false, () => Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenPredicateIsFalseWithError() =>
        Verify(Result.OkIf(() => false, new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnSuccessWhenPredicateIsTrueWithCustomErrorFactory() =>
        Verify(Result.OkIf(() => true, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWithCustomErrorFactoryWhenPredicateIsFalse() =>
        Verify(Result.OkIf(() => false, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldReturnFailureWhenPredicateIsFalseWithErrorMessageFactory() =>
        Verify(Result.OkIf(() => false, () => "Test error message"), Settings);

    [Fact]
    public void ShouldThrowIfPredicateIsNull() =>
        Throws(() => Result.OkIf((Func<bool>)null!, "Error"), Settings);
}
