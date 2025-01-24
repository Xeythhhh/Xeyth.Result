using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests.Base;
public partial class ResultBaseTests
{
    public class GetErrors : TestBase
    {
        private class TestUnusedError(string errorMessage) : Error(errorMessage);

        [Fact]
        public void ShouldFilterErrorsByType()
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
                .WithError(new CustomError(errorMessage))
                .WithError(new CustomError(errorMessage)
                    .CausedBy(new Error("Nested Error"))
                    .CausedBy(new CustomError(errorMessage)));

            // Act

            result.GetErrors<Error>().Count().ShouldBe(9);
            result.GetErrors<CustomError>().Count().ShouldBe(4);
            result.GetErrors<TestUnusedError>().Count().ShouldBe(0);

            result.GetErrors(error => error.Message == errorMessage).Count().ShouldBe(5);

            result.GetErrors<Error>(error => error.Message == errorMessage).Count().ShouldBe(5);
            result.GetErrors<CustomError>(error => error.Message == errorMessage).Count().ShouldBe(3);
            result.GetErrors<TestUnusedError>(error => error.Message == errorMessage).Count().ShouldBe(0);
        }

        [Fact]
        public void WithMetadata_ShouldFilterBasedOnMetadata()
        {
            // Arrange

            const string existingKey1 = "Key 1";
            const string existingKey2 = "Key 2";
            const string unusedKey = "Key 3";
            const string value1 = "Value 1";
            const string value2 = "Value 2";
            const string unusedValue = "Value 3";

            Result result = Result.Ok()
                .WithError(new Error("Error 1").WithMetadata(existingKey1, value1))
                .WithError(new Error("Error 2").WithMetadata(existingKey1, value1))
                .WithError(new Error("Error 3").WithMetadata(existingKey1, value1))
                .WithError(new Error("Error 3").WithMetadata(existingKey1, value2))
                .WithError(new Error("Error 3").WithMetadata(existingKey2, value1))
                .WithError(new Error("Error 4").WithMetadata(existingKey2, value2));

            // Act

            IEnumerable<IError> key1Value1Errors = result.GetErrorsWithMetadata(existingKey1, value1);
            IEnumerable<IError> key1Value2Errors = result.GetErrorsWithMetadata(existingKey1, value2);
            IEnumerable<IError> key2Value2Errors = result.GetErrorsWithMetadata(existingKey1, value2);
            IEnumerable<IError> noMatchingErrors1 = result.GetErrorsWithMetadata(unusedKey, value1);
            IEnumerable<IError> noMatchingErrors2 = result.GetErrorsWithMetadata(existingKey2, unusedValue);

            // Assert

            key1Value1Errors.Count().ShouldBe(3);
            key1Value2Errors.Count().ShouldBe(1);
            key2Value2Errors.Count().ShouldBe(1);
            noMatchingErrors1.ShouldBeEmpty();
            noMatchingErrors2.ShouldBeEmpty();
        }

        [Fact]
        public void NullPredicate_ShouldThrowArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => Result.Ok().GetErrors(null!));
    }
}
