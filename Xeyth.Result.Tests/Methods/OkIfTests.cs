using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfTests : SnapshotTestBase
{
    [Fact]
    public Task ShouldCreateSuccessResultWhenPredicateIsTrue() =>
        Verify(Result.OkIf(() => true, "Test error message"), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWhenPredicateIsFalseWithErrorMessage() =>
        Verify(Result.OkIf(() => false, "Test error message"), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWhenPredicateIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(() => false, () => Error.DefaultFactory("Test error")), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWhenPredicateIsFalseWithCustomError() =>
        Verify(Result.OkIf(() => false, new Error("Test error")), Settings);

    [Fact]
    public Task ShouldCreateSuccessResultWhenPredicateIsTrueWithCustomErrorFactory() =>
        Verify(Result.OkIf(() => true, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWithCustomErrorFactoryWhenPredicateIsFalse() =>
        Verify(Result.OkIf(() => false, () => new Error("Test error")), Settings);

    [Fact]
    public Task ShouldCreateFailureResultWhenPredicateIsFalseWithErrorMessageFactory() =>
        Verify(Result.OkIf(() => false, () => "Test error message"), Settings);

    [Fact]
    public void ShouldThrowIfPredicateIsNull() =>
        Throws(() => Result.OkIf(null!, "Error"), Settings);
}