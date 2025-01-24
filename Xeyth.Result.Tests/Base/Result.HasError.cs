using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests.Base;
public partial class ResultBaseTests
{
    public class HasError : TestBase
    {
        private class TestUnusedError(string errorMessage) : Error(errorMessage);

        [Fact]
        public void ShouldIdentifyErrorPresence()
        {
            // Arrange

            const string errorMessage = "Error for checking predicates";
            TestUnusedError unusedError = new(errorMessage);

            Result result = Result.Ok()
                .WithError("Error 1")
                .WithError(errorMessage)
                .WithError(new Error("Error 2"))
                .WithError(new Error(errorMessage))
                .WithError(new CustomError("Error 3"))
                .WithError(new CustomError(errorMessage));

            // Act and Assert

            result.HasError().ShouldBeTrue();

            result.HasError<IError>().ShouldBeTrue();
            result.HasError<Error>().ShouldBeTrue();
            result.HasError<CustomError>().ShouldBeTrue();
            result.HasError<TestUnusedError>().ShouldBeFalse();

            result.HasError(error => error.Message == errorMessage).ShouldBeTrue();
            result.HasError<Error>(error => error.Message == errorMessage).ShouldBeTrue();
            result.HasError<CustomError>(error => error.Message == errorMessage).ShouldBeTrue();
            result.HasError<TestUnusedError>(error => error.Message == errorMessage).ShouldBeFalse();
        }

        [Fact]
        public void NullPredicate_ShouldThrowArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => Result.Ok().HasError(null!));
    }
}
