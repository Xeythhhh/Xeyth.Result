using Shouldly;

using Xeyth.Result.Extensions.Reasons;
using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Reasons;

public sealed class Metadata
{
    [Fact]
    public void HasMetadataKey_ShouldReturnTrue_WhenKeyExists() => new Success("Test").WithMetadata("Key", "Value")
        .HasMetadataKey("Key")
        .ShouldBeTrue();

    [Fact]
    public void HasMetadataKey_ShouldReturnFalse_WhenKeyDoesNotExist() => new Success("Test").WithMetadata("Key", "Value")
        .HasMetadataKey("Another Key")
        .ShouldBeFalse();

    [Fact]
    public void HasMetadataKey_ShouldThrowArgumentNullException_WhenKeyIsNull() => Should.Throw<ArgumentNullException>(() => new Success("Test")
        .HasMetadataKey(null!));

    [Fact]
    public void HasMetadataKey_ShouldThrowArgumentException_WhenKeyIsEmpty() => Should.Throw<ArgumentException>(() => new Success("Test")
        .HasMetadataKey(string.Empty));

    [Fact]
    public void HasMetadata_ShouldReturnTrue_WhenKeyExistsAndPredicateMatches() => new Success("Test").WithMetadata("Key", 420)
        .HasMetadata("Key", value => (int)value == 420)
        .ShouldBeTrue();

    [Fact]
    public void HasMetadata_ShouldReturnFalse_WhenKeyExistsAndPredicateDoesNotMatch() => new Success("Test").WithMetadata("Key", 420)
        .HasMetadata("Key", value => (int)value == 69)
        .ShouldBeFalse();

    [Fact]
    public void HasMetadata_ShouldReturnFalse_WhenKeyDoesNotExist() => new Success("Test")
        .HasMetadata("Key", _ => true)
        .ShouldBeFalse();

    [Fact]
    public void HasMetadata_ShouldThrowArgumentNullException_WhenKeyIsNull() => Should.Throw<ArgumentNullException>(() => new Success("Test")
        .HasMetadata(null!, _ => true));

    [Fact]
    public void HasMetadata_ShouldThrowArgumentException_WhenKeyIsEmpty() => Should.Throw<ArgumentException>(() => new Success("Test")
        .HasMetadata(string.Empty, _ => true));

    [Fact]
    public void HasMetadata_ShouldThrowArgumentNullException_WhenPredicateIsNull() => Should.Throw<ArgumentNullException>(() => new Success("Test")
        .HasMetadata("Key", null!));
}
