using Shouldly;

using Xeyth.Result.Extensions.Reasons;
using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Reasons;
public class Metadata
{
    [Fact]
    public void HasMetadataKey_ShouldReturnTrue_WhenKeyExists()
    {
        // Arrange
        Success reason = new Success("Test").WithMetadata("Key", "Value");

        // Act
        bool result = reason.HasMetadataKey("Key");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasMetadataKey_ShouldReturnFalse_WhenKeyDoesNotExist()
    {
        // Arrange
        Success reason = new Success("Test").WithMetadata("OtherKey", "Value");

        // Act
        bool result = reason.HasMetadataKey("Key");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasMetadataKey_ShouldThrowArgumentNullException_WhenKeyIsNull()
    {
        // Arrange
        Success reason = new("Test");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => reason.HasMetadataKey(null!));
    }

    [Fact]
    public void HasMetadataKey_ShouldThrowArgumentException_WhenKeyIsEmpty()
    {
        // Arrange
        Success reason = new("Test");

        // Act & Assert
        Should.Throw<ArgumentException>(() => reason.HasMetadataKey(string.Empty));
    }

    [Fact]
    public void HasMetadata_ShouldReturnTrue_WhenKeyExistsAndPredicateMatches()
    {
        // Arrange
        Success reason = new Success("Test").WithMetadata("Key", 42);

        // Act
        bool result = reason.HasMetadata("Key", value => (int)value == 42);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasMetadata_ShouldReturnFalse_WhenKeyExistsAndPredicateDoesNotMatch()
    {
        // Arrange
        Success reason = new Success("Test").WithMetadata("Key", 42);

        // Act
        bool result = reason.HasMetadata("Key", value => (int)value == 24);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasMetadata_ShouldReturnFalse_WhenKeyDoesNotExist()
    {
        // Arrange
        Success reason = new("Test");

        // Act
        bool result = reason.HasMetadata("Key", _ => true);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasMetadata_ShouldThrowArgumentNullException_WhenKeyIsNull()
    {
        // Arrange
        Success reason = new("Test");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => reason.HasMetadata(null!, _ => true));
    }

    [Fact]
    public void HasMetadata_ShouldThrowArgumentException_WhenKeyIsEmpty()
    {
        // Arrange
        Success reason = new("Test");

        // Act & Assert
        Should.Throw<ArgumentException>(() => reason.HasMetadata(string.Empty, _ => true));
    }

    [Fact]
    public void HasMetadata_ShouldThrowArgumentNullException_WhenPredicateIsNull()
    {
        // Arrange
        Success reason = new("Test");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => reason.HasMetadata("Key", null!));
    }
}
