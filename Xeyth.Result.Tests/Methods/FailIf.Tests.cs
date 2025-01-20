using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class FailIf : TestBase
{
    [Fact]
    public Task ShouldReturnFailure_WhenIsFailureIsTrue() =>
        Verify(Result.FailIf(true, "Error"), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsFailureIsFalse() =>
        Verify(Result.FailIf(false, "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsFailureIsTrueWithStringErrorFactory() =>
        Verify(Result.FailIf(true, () => "Error"), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsFailureIsFalseWithStringErrorFactory() =>
        Verify(Result.FailIf(false, () => "Error"), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsFailureIsTrueWithIErrorErrorFactory() =>
        Verify(Result.FailIf(true, () => new Error("IError")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsFailureIsFalseWithIErrorErrorFactory() =>
        Verify(Result.FailIf(false, () => new Error("IError")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsFailureIsTrueWithError() =>
        Verify(Result.FailIf(true, Error.DefaultFactory("Error")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsFailureIsFalseWithError() =>
        Verify(Result.FailIf(false, Error.DefaultFactory("Error")), Settings);

    [Fact]
    public Task ShouldReturnFailure_WhenIsFailureIsTrueWithGenericError() =>
        Verify(Result.FailIf(true, new Error("Generic error")), Settings);

    [Fact]
    public Task ShouldReturnSuccess_WhenIsFailureIsFalseWithGenericError() =>
        Verify(Result.FailIf(false, new Error("Generic error")), Settings);
}
