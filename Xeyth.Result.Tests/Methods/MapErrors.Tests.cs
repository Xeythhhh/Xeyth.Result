using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    public class MapErrors : TestBase
    {
        // Sample custom error for mapping tests
        public class CustomError(string message) : Error(message)
        {
            public string SomeCustomErrorField { get; set; } = "This is a custom Error implementation";
        }

        [Fact]
        public Task ShouldMapErrorsToCustomError() =>
            Verify(
                Result.Fail("Original Error").MapErrors(
                    e => new CustomError($"(Mapped) {e.Message}")),
                Settings);

        [Fact]
        public Task ShouldMapErrorsToExceptionalError() =>
            Verify(
                Result.Fail("Original Error").MapErrors(
                    e => new ExceptionalError($"(Mapped) {e.Message}", new InvalidOperationException("Exception detail"))),
                Settings);

        [Fact]
        public Task ShouldNotChangeSuccessfulResult() =>
            Verify(
                Result.Ok().MapErrors(_ => new Error("Should not map")),
                Settings);

        [Fact]
        public async Task ShouldMapErrorsAsynchronouslyToCustomError() =>
            await Verify(
                await Result.Fail("Original Error").MapErrorsAsync(
                    async e => await Task.FromResult(new CustomError($"(Async Mapped) {e.Message}"))),
                Settings);

        [Fact]
        public async Task ShouldMapErrorsAsynchronouslyToExceptionalError() =>
            await Verify(
                await Result.Fail("Original Error").MapErrorsAsync(
                    async e => await Task.FromResult(new ExceptionalError($"(Async Mapped) {e.Message}", new InvalidOperationException("Async exception detail")))),
                Settings);

        [Fact]
        public Task ShouldThrowWhenErrorMapperIsNull() =>
            Throws(() => Result.Fail("Original Error").MapErrors(null!), Settings)
                .IgnoreStackTrace();

        [Fact]
        public async Task ShouldThrowWhenAsyncErrorMapperIsNull() =>
            await ThrowsTask(
                async () => await Result.Fail("Original Error").MapErrorsAsync(null!),
                Settings)
                .IgnoreStackTrace();
    }
}
