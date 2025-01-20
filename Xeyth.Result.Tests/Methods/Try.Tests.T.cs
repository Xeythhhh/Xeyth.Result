using System.Collections;

using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class TryGeneric : TestBase
{
    public record TestCase(Func<Result<int>> Func, bool ExpectedSuccess, bool ExpectedFuncInvoked, bool ExpectedExceptionHandlerInvoked, bool UseDefaultExceptionHandler);

    public sealed class TryGenericData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Case 1: No exception (func invoked, exception handler not invoked)
            yield return new object[]
            {
                new TestCase(
                    () => Result.Ok(420),
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
    [ClassData(typeof(TryGenericData))]
    public void ShouldInvokeActionWithReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        int wrappedFunc()
        {
            funcInvoked = true;
            return testCase.Func().ValueOrDefault;
        }

        IError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result<int> result = Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Theory]
    [ClassData(typeof(TryGenericData))]
    public void ShouldInvokeActionWithResultReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        Result<int> wrappedFunc()
        {
            funcInvoked = true;
            return testCase.Func();
        }

        IError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result<int> result = Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_WhenActionWithReturnValueIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Try((Func<int>)null!));

    [Fact]
    public void ShouldThrowArgumentNullException_WhenActionWithResultReturnValueIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Try((Func<Result<int>>)null!));
}
