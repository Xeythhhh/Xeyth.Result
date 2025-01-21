using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public class BindGeneric : TestBase
{
    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
        Verify(
            Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => Result.Ok().WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
        Verify(
            Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => Result.Fail("Binding Result Error")),
            Settings);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
        Verify(
            Result.Fail<int>("Initial Result Error")
                .Bind((_) => Result.Ok().WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
        Verify(
            Result.Fail<int>("Initial Result Error")
                .Bind((_) => Result.Fail("Binding Result Error")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() =>
        Verify(
            Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => Result.Ok(69).WithSuccess("Binding Result Success")),
            Settings);

    [Fact]
    public Task ShouldBind_WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() =>
        Verify(
            Result.Ok(420).WithSuccess("Initial Result Success")
                .Bind((_) => Result.Fail<int>("Binding Result Error")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() =>
        Verify(
            Result.Fail<int>("Initial Result Error")
                .Bind((_) => Result.Ok(420).WithSuccess("Binding Result Success")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public Task ShouldNotBind_WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() =>
        Verify(
            Result.Fail<int>("Initial Result Error")
                .Bind((_) => Result.Fail<int>("Binding Result Error")),
            Settings)
            .ScrubMember<Result<int>>(r => r.Value);

    [Fact]
    public void ShouldThrowArgumentNullException_WhenBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).Bind((Func<int, Result>)null!));

    [Fact]
    public void ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).Bind((Func<int, Result<float>>)null!));
}
