using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests.Base;

public sealed class WithSuccess
{
    [Fact]
    public void ShouldAddSuccesss()
    {
        // Arrange

        const string successMessage = "Success for checking overloads";

        // Act

        Result result = Result.Ok()
            .WithSuccess(successMessage)
            .WithSuccess(new Success(successMessage))
            .WithSuccess<CustomSuccessWithEmptyConstructor>()
            .WithSuccess(() => new CustomSuccess(successMessage))
            .WithSuccesses([successMessage, successMessage])
            .WithSuccesses([
                new Success(successMessage),
                new CustomSuccess(successMessage),
                new CustomSuccessWithEmptyConstructor()
                ]);

        // Assert

        result.Successes.Count.ShouldBe(9);
    }

    [Fact]
    public void NullCollection_ShouldThrowArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().WithSuccesses((IEnumerable<ISuccess>)null!));
}