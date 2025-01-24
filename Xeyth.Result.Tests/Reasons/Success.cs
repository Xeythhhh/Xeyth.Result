using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

using static Xeyth.Result.Tests.Abstract.TestBase;

namespace Xeyth.Result.Tests.Reasons;

public class SuccessTests
{
    [Fact]
    public async Task ShouldCreateSuccessWithMessage() =>
        await Verify(new Success("Success"));

    [Fact]
    public async Task ShouldAddMetadata() =>
        await Verify(new Success("Success")
            .WithMetadata("Key", "Value"));

    [Fact]
    public async Task ShouldAddMultipleMetadata() =>
        await Verify(new Success("Success")
            .WithMetadata(new Dictionary<string, object>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            }));

    [Fact]
    public async Task ShouldReturnStringRepresentation() =>
        await Verify(new Success("Success")
            .WithMetadata("Key", "Value").ToString());

    [Fact]
    public async Task ShouldCreateSuccessUsingDefaultFactory() =>
        await Verify(Success.Factory("Success from factory"));

    [Fact]
    public void ShouldThrowWhenTryingToOverrideFactoryWithNull() =>
        Should.Throw<ArgumentNullException>(() => Success.Factory = null!);

    [Fact]
    public async Task ShouldCreateSuccessAfterOverridingFactory()
    {
        // If this causes other tests to fail in the future, look into IAsynLifetime Setup/Teardown
        // and disable parallelization on this test class

        // Arrange

        Func<string, ISuccess> originalFactory = Success.Factory;

        try
        {
            // Act

            Success.Factory = message => new CustomSuccess($"(Success from overriden factory) {message}");
            ISuccess success = Success.Factory("Success Message");

            // Assert

            await Verify(success);
        }
        finally
        {
            // Cleanup

            Success.Factory = originalFactory;
        }
    }
}
