using Shouldly;

using Xeyth.Result.Extensions.Reasons;
using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests.Reasons;

public sealed class ErrorTests
{
    [Fact]
    public void ShouldCreateErrorWithMessage() => Verify(new Error("Error"));

    [Fact]
    public void ShouldAddSingleCause() => Verify(new Error("Error")
        .CausedBy("Cause 1"));

    [Fact]
    public void ShouldAddSingleCauseFromConstructor() => Verify(new Error("Error",
        new Error("Cause 1")));

    [Fact]
    public void ShouldAddMultipleCauses() => Verify(new Error("Error")
        .CausedBy("Cause 1")
        .CausedBy("Cause 2"));

    [Fact]
    public void ShouldAddMultipleCausesStringEnumerable() => Verify(new Error("Error")
        .CausedBy((IEnumerable<string>)[
            "Cause 1",
            "Cause 2"]));

    [Fact]
    public Task ShouldAddMultipleCausesStringParams() => Verify(new Error("Error")
        .CausedBy("Cause 1", "Cause 2"));

    [Fact]
    public void ShouldAddMultipleCausesIErrorEnumerable() => Verify(new Error("Error")
        .CausedBy((IEnumerable<IError>)[
            new Error("Error 1"),
            new Error("Error 2")]));

    [Fact]
    public void ShouldAddMultipleCausesIErrorParams() => Verify(new Error("Error")
        .CausedBy(new Error("Error 1"), new Error("Error 2")));

    [Fact]
    public void ShouldAddExceptionAsCause() => Verify(new Error("Error")
        .CausedBy(new Exception("Exception")));

    [Fact]
    public void ShouldAddMultipleNestedCauses() => Verify(new Error("Error")
        .CausedBy(new Error("Cause 1")
            .CausedBy("Cause 2 (Deep)"))
        .CausedBy(new Exception("Exception")));

    [Fact]
    public void ShouldAddMetadata() => Verify(new Error("Error")
        .WithMetadata("Key", "Value"));

    [Fact]
    public void ShouldAddMultipleMetadata() => Verify(new Error("Error")
        .WithMetadata(new Dictionary<string, object>
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" }
        }));

    [Fact]
    public Task ShouldReturnStringRepresentation() => Verify(new Error("Error")
        .WithMetadata("Key", "Value")
        .CausedBy("Cause 1")
        .CausedBy(new Exception("Exception"))
        .ToString());

    [Fact]
    public Task ShouldCreateErrorUsingDefaultFactory() => Verify(Error.Factory("Error from factory"));

    [Fact]
    public void ShouldThrowWhenUsingDefaultFactoryWithNullInput() => Should.Throw<ArgumentNullException>(() => Error.Factory(null!));

    [Fact]
    public void ShouldThrowWhenTryingToOverrideFactoryWithNull() => Should.Throw<ArgumentNullException>(() => Error.Factory = null!);

    [Fact]
    public void ShouldCreateErrorAfterOverridingFactory()
    {
        lock (Error.FactoryLock)
        {
            // Arrange

            Func<string, IError> originalFactory = Error.Factory;
            Error.Factory = message => new CustomError($"(Error from overridden factory) {message}");

            // Act

            IError error = Error.Factory("Error Message");

            // Assert

            Verify(error);

            // Cleanup

            Error.Factory = originalFactory;
        }
    }

    [Fact]
    public void CastError_ShouldReturnCastedError_WhenTypeMatches()
    {
        // Arrange

        CustomError error = new("Custom Error message");

        // Act

        Error castedError = error.CastError<Error>();

        // Assert

        castedError.ShouldBeOfType<CustomError>();
        castedError.ShouldBe(error);
    }

    [Fact]
    public void CastError_ShouldThrow_WhenTypeMismatch()
    {
        // Arrange

        Error error = new("Error message");

        // Act & Assert

        Should.Throw<InvalidCastException>(() => error.CastError<CustomError>());
    }
}
