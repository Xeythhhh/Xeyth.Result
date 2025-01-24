using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.Abstract;
using Xeyth.Result.Extensions.Reasons;

namespace Xeyth.Result.Tests.Reasons;

public class ErrorTests : TestBase
{
    [Fact]
    public Task ShouldCreateErrorWithMessage() =>
        Verify(new Error("Error"));

    [Fact]
    public Task ShouldAddSingleCause() =>
        Verify(new Error("Error").CausedBy("Cause 1"));

    [Fact]
    public Task ShouldAddSingleCauseFromConstructor() =>
        Verify(new Error("Error", new Error("Cause 1")));

    [Fact]
    public Task ShouldAddMultipleCauses() =>
        Verify(new Error("Error")
            .CausedBy("Cause 1")
            .CausedBy("Cause 2"));

    [Fact]
    public Task ShouldAddMultipleCausesStringEnumerable() =>
        Verify(new Error("Error").CausedBy((IEnumerable<string>)[
            "Cause 1",
            "Cause 2"]));

    [Fact]
    public Task ShouldAddMultipleCausesStringParams() =>
        Verify(new Error("Error")
            .CausedBy("Cause 1", "Cause 2"));

    [Fact]
    public Task ShouldAddMultipleCausesIErrorEnumerable() =>
        Verify(new Error("Error")
            .CausedBy((IEnumerable<IError>)[
                new Error("Error 1"),
                new Error("Error 2")]));

    [Fact]
    public Task ShouldAddMultipleCausesIErrorParams() =>
        Verify(new Error("Error")
            .CausedBy(new Error("Error 1"), new Error("Error 2")));

    [Fact]
    public Task ShouldAddExceptionAsCause() =>
        Verify(new Error("Error")
            .CausedBy(new Exception("Exception")));

    [Fact]
    public Task ShouldAddMultipleNestedCauses() =>
        Verify(new Error("Error")
            .CausedBy(new Error("Cause 1")
                .CausedBy("Cause 2 (Deep)"))
            .CausedBy(new Exception("Exception")));

    [Fact]
    public Task ShouldAddMetadata() =>
        Verify(new Error("Error")
            .WithMetadata("Key", "Value"));

    [Fact]
    public Task ShouldAddMultipleMetadata() =>
        Verify(new Error("Error")
            .WithMetadata(new Dictionary<string, object>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            }));

    [Fact]
    public Task ShouldReturnStringRepresentation() =>
        Verify(new Error("Error")
            .WithMetadata("Key", "Value")
            .CausedBy("Cause 1")
            .CausedBy(new Exception("Exception")).ToString());

    [Fact]
    public Task ShouldCreateErrorUsingDefaultFactory() =>
        Verify(Error.Factory("Error from factory"));

    [Fact]
    public void ShouldThrowWhenUsingDefaultFactoryWithNullInput() =>
        Should.Throw<ArgumentNullException>(() => Error.Factory(null!));

    [Fact]
    public void ShouldThrowWhenTryingToOverrideFactoryWithNull() =>
        Should.Throw<ArgumentNullException>(() => Error.Factory = null!);

    [Fact]
    public async Task ShouldCreateErrorAfterOverridingFactory()
    {
        // If this causes other tests to fail in the future, look into IAsynLifetime Setup/Teardown
        // and disable parallelization on this test class

        // Arrange

        Func<string, IError> originalFactory = Error.Factory;

        try
        {
            // Act

            Error.Factory = message => new CustomError($"(Error from overridden factory) {message}");
            IError error = Error.Factory("Error Message");

            // Assert

            await Verify(error);
        }
        finally
        {
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
