using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnSuccess(ITestOutputHelper output)
{
    public sealed class OnSuccessData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                Result.Ok(),
                true
            };

            yield return new object[]
            {
                Result.Ok().WithSuccess("Some success"),
                true
            };

            yield return new object[]
            {
                Result.Ok().WithError("Test Error"),
                false
            };

            yield return new object[]
            {
                Result.Ok().WithError("Test Error").WithSuccess("Some success"),
                false
            };

            yield return new object[]
            {
                Result.Fail("Test Error"),
                false
            };

            yield return new object[]
            {
                Result.Fail("Test Error").WithSuccess("Some success"),
                false
            };
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
    public async Task OnSuccess_ShouldInvokeAsyncAction_IfResultIsSuccess(Result result, bool expectedActionInvoked)
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
    public void OnSuccess_ShouldInvokeActionWithErrors_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action<IEnumerable<ISuccess>> action = new((successes) =>
        {
            actionInvoked = true;
            foreach (ISuccess success in successes)
                output.WriteLine(success.Message);
        });

        // Act
        result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessData))]
    public async Task OnSuccess_ShouldInvokeAsyncActionWithErrors_IfResultIsSuccess(Result result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<IEnumerable<ISuccess>, Task> action = new((successes) =>
        {
            actionInvoked = true;
            foreach (ISuccess success in successes)
                output.WriteLine(success.Message);
            return Task.CompletedTask;
        });

        // Act
        await result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }
}
