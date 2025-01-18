using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    public class FailIf : SnapshotTestBase
    {
        [Fact]
        public Task ShouldReturnFailureWhenIsFailureIsTrue() =>
            Verify(Result.FailIf(true, "Error"), Settings);

        [Fact]
        public Task ShouldReturnSuccessWhenIsFailureIsFalse() =>
            Verify(Result.FailIf(false, "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenIsFailureIsTrueAndErrorFactory() =>
            Verify(Result.FailIf(true, () => "Error"), Settings);

        [Fact]
        public Task ShouldReturnSuccessWhenIsFailureIsFalseAndErrorFactory() =>
            Verify(Result.FailIf(false, () => "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWithErrorWhenIsFailureIsTrue() =>
            Verify(Result.FailIf(true, Error.DefaultFactory("Error")), Settings);

        [Fact]
        public Task ShouldReturnSuccessWithErrorWhenIsFailureIsFalse() =>
            Verify(Result.FailIf(false, Error.DefaultFactory("Error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWithGenericErrorWhenIsFailureIsTrue() =>
            Verify(Result.FailIf(true, new Error("Generic error")), Settings);

        [Fact]
        public Task ShouldReturnSuccessWithGenericErrorWhenIsFailureIsFalse() =>
            Verify(Result.FailIf(false, new Error("Generic error")), Settings);
    }
}
