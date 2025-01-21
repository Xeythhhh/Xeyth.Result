using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public class Bind : TestBase
{
    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
        Verify(
            Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Result.Ok().WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
        Verify(
            Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Result.Fail("Binding Result Error")),
            Settings);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
        Verify(
            Result.Fail("Initial Result Error")
                .Bind(() => Result.Ok().WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
        Verify(
            Result.Fail("Initial Result Error")
                .Bind(() => Result.Fail("Binding Result Error")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() =>
        Verify(
            Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Result.Ok(420).WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() =>
        Verify(
            Result.Ok().WithSuccess("Initial Result Success")
                .Bind(() => Result.Fail<int>("Binding Result Error")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() =>
        Verify(
            Result.Fail("Initial Result Error")
                .Bind(() => Result.Ok(420).WithSuccess("Binding Result Success")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() =>
        Verify(
            Result.Fail("Initial Result Error")
                .Bind(() => Result.Fail<int>("Binding Result Error")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public void ShouldThrowArgumentNullException_WhenBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().Bind((Func<Result>)null!));

    [Fact]
    public void ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().Bind((Func<Result<object>>)null!));
}
