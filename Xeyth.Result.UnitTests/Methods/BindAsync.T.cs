using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public sealed class BindGenericAsync
{
    public sealed class ShouldBind
    {
        private const string Confirmation = "Bound to new result";

        [Fact]
        public Task WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() => Verify(() => Result.Ok(420)
            .Bind((_) => Task.FromResult(Result.Ok().WithSuccess(Confirmation))));

        [Fact]
        public Task AndKeepValue_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() => Verify(() => Result.Ok(420)
            .BindAndKeepValue((_) => Task.FromResult(Result.Ok().WithSuccess(Confirmation))));

        [Fact]
        public Task WhenInitialResultIsSuccessful_AndBindingResultIsFailure() => Verify(() => Result.Ok(420)
            .Bind((_) => Task.FromResult(Result.Fail(Confirmation))));

        [Fact]
        public Task AndKeepValue_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() => Verify(() => Result.Ok(420)
            .BindAndKeepValue((_) => Task.FromResult(Result.Fail(Confirmation))));

        [Fact]
        public Task WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() => Verify(() => Result.Ok(420)
            .Bind((_) => Task.FromResult(Result.Ok(69).WithSuccess(Confirmation))));

        [Fact]
        public Task WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() => Verify(() => Result.Ok(420)
            .Bind((_) => Task.FromResult(Result.Fail<int>(Confirmation))));
    }

    public sealed class ShouldNotBind
    {
        private const string Warning = "Should not bind!";
        private const string Error = "Initial Result Error";

        [Fact]
        public Task WhenInitialResultIsFailure_AndBindingResultIsSuccessful() => Verify(() => Result.Fail<int>(Error)
            .Bind((_) => Task.FromResult(Result.Ok().WithSuccess(Warning))));

        [Fact]
        public Task WhenInitialResultIsFailure_AndBindingResultIsFailure() => Verify(() => Result.Fail<int>(Error)
            .Bind((_) => Task.FromResult(Result.Fail(Warning))));

        [Fact]
        public Task WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() => Verify(() => Result.Fail<int>(Error)
            .Bind((_) => Task.FromResult(Result.Ok(69).WithSuccess(Warning))));

        [Fact]
        public Task WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() => Verify(() => Result.Fail(Error)
            .Bind(() => Task.FromResult(Result.Fail<int>(Warning))));
    }

    [Fact]
    public Task ShouldThrowArgumentNullException_WhenBindActionIsNull() => Should.ThrowAsync<ArgumentNullException>(() =>
        Result.Ok(420).Bind((Func<int, Task<Result>>)null!));

    [Fact]
    public Task ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() => Should.ThrowAsync<ArgumentNullException>(() =>
        Result.Ok(420).Bind((Func<int, Task<Result<float>>>)null!));
}
