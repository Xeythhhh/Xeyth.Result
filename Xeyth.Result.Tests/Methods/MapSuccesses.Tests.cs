using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

// Sample custom success for mapping tests
public class CustomSuccess(string message) : Success(message)
{
    public string SomeCustomSuccessField { get; set; } = "This is a custom Success implementation";
}

public class MapSuccesses : TestBase
{
    [Fact]
    public Task ShouldMapSuccesses_ToCustomSuccess() =>
        Verify(
            Result.Ok().WithSuccess(new CustomSuccess("Original Success")).MapSuccesses(
                s => new CustomSuccess($"(Mapped) {s.Message}")),
            Settings);

    [Fact]
    public async Task ShouldMapSuccessesAsynchronously_ToCustomSuccess() =>
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
    public Task ShouldNotChangeSuccessfulResult_WhenNoSuccesses() =>
        Verify(
            Result.Ok().MapSuccesses(
                s => new CustomSuccess($"(Mapped) {s.Message}")),
            Settings);

    [Fact]
    public async Task ShouldNotChangeSuccessfulResultAsynchronously_WhenNoSuccesses() =>
        await Verify(
            await Result.Ok().MapSuccessesAsync(
                async s => await Task.FromResult(new CustomSuccess($"(Async Mapped) {s.Message}"))),
            Settings);

    [Fact]
    public void ShouldThrow_WhenSuccessMapperIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().MapSuccesses(null!));

    [Fact]
    public async Task ShouldThrow_WhenAsyncSuccessMapperIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Ok().MapSuccessesAsync(null!));
}
