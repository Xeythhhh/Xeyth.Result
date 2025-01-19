using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;

namespace Xeyth.Result.Tests.Methods;

public class OnSuccess
{
    public sealed class OnSuccessData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Result.Ok().WithSuccess("Some success"), true };
            yield return new object[] { Result.Fail("Test Error").WithSuccess("Some success"), false };
        }
    }

    [Theory]
    [ClassData(typeof(OnSuccessData))]
    public void OnSuccess_ShouldInvokeAction_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action action = new(() => actionInvoked = true);

        // Act
        result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessData))]
    public async Task OnError_ShouldInvokeAsyncAction_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<Task> action = new(() =>
        {
            actionInvoked = true;
            return Task.CompletedTask;
        });

        // Act
        await result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessData))]
    public void OnError_ShouldInvokeActionWithErrors_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action<IEnumerable<ISuccess>> action = new((successes) => actionInvoked = successes.Any());

        // Act
        result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessData))]
    public async Task OnError_ShouldInvokeAsyncActionWithErrors_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<IEnumerable<ISuccess>, Task> action = new((successes) =>
        {
            actionInvoked = successes.Any();
            return Task.CompletedTask;
        });

        // Act
        await result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }
}
