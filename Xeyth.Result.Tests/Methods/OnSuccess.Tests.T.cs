using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnSuccessT(ITestOutputHelper output)
{
    private const int _testValue = 420;

    public sealed class OnSuccessTData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                Result.Ok(_testValue),
                true
            };

            yield return new object[]
            {
                Result.Ok(_testValue).WithSuccess("Some success"),
                true
            };

            yield return new object[]
            {
                Result.Ok(_testValue).WithError("Test Error"),
                false
            };

            yield return new object[]
            {
                Result.Ok(_testValue).WithError("Test Error").WithSuccess("Some success"),
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
    [ClassData(typeof(OnSuccessTData))]
    public void OnSuccessGeneric_ShouldInvokeAction_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessTData))]
    public void OnSuccessGeneric_ShouldInvokeActionWithValue_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Action<int> action = new((value) => actionInvoked = value == _testValue);

        // Act
        result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessTData))]
    public async Task OnSuccessGeneric_ShouldInvokeAsyncAction_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessTData))]
    public async Task OnSuccessGeneric_ShouldInvokeAsyncActionWithValue_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
    {
        // Arrange
        bool actionInvoked = false;
        Func<int, Task> action = new((value) =>
        {
            actionInvoked = value == _testValue;
            return Task.CompletedTask;
        });

        // Act
        await result.OnSuccess(action);

        // Assert
        actionInvoked.ShouldBe(expectedActionInvoked);
    }

    [Theory]
    [ClassData(typeof(OnSuccessTData))]
    public void OnSuccessGeneric_ShouldInvokeActionWithErrors_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessTData))]
    public async Task OnSuccessGeneric_ShouldInvokeAsyncActionWithErrors_IfResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
