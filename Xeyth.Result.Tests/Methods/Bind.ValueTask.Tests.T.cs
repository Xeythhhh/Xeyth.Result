using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public class BindGenericValueTask : TestBase
{
    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => ValueTask.FromResult(Result.Ok().WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => ValueTask.FromResult(Result.Fail("Binding Result Error"))),
            Settings);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Fail<int>("Initial Result Error")
                .Bind((_) => ValueTask.FromResult(Result.Ok().WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Fail<int>("Initial Result Error")
                .Bind((_) => ValueTask.FromResult(Result.Fail("Binding Result Error"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => ValueTask.FromResult(Result.Ok(69).WithSuccess("Binding Result Success"))),
            Settings);

    [Fact]
    public async Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => ValueTask.FromResult(Result.Fail<int>("Binding Result Error"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() =>
        await Verify(
            async () => await Result.Fail<int>("Initial Result Error")
                .Bind((_) => ValueTask.FromResult(Result.Ok(69).WithSuccess("Binding Result Success"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() =>
        await Verify(
            async () => await Result.Fail("Initial Result Error")
                .Bind(() => ValueTask.FromResult(Result.Fail<int>("Binding Result Error"))),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenBindActionIsNull() =>
        await ThrowsValueTask(() => Result.Ok(420).Bind((Func<int, ValueTask<Result>>)null!), Settings)
            .IgnoreStackTrace();

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() =>
        await ThrowsValueTask(() => Result.Ok(420).Bind((Func<int, ValueTask<Result<float>>>)null!), Settings)
        .IgnoreStackTrace();
}
