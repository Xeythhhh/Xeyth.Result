using System.Collections;

using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class Try : TestBase
{
    public record TestCase(Func<Result> Action, bool ExpectedSuccess, bool ExpectedActionInvoked, bool ExpectedExceptionHandlerInvoked, bool UseDefaultExceptionHandler);

    public sealed class TryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Case 1: No exception (action invoked, exception handler not invoked)
            yield return new object[]
            {
                new TestCase(
                    () => Result.Ok(),
                    true,
                    true,
                    false,
                    false)
            };

            // Case 2: Exception thrown and custom handler invoked
            yield return new object[]
            {
                new TestCase(
                    () => throw new InvalidOperationException("Test exception"),
                    false,
                    true,
                    true,
                    false)
            };

            // Case 3: Exception thrown and default handler invoked (by passing null handler)
            yield return new object[]
            {
                new TestCase(
                    () => throw new InvalidOperationException("Test exception"),
                    false,
                    true,
                    false,
                    true)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TryData))]
    public void ShouldInvokeAction_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool actionInvoked = false;
        bool exceptionHandlerInvoked = false;

        void wrappedAction()
        {
            actionInvoked = true;
            testCase.Action();
        }

        ExceptionalError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result result = Result.Try(wrappedAction, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        actionInvoked.ShouldBe(testCase.ExpectedActionInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Theory]
    [ClassData(typeof(TryData))]
    public void ShouldInvokeActionWithReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        Result wrappedFunc()
        {
            funcInvoked = true;
            return testCase.Action();
        }

        ExceptionalError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result result = Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedActionInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_WhenActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Try((Action)null!));

    [Fact]
    public void ShouldThrowArgumentNullException_WhenActionWithReturnValueIsNull() =>
        Throws(() => Result.Try((Func<Result>)null!), Settings)
            .IgnoreStackTrace();
}
