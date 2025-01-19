using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods
{
    // Sample custom success for mapping tests
    public class CustomSuccess(string message) : Success(message)
    {
        public string SomeCustomSuccessField { get; set; } = "This is a custom Success implementation";
    }

    public class MapSuccesses : SnapshotTestBase
    {
        [Fact]
        public Task ShouldMapSuccessesToCustomSuccess() =>
            Verify(
                Result.Ok().WithSuccess(new CustomSuccess("Original Success")).MapSuccesses(
                    s => new CustomSuccess($"(Mapped) {s.Message}")),
                Settings);

        [Fact]
        public async Task ShouldMapSuccessesAsynchronouslyToCustomSuccess() =>
            await Verify(
                await Result.Ok().WithSuccess(new CustomSuccess("Original Success")).MapSuccessesAsync(
                    async s => await Task.FromResult(new CustomSuccess($"(Async Mapped) {s.Message}"))),
                Settings);

        [Fact]
        public Task ShouldNotChangeErrorsInSuccessMapping() =>
            Verify(
                Result.Fail("Error").WithSuccess(new CustomSuccess("Success")).MapSuccesses(
                    s => new CustomSuccess($"(Mapped) {s.Message}")),
                Settings);

        [Fact]
        public Task ShouldNotChangeSuccessfulResultWhenNoSuccesses() =>
            Verify(
                Result.Ok().MapSuccesses(
                    s => new CustomSuccess($"(Mapped) {s.Message}")),
                Settings);

        [Fact]
        public async Task ShouldNotChangeSuccessfulResultAsynchronouslyWhenNoSuccesses() =>
            await Verify(
                await Result.Ok().MapSuccessesAsync(
                    async s => await Task.FromResult(new CustomSuccess($"(Async Mapped) {s.Message}"))),
                Settings);

        [Fact]
        public Task ShouldThrowWhenSuccessMapperIsNull() =>
            Throws(() => Result.Ok().MapSuccesses(null!), Settings)
                .IgnoreStackTrace();

        [Fact]
        public async Task ShouldThrowWhenAsyncSuccessMapperIsNull() =>
            await ThrowsTask(async () => await Result.Ok().MapSuccessesAsync(null!), Settings)
                .IgnoreStackTrace();
    }
}
