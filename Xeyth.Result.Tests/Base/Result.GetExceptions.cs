using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests.Base;
public partial class ResultBaseTests
{
    public class GetExceptions : TestBase
    {
        [Fact]
        public void ShouldFilterReturnExceptionsCorrectly()
        {
            // Arrange

            const string exceptionMessage = "Exceptional_Error";

            Result result = Result.Ok()
                .WithError(new Error("Error"))
                .WithError(new Error(exceptionMessage))
                .WithError(new ExceptionalError(new Exception()))
                .WithError(new ExceptionalError(new Exception(exceptionMessage)))
                .WithError(new ExceptionalError(new InvalidOperationException()))
                .WithError(new ExceptionalError(new InvalidOperationException(exceptionMessage))
                        .CausedBy(new CustomError("tracker 2")))
                .WithError(new ExceptionalError(exceptionMessage, new Exception()))
                .WithError(new ExceptionalError(exceptionMessage, new Exception(exceptionMessage)))
                .WithError(new ExceptionalError(exceptionMessage, new InvalidOperationException()))
                .WithError(new ExceptionalError(exceptionMessage, new InvalidOperationException(exceptionMessage))
                    .CausedBy(new ExceptionalError(exceptionMessage, new InvalidOperationException(exceptionMessage))
                        .CausedBy(new CustomError("For Tracking purposes1"))));

            // Act and Assert

            result.GetExceptions(error => error.Message == exceptionMessage).Count().ShouldBe(7);
            result.GetExceptions<InvalidOperationException>().Count().ShouldBe(5);
            result.GetExceptions<Exception>(e => e.Message == exceptionMessage).Count().ShouldBe(5);
            result.GetExceptions<InvalidOperationException>(e => e.Message == exceptionMessage).Count().ShouldBe(3);

            result.GetExceptions<Exception>(
                    e => e.Message == exceptionMessage,
                    e => e.Message == exceptionMessage)
                .Count().ShouldBe(5);

            result.GetExceptions<InvalidOperationException>(
                    e => e.Message == exceptionMessage,
                    e => e.Message == exceptionMessage)
                .Count().ShouldBe(3);
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
                .WithError(new ExceptionalError("Error 1", new Exception()).WithMetadata(existingKey1, value1))
                .WithError(new ExceptionalError("Error 2", new Exception()).WithMetadata(existingKey1, value1))
                .WithError(new ExceptionalError("Error 3", new Exception()).WithMetadata(existingKey1, value1))
                .WithError(new ExceptionalError("Error 3", new Exception()).WithMetadata(existingKey1, value2))
                .WithError(new ExceptionalError("Error 3", new Exception()).WithMetadata(existingKey2, value1))
                .WithError(new ExceptionalError("Error 4", new Exception()).WithMetadata(existingKey2, value2));

            // Act

            IEnumerable<ExceptionalError> key1Value1Errors = result.GetExceptionsWithMetadata(existingKey1, value1);
            IEnumerable<ExceptionalError> key1Value2Errors = result.GetExceptionsWithMetadata(existingKey1, value2);
            IEnumerable<ExceptionalError> key2Value2Errors = result.GetExceptionsWithMetadata(existingKey1, value2);
            IEnumerable<ExceptionalError> noMatchingErrors1 = result.GetExceptionsWithMetadata(unusedKey, value1);
            IEnumerable<ExceptionalError> noMatchingErrors2 = result.GetExceptionsWithMetadata(existingKey2, unusedValue);

            // Assert

            key1Value1Errors.Count().ShouldBe(3);
            key1Value2Errors.Count().ShouldBe(1);
            key2Value2Errors.Count().ShouldBe(1);
            noMatchingErrors1.ShouldBeEmpty();
            noMatchingErrors2.ShouldBeEmpty();
        }

        [Fact]
        public void NullPredicate_ShouldThrowArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => Result.Ok().GetExceptions<Exception>(null!));
    }
}
