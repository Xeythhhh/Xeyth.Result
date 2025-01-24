using Shouldly;

namespace Xeyth.Result.Tests.Methods;

public class BindGeneric
{
    public class ShouldBind
    {
        private const string Confirmation = "Bound to new result";

        [Fact]
        public void WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
            Verify(Result.Ok(420)
                .Bind((_) => Result.Ok().WithSuccess(Confirmation)));

        [Fact]
        public void AndKeepValue_WhenInitialResultIsSuccessful_AndBindingResultIsSuccessful() =>
            Verify(Result.Ok(420)
                .BindAndKeepValue((_) => Result.Ok().WithSuccess(Confirmation)));

        [Fact]
        public void WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
            Verify(Result.Ok(420)
                .Bind((_) => Result.Fail(Confirmation)));

        [Fact]
        public void AndKeepValue_WhenInitialResultIsSuccessful_AndBindingResultIsFailure() =>
            Verify(Result.Ok(420)
                .BindAndKeepValue((_) => Result.Fail(Confirmation)));

        [Fact]
        public void WhenInitialResultIsSuccessful_AndGenericBindingResultIsSuccessful() =>
            Verify(Result.Ok(420)
                .Bind((_) => Result.Ok(69).WithSuccess(Confirmation)));

        [Fact]
        public void WhenInitialResultIsSuccessful_AndGenericBindingResultIsFailure() =>
            Verify(Result.Ok(420)
                .Bind((_) => Result.Fail<int>(Confirmation)));
    }

    public class ShouldNotBind
    {
        private const string Warning = "Should not bind!";
        private const string Error = "Initial Result Error";

        [Fact]
        public void WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
            Verify(Result.Fail<int>(Error)
                .Bind((_) => Result.Ok().WithSuccess(Warning)));

        [Fact]
        public void AndKeepValue_WhenInitialResultIsFailure_AndBindingResultIsSuccessful() =>
            Verify(Result.Fail<int>(Error)
                .BindAndKeepValue((_) => Result.Ok().WithSuccess(Warning)));

        [Fact]
        public void WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
            Verify(Result.Fail<int>(Error)
                .Bind((_) => Result.Fail(Warning)));

        [Fact]
        public void AndKeepValue_WhenInitialResultIsFailure_AndBindingResultIsFailure() =>
            Verify(Result.Fail<int>(Error)
                .BindAndKeepValue((_) => Result.Fail(Warning)));

        [Fact]
        public void WhenInitialResultIsFailure_AndGenericBindingResultIsSuccessful() =>
            Verify(Result.Fail<int>(Error)
                .Bind((_) => Result.Ok(420).WithSuccess(Warning)));

        [Fact]
        public void WhenInitialResultIsFailure_AndGenericBindingResultIsFailure() =>
            Verify(Result.Fail<int>(Error)
                .Bind((_) => Result.Fail<int>(Warning)));
    }

    [Fact]
    public void ShouldThrowArgumentNullException_WhenBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).Bind((Func<int, Result>)null!));

    [Fact]
    public void ShouldThrowArgumentNullException_WhenGenericBindActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).Bind((Func<int, Result<float>>)null!));
}
