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
    public void ShouldInvokeAction_WhenResultIsSuccess(Result result, bool expectedActionInvoked)
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
    public async Task ShouldInvokeAsyncAction_WhenResultIsSuccess(Result result, bool expectedActionInvoked)
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
    public void ShouldInvokeActionWithSuccesses_WhenResultIsSuccess(Result result, bool expectedActionInvoked)
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
    public async Task ShouldInvokeAsyncActionWithSuccesses_WhenResultIsSuccess(Result result, bool expectedActionInvoked)
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
        Should.Throw<ArgumentNullException>(() => Result.Ok().OnSuccess((Action)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().OnSuccess((Func<Task>)null!));

    [Fact]
    public void ShouldThrow_WhenActionWithSuccessesIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().OnSuccess((Action<IEnumerable<ISuccess>>)null!));

    [Fact]
    public void ShouldThrow_WhenAsyncActionWithSuccessesIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().OnSuccess((Func<IEnumerable<ISuccess>, Task>)null!));
}
