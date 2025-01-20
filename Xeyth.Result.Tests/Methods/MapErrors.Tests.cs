using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class MapErrors : TestBase
{
    public class CustomError(string message) : Error(message)
    {
        public string SomeCustomErrorField { get; set; } = "This is a custom Error implementation";
    }

    [Fact]
    public Task ShouldMapErrors_ToCustomError() =>
        Verify(
            Result.Fail("Original Error").MapErrors(
                e => new CustomError($"(Mapped) {e.Message}")),
            Settings);

    [Fact]
    public Task ShouldMapErrors_ToExceptionalError() =>
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
    public async Task ShouldMapErrorsAsynchronously_ToCustomError() =>
        await Verify(
            await Result.Fail("Original Error").MapErrorsAsync(
                async e => await Task.FromResult(new CustomError($"(Async Mapped) {e.Message}"))),
            Settings);

    [Fact]
    public async Task ShouldMapErrorsAsynchronously_ToExceptionalError() =>
        await Verify(
            await Result.Fail("Original Error").MapErrorsAsync(
                async e => await Task.FromResult(new ExceptionalError($"(Async Mapped) {e.Message}", new InvalidOperationException("Async exception detail")))),
            Settings);

    [Fact]
    public void ShouldThrow_WhenErrorMapperIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Fail("Original Error").MapErrors(null!));

    [Fact]
    public async Task ShouldThrow_WhenAsyncErrorMapperIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Fail("Original Error").MapErrorsAsync(null!));
}
