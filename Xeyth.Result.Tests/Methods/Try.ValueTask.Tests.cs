using System.Collections;

using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class TryValueTask : TestBase
{
    public record TestCase(Func<ValueTask<Result>> Func, bool ExpectedSuccess, bool ExpectedFuncInvoked, bool ExpectedExceptionHandlerInvoked, bool UseDefaultExceptionHandler);

    public sealed class TryValueTaskData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Case 1: No exception (func invoked, exception handler not invoked)
            yield return new object[]
            {
                new TestCase(
                    () => new ValueTask<Result>(Result.Ok()),
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
    [ClassData(typeof(TryValueTaskData))]
    public async Task ShouldInvokeValueTaskActionWithResultReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        async ValueTask<Result> wrappedFunc()
        {
            funcInvoked = true;
            return await testCase.Func();
        }

        IError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result result = await Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenValueTaskActionWithResultReturnValueIsNull() =>
        await ThrowsValueTask(() => Result.Try((Func<ValueTask<Result>>)null!), Settings)
            .IgnoreStackTrace();
}
