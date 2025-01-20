using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnError(ITestOutputHelper output)
{
    public sealed class OnErrorData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                Result.Ok(),
                false
            };

            yield return new object[]
            {
                Result.Ok().WithError("Test Error"),
                true
            };

            yield return new object[]
            {
                Result.Fail("Test Error"),
                true
            };
        }
    }

    [Theory]
    [ClassData(typeof(OnErrorData))]
    public void OnError_ShouldInvokeAction_IfResultIsFailure(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action action = new(() => actionInvoked = true);

        // Act
        result.OnError(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnErrorData))]
    public async Task OnError_ShouldInvokeAsyncAction_IfResultIsFailure(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<Task> action = new(() =>
        {
            actionInvoked = true;
            return Task.CompletedTask;
        });

        // Act
        await result.OnError(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnErrorData))]
    public void OnError_ShouldInvokeActionWithErrors_IfResultIsFailure(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action<IEnumerable<IError>> action = new((errors) =>
        {
            actionInvoked = errors.Any();
            foreach (IError error in errors)
                output.WriteLine(error.Message);
        });

        // Act
        result.OnError(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnErrorData))]
    public async Task OnError_ShouldInvokeAsyncActionWithErrors_IfResultIsFailure(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<IEnumerable<IError>, Task> action = new((errors) =>
        {
            actionInvoked = errors.Any();
            foreach (IError error in errors)
                output.WriteLine(error.Message);
            return Task.CompletedTask;
        });

        // Act
        await result.OnError(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }
}
