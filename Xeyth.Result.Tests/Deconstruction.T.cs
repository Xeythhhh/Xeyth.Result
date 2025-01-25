using Shouldly;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Tests;

public sealed class DeconstructionGeneric(ITestOutputHelper output)
{
    private const string errorMessage = "Error message";
    private const int Value = 420;

    [Fact]
    public void ShouldDeconstructSuccess()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, int value) = Result.Ok(Value);

        // Assert
        isSuccess.ShouldBeTrue();
        isFailed.ShouldBeFalse();
        value.ShouldBe(Value);
    }

    [Fact]
    public void ShouldDeconstructSuccessWithEmptyErrorCollection()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, int value, List<IError> errors) = Result.Ok(Value);

        // Assert
        isSuccess.ShouldBeTrue();
        isFailed.ShouldBeFalse();

        foreach (IError error in errors)
            output.WriteLine(error.ToString() ?? "null");

        errors.ShouldBeEmpty();
        value.ShouldBe(Value);
    }

    [Fact]
    public void ShouldDeconstructFailure()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, int value) = Result.Fail<int>(errorMessage);

        // Assert
        isSuccess.ShouldBeFalse();
        isFailed.ShouldBeTrue();
        value.ShouldBe(default);
    }

    [Fact]
    public void ShouldDeconstructFailureWithErrors()
    {
        // Arrange and Act
        (bool isSuccess, bool isFailed, int value, List<IError> errors) = Result.Fail<int>(errorMessage);

        // Assert
        isSuccess.ShouldBeFalse();
        isFailed.ShouldBeTrue();

        foreach (IError error in errors)
            output.WriteLine(error.ToString() ?? "null");

        errors.ShouldContain(error => error.Message == errorMessage);
        value.ShouldBe(default);
    }
}
