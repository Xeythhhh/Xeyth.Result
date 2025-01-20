using System.Collections;

using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class TryAsyncGeneric : TestBase
{
    public record TestCase(Func<Task<int>> Func, int ExpectedValue, bool ExpectedSuccess, bool ExpectedFuncInvoked, bool ExpectedExceptionHandlerInvoked, bool UseDefaultExceptionHandler);

    public sealed class TryAsyncGenericData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Case 1: No exception (func invoked, exception handler not invoked)
            yield return new object[]
            {
                new TestCase(
                    () => Task.FromResult(420),
                    420,
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
                    0,
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
                    0,
                    false,
                    true,
                    false,
                    true)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TryAsyncGenericData))]
    public async Task ShouldInvokeAsyncActionWithReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        async Task<int> wrappedFunc()
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
        Result<int> result = await Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
        if (testCase.ExpectedSuccess)
        {
            result.Value.ShouldBe(testCase.ExpectedValue);
        }
    }

    [Theory]
    [ClassData(typeof(TryAsyncGenericData))]
    public async Task ShouldInvokeAsyncActionWithResultReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        async Task<Result<int>> wrappedFunc()
        {
            funcInvoked = true;
            return await testCase.Func(); // automatically converted to Result<int> via implicit conversion
        }

        IError customExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result<int> result = await Result.Try(wrappedFunc, testCase.UseDefaultExceptionHandler ? null : customExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
        if (testCase.ExpectedSuccess)
        {
            result.Value.ShouldBe(testCase.ExpectedValue);
        }
    }

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenAsyncActionWithReturnValueIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Try((Func<Task<int>>)null!));

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenAsyncActionWithResultReturnValueIsNull() =>
        await Should.ThrowAsync<ArgumentNullException>(() => Result.Try((Func<Task<Result<int>>>)null!));
}
