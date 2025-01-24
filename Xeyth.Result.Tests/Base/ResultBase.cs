using Shouldly;

using Xeyth.Result.Base;
using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Tests.Base;

public partial class ResultBaseTests(ITestOutputHelper output)
{
    [Fact]
    public void SuccessReasonsToString_ShouldReturnConcatenatedSuccessMessages()
    {
        // Arrange
        List<ISuccess> successes = [
            new Success("First success"),
            new Success("Second success")
        ];

        // Act
        string result = ResultBase.ReasonsToString(successes);

        // Assert
        result.ShouldBe("Success with Message='First success'; Success with Message='Second success'");
        output.WriteLine(result);
    }

    [Fact]
    public void ErrorReasonsToString_ShouldReturnConcatenatedErrorMessages()
    {
        // Arrange
        List<IError> errors = [
            new Error("First error"),
            new Error("Second error")
        ];

        // Act
        string result = ResultBase.ReasonsToString(errors);

        // Assert
        result.ShouldBe("Error with Message='First error'; Error with Message='Second error'");
        output.WriteLine(result);
    }

    [Fact]
    public void ErrorReasonsToString_ShouldReturnConcatenatedErrorMessagesForExceptionalError()
    {
        // Arrange
        List<IError> errors = [
            new ExceptionalError("First error", new Exception("First Exception message")),
            new ExceptionalError("Second error", new Exception("Second Exception message"))
        ];

        // Act
        string result = ResultBase.ReasonsToString(errors);

        // Assert
        result.ShouldBe("ExceptionalError with Message='First error', Exception='System.Exception: First Exception message'; ExceptionalError with Message='Second error', Exception='System.Exception: Second Exception message'");
        output.WriteLine(result);
    }
}
