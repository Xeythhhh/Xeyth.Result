using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    public class OkIfT : TestBase
    {
        [Fact]
        public Task ShouldReturnSuccessWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, true, "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, false, "Error"), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWhenPredicateIsTrue() =>
            Verify(Result.OkIf(420, () => true, "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenPredicateIsFalse() =>
            Verify(Result.OkIf(420, () => false, "Error"), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithErrorFactoryWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, true, () => new Error("Custom error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWithErrorFactoryWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, false, () => new Error("Custom error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithGenericErrorWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, true, new Error("Generic error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWithGenericErrorWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, false, new Error("Generic error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWhenPredicateIsTrueWithErrorFactory() =>
            Verify(Result.OkIf(420, () => true, () => new Error("Predicate error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenPredicateIsFalseWithErrorFactory() =>
            Verify(Result.OkIf(420, () => false, () => new Error("Predicate error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWhenPredicateIsTrueWithGenericError() =>
            Verify(Result.OkIf(420, () => true, new Error("Generic error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenPredicateIsFalseWithGenericError() =>
            Verify(Result.OkIf(420, () => false, new Error("Generic error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);
    }
}
