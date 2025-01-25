using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests.Reasons;

public sealed class SuccessTests
{
    [Fact]
    public void ShouldCreateSuccessWithMessage() => Verify(new Success("Success"));

    [Fact]
    public void ShouldAddMetadata() => Verify(new Success("Success")
        .WithMetadata("Key", "Value"));

    [Fact]
    public void ShouldAddMultipleMetadata() => Verify(new Success("Success")
        .WithMetadata(new Dictionary<string, object>
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" }
        }));

    [Fact]
    public void ShouldReturnStringRepresentation() => Verify(new Success("Success")
        .WithMetadata("Key", "Value")
        .ToString());

    [Fact]
    public void ShouldCreateSuccessUsingDefaultFactory() => Verify(Success.Factory("Success from factory"));

    [Fact]
    public void ShouldThrowWhenTryingToOverrideFactoryWithNull() => Should.Throw<ArgumentNullException>(() => Success.Factory = null!);

    [Fact]
    public void ShouldCreateSuccessAfterOverridingFactory()
    {
        // Arrange

        lock (Success.FactoryLock)
        {
            Func<string, ISuccess> originalFactory = Success.Factory;
            Success.Factory = message => new CustomSuccess($"(Success from overriden factory) {message}");

            // Act

            ISuccess success = Success.Factory("Success Message");

            // Assert

            Verify(success);

            // Cleanup

            Success.Factory = originalFactory;
        }
    }
}
