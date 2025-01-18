using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    public class OkIfValueTask : SnapshotTestBase
    {
        [Fact]
        public Task ShouldReturnSuccessWhenValueTaskPredicateIsTrue() =>
            Verify(Result.OkIf(ValueTask.FromResult(true), "Error"), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenValueTaskPredicateIsFalse() =>
            Verify(Result.OkIf(ValueTask.FromResult(false), "Error"), Settings);

        [Fact]
        public Task ShouldReturnSuccessWhenPredicateIsTrueAndValueTaskErrorMessage() =>
            Verify(Result.OkIf(true, ValueTask.FromResult("Error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenPredicateIsFalseAndValueTaskErrorMessage() =>
            Verify(Result.OkIf(false, ValueTask.FromResult("Error")), Settings);

        [Fact]
        public Task ShouldReturnSuccessWithValueTaskError() =>
            Verify(Result.OkIf(ValueTask.FromResult(true), ValueTask.FromResult(Error.DefaultFactory("Task error"))), Settings);

        [Fact]
        public Task ShouldReturnFailureWithValueTaskError() =>
            Verify(Result.OkIf(ValueTask.FromResult(false), ValueTask.FromResult(Error.DefaultFactory("Task error"))), Settings);

        [Fact]
        public Task ShouldReturnSuccessWhenValueTaskPredicateIsTrueWithGenericError() =>
            Verify(Result.OkIf(ValueTask.FromResult(true), new Error("Task error")), Settings);

        [Fact]
        public Task ShouldReturnFailureWhenValueTaskPredicateIsFalseWithGenericError() =>
            Verify(Result.OkIf(ValueTask.FromResult(false), new Error("Task error")), Settings);
    }
}
