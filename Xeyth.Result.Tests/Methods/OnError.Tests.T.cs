using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnErrorT(ITestOutputHelper output)
{
    private const int _testValue = 420;

    public sealed class OnErrorTData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                Result.Ok(_testValue),
                false
            };

            yield return new object[]
            {
                Result.Ok(_testValue).WithError("Test Error"),
                true
            };

            yield return new object[]
            {
                Result.Fail<int>("Test Error"),
                true
            };
        }
    }

    [Theory]
    [ClassData(typeof(OnErrorTData))]
    public void OnErrorGeneric_ShouldInvokeAction_IfResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorTData))]
    public async Task OnErrorGeneric_ShouldInvokeAsyncAction_IfResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorTData))]
    public void OnErrorGeneric_ShouldInvokeActionWithErrors_IfResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorTData))]
    public async Task OnErrorGeneric_ShouldInvokeAsyncActionWithErrors_IfResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
