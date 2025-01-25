using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests.Base;

public sealed class WithError
{
    [Fact]
    public void ShouldAddErrors()
    {
        // Arrange

        const string errorMessage = "Error for checking overloads";

        // Act

        Result result = Result.Fail(errorMessage)
            .WithError(errorMessage)
            .WithError(new Error(errorMessage))
            .WithError<CustomErrorWithEmptyConstructor>()
            .WithError(() => new CustomError(errorMessage))
            .WithErrors([errorMessage, errorMessage])
            .WithErrors([
                new Error(errorMessage),
                new CustomError(errorMessage),
                new CustomErrorWithEmptyConstructor()
                ]);

        // Assert

        result.Errors.Count.ShouldBe(10);
    }

    [Fact]
    public void NullCollection_ShouldThrowArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().WithErrors((IEnumerable<IError>)null!));
}
