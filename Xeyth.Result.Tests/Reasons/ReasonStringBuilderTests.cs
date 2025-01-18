using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Reasons;

public class ReasonStringBuilderTests : SnapshotTestBase
{
    [Fact]
    public async Task ShouldSetReasonType() =>
        await Verify(new ReasonStringBuilder()
            .WithReasonType(typeof(Error))
            .Build(), Settings);

    [Fact]
    public async Task ShouldAddSingleInfo() =>
        await Verify(new ReasonStringBuilder()
            .WithReasonType(typeof(Error))
            .WithInfo("Key", "Value")
            .Build(), Settings);

    [Fact]
    public async Task ShouldAddMultipleInfos() =>
        await Verify(new ReasonStringBuilder()
            .WithReasonType(typeof(Error))
            .WithInfo("Key1", "Value1")
            .WithInfo("Key2", "Value2")
            .Build(), Settings);

    [Fact]
    public async Task ShouldIgnoreEmptyInfo() =>
        await Verify(new ReasonStringBuilder()
            .WithReasonType(typeof(Error))
            .WithInfo("Key1", string.Empty)
            .WithInfo("Key2", "Value2")
            .Build(), Settings);
}
