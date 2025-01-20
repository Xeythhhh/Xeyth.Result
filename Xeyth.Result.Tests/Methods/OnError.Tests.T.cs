using Xeyth.Result.Reasons;
using Shouldly;
using System.Collections;
using Xunit.Abstractions;

namespace Xeyth.Result.Tests.Methods;

public class OnErrorGeneric(ITestOutputHelper output)
{
    private const int _testValue = 420;

    public sealed class OnErrorGenericData : IEnumerable<object[]>
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
    [ClassData(typeof(OnErrorGenericData))]
    public void ShouldInvokeAction_WhenResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorGenericData))]
    public async Task ShouldInvokeAsyncAction_WhenResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorGenericData))]
    public void ShouldInvokeActionWithErrors_WhenResultIsFailure(Result<int> result, bool expectedActionInvoked)
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
    [ClassData(typeof(OnErrorGenericData))]
    public async Task ShouldInvokeAsyncActionWithErrors_WhenResultIsFailure(Result<int> result, bool expectedActionInvoked)
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

    [Fact]
    public void ShouldThrow_WhenActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnError((Action)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnError((Func<Task>)null!));

    [Fact]
    public void ShouldThrow_WhenActionWithErrorsIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnError((Action<IEnumerable<IError>>)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionWithErrorsIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok(420).OnError((Func<IEnumerable<IError>, Task>)null!));
}
