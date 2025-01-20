using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnSuccessGeneric(ITestOutputHelper output)
{
    private const int _testValue = 420;

    public sealed class OnSuccessGenericData : IEnumerable<object[]>
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
    [ClassData(typeof(OnSuccessGenericData))]
    public void ShouldInvokeAction_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessGenericData))]
    public void ShouldInvokeActionWithValue_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessGenericData))]
    public async Task ShouldInvokeAsyncAction_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessGenericData))]
    public async Task ShouldInvokeAsyncActionWithValue_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessGenericData))]
    public void ShouldInvokeActionWithSuccesses_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnSuccessGenericData))]
    public async Task ShouldInvokeAsyncActionWithSuccesses_WhenResultIsSuccess(Result<int> result, bool expectedActionInvoked)
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

    [Fact]
    public void ShouldThrow_WhenActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Action)null!));

    [Fact]
    public void ShouldThrow_WhenGenericActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Action<int>)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Func<Task>)null!));

    [Fact]
    public void ShouldThrow_WhenGenericAsyncActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Func<int, Task>)null!));

    [Fact]
    public void ShouldThrow_WhenActionWithSuccessesIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Action<IEnumerable<ISuccess>>)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionWithSuccessesIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnSuccess((Func<IEnumerable<ISuccess>, Task>)null!));
}
