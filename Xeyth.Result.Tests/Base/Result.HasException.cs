using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests.Base;
public partial class ResultBaseTests
{
    public class HasException : TestBase
    {
        [Fact]
        public void ShouldIdentifyExceptionPresence()
        {
            // Arrange

            const string exceptionMessage = "Exceptional Error";
            const string unusedExceptionMessage = "Another Error Message";

            Result result = Result.Ok()
                .WithError(new ExceptionalError(new Exception()))
                .WithError(new ExceptionalError(new Exception(exceptionMessage)))
                .WithError(new ExceptionalError(exceptionMessage, new Exception()))
                .WithError(new ExceptionalError(exceptionMessage, new Exception(exceptionMessage)));

            // Act and Assert

            result.HasException().ShouldBeTrue();

            result.HasException<Exception>().ShouldBeTrue();
            result.HasException<InvalidOperationException>().ShouldBeFalse();

            result.HasException(e => e.Message == exceptionMessage).ShouldBeTrue();
            result.HasException(e => e.Message == unusedExceptionMessage).ShouldBeFalse();

            result.HasException<Exception>(e => e.Message == exceptionMessage).ShouldBeTrue();
            result.HasException<InvalidOperationException>(e => e.Message == exceptionMessage).ShouldBeFalse();

            result.HasException<Exception>(
                e => e.Message == exceptionMessage,
                e => e.Message == exceptionMessage).ShouldBeTrue();

            result.HasException<Exception>(
                e => e.Message == unusedExceptionMessage,
                e => e.Message == exceptionMessage).ShouldBeFalse();

            result.HasException<Exception>(
                e => e.Message == exceptionMessage,
                e => e.Message == unusedExceptionMessage).ShouldBeFalse();

            result.HasException<InvalidOperationException>(
                e => e.Message == exceptionMessage,
                e => e.Message == exceptionMessage).ShouldBeFalse();

            // We do not test the overloads with an out parameter because they internally use the GetException methods that are tested separatedly
        }

        [Fact]
        public void NullPredicate_ShouldThrowArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => Result.Ok().HasException<Exception>(null!));
    }
}
