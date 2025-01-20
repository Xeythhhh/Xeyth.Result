using System.Collections;

using Shouldly;

using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Methods;

public class TryValueTaskGeneric : TestBase
{
    public record TestCase(
        Func<ValueTask<Result<int>>> Func,
        bool ExpectedSuccess,
        int ExpectedValue,
        bool ExpectedFuncInvoked,
        bool ExpectedExceptionHandlerInvoked,
        bool UseDefaultExceptionHandler);

    public sealed class TryValueTaskGenericData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Case 1: No exception (success, value returned)
            yield return new object[]
            {
                new TestCase(
                    () => new ValueTask<Result<int>>(Result.Ok(420)),
                    true,
                    420,
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
                    default,
                    true,
                    true,
                    false)
            };

            // Case 3: Exception thrown and default handler invoked
            yield return new object[]
            {
                new TestCase(
                    () => throw new InvalidOperationException("Test exception"),
                    false,
                    default,
                    true,
                    false,
                    true)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TryValueTaskGenericData))]
    public async Task ShouldInvokeValueTaskActionWithReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        async ValueTask<int> WrappedFunc()
        {
            funcInvoked = true;
            return (await testCase.Func()).ValueOrDefault;
        }

        IError CustomExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result<int> result = await Result.Try(
            WrappedFunc,
            testCase.UseDefaultExceptionHandler ? null : CustomExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        result.ValueOrDefault.ShouldBe(testCase.ExpectedValue);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Theory]
    [ClassData(typeof(TryValueTaskGenericData))]
    public async Task ShouldInvokeValueTaskActionWithResultReturnValue_AndExceptionHandlerCorrectly(TestCase testCase)
    {
        // Arrange
        bool funcInvoked = false;
        bool exceptionHandlerInvoked = false;

        async ValueTask<Result<int>> WrappedFunc()
        {
            funcInvoked = true;
            return await testCase.Func();
        }

        IError CustomExceptionHandler(Exception ex)
        {
            exceptionHandlerInvoked = true;
            return new ExceptionalError("Handled exception", ex);
        }

        // Act
        Result<int> result = await Result.Try(
            WrappedFunc,
            testCase.UseDefaultExceptionHandler ? null : CustomExceptionHandler);

        // Assert
        result.IsSuccess.ShouldBe(testCase.ExpectedSuccess);
        result.ValueOrDefault.ShouldBe(testCase.ExpectedValue);
        funcInvoked.ShouldBe(testCase.ExpectedFuncInvoked);
        exceptionHandlerInvoked.ShouldBe(testCase.ExpectedExceptionHandlerInvoked);
    }

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenValueTaskActionWithReturnValueIsNull() =>
        await ThrowsValueTask(() => Result.Try((Func<ValueTask<int>>)null!), Settings)
            .IgnoreStackTrace();

    [Fact]
    public async Task ShouldThrowArgumentNullException_WhenValueTaskActionWithResultReturnValueIsNull() =>
        await ThrowsValueTask(() => Result.Try((Func<ValueTask<Result<int>>>)null!), Settings)
            .IgnoreStackTrace();
}
