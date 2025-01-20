using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfGeneric : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrue() =>
        Verify(Result.OkIf(420, true, "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalse() =>
        Verify(Result.OkIf(420, false, "Error"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrue() =>
        Verify(Result.OkIf(420, () => true, "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalse() =>
        Verify(Result.OkIf(420, () => false, "Error"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithErrorFactory() =>
        Verify(Result.OkIf(420, true, () => new Error("Custom error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(420, false, () => new Error("Custom error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithGenericError() =>
        Verify(Result.OkIf(420, true, new Error("Generic error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithGenericError() =>
        Verify(Result.OkIf(420, false, new Error("Generic error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrueWithErrorFactory() =>
        Verify(Result.OkIf(420, () => true, () => new Error("Predicate error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(420, () => false, () => new Error("Predicate error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenPredicateIsTrueWithGenericError() =>
        Verify(Result.OkIf(420, () => true, new Error("Generic error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenPredicateIsFalseWithGenericError() =>
        Verify(Result.OkIf(420, () => false, new Error("Generic error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);
}
