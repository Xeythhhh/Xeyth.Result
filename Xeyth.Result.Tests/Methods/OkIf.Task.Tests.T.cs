using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    public class OkIfTaskT : TestBase
    {
        [Fact]
        public Task ShouldReturnSuccessWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, Task.FromResult(true), "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, Task.FromResult(false), "Error"), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWhenIsSuccessIsTrueWithTaskErrorMessage() =>
            Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult("Error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenIsSuccessIsFalseWithTaskErrorMessage() =>
            Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult("Error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithErrorWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, Task.FromResult(true), new Error("Custom error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWithErrorWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, Task.FromResult(false), new Error("Custom error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithTaskErrorFactoryWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult(new Error("Custom error"))), Settings);

        [Fact]
        public Task ShouldReturnFailureWithTaskErrorFactoryWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult(new Error("Custom error"))), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithErrorFactoryWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, Task.FromResult(true), new Error("Generic error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWithErrorFactoryWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, Task.FromResult(false), new Error("Generic error")), Settings)
                .ScrubMember<Result<int>>(r => r.Value);

        [Fact]
        public Task ShouldReturnSuccessWithTaskErrorWhenIsSuccessIsTrue() =>
            Verify(Result.OkIf(420, Task.FromResult(true), Task.FromResult(new Error("Predicate error"))), Settings);

        [Fact]
        public Task ShouldReturnFailureWithTaskErrorWhenIsSuccessIsFalse() =>
            Verify(Result.OkIf(420, Task.FromResult(false), Task.FromResult(new Error("Predicate error"))), Settings)
                .ScrubMember<Result<int>>(r => r.Value);
    }
}
