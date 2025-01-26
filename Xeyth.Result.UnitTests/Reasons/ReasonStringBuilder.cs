using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Reasons;

public sealed class ReasonStringBuilderTests
{
    [Fact]
    public void ShouldSetReasonType() => Verify(new ReasonStringBuilder()
        .WithReasonType(typeof(Error))
        .Build());

    [Fact]
    public void ShouldAddSingleInfo() => Verify(new ReasonStringBuilder().WithReasonType(typeof(Error))
        .WithInfo("Key", "Value")
        .Build());

    [Fact]
    public void ShouldAddMultipleInfos() => Verify(new ReasonStringBuilder().WithReasonType(typeof(Error))
        .WithInfo("Key1", "Value1")
        .WithInfo("Key2", "Value2")
        .Build());

    [Fact]
    public void ShouldIgnoreEmptyInfo() => Verify(new ReasonStringBuilder().WithReasonType(typeof(Error))
        .WithInfo("Key1", string.Empty)
        .WithInfo("Key2", "Value2")
        .Build());
}
