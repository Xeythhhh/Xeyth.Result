using Shouldly;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Tests;

public class Deconstruction(ITestOutputHelper output)
{
    private const string errorMessage = "Error message";

    [Fact]
    public void ShouldDeconstructSuccess()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed) = Result.Ok();

        // Assert
        isSuccess.ShouldBeTrue();
        isFailed.ShouldBeFalse();
    }

    [Fact]
    public void ShouldDeconstructSuccessWithEmptyErrorCollection()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, List<IError> errors) = Result.Ok();

        // Assert
        isSuccess.ShouldBeTrue();
        isFailed.ShouldBeFalse();

        foreach (IError error in errors)
            output.WriteLine(error.ToString() ?? "null");

        errors.ShouldBeEmpty();
    }

    [Fact]
    public void ShouldDeconstructFailure()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, List<IError> errors) = Result.Fail(errorMessage);

        // Assert
        isSuccess.ShouldBeFalse();
        isFailed.ShouldBeTrue();

        errors.ShouldContain(error => error.Message == errorMessage);
    }
}
