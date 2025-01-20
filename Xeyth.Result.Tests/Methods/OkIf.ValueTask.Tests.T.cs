using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class OkIfValueTaskGeneric : TestBase
{
    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrue() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalse() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), "Error"), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithValueTaskErrorMessage() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), new ValueTask<string>("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithValueTaskErrorMessage() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), new ValueTask<string>("Error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithError() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), new Error("Custom error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithError() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), new Error("Custom error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithValueTaskErrorFactory() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), new ValueTask<Error>(new Error("Custom error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithValueTaskErrorFactory() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), new ValueTask<Error>(new Error("Custom error"))), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithErrorFactory() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), new Error("Generic error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithErrorFactory() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), new Error("Generic error")), Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsSuccessIsTrueWithValueTaskError() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(true), new ValueTask<Error>(new Error("Predicate error"))), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsSuccessIsFalseWithValueTaskError() =>
        Verify(Result.OkIf(420, new ValueTask<bool>(false), new ValueTask<Error>(new Error("Predicate error"))), Settings)
            .ScrubMember<Result<int>>(r => r.Value);
}
