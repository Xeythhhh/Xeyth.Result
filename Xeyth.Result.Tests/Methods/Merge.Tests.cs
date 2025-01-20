using Shouldly;

using Xeyth.Result.Base;

namespace Xeyth.Result.Tests.Methods;

public class Merge : TestBase
{
    [Fact]
    public Task ShouldMergeFailures() =>
        Verify(
            Result.Merge(Result.Fail("Error 1"), Result.Fail("Error 2")),
            Settings);

    [Fact]
    public Task ShouldMergeSuccesses() =>
        Verify(
            Result.Merge(Result.Ok().WithSuccess("Success 1"), Result.Ok().WithSuccess("Success 2")),
            Settings);

    [Fact]
    public Task ShouldMergeSuccessesWithValue() =>
        Verify(
            Result.Merge(Result.Ok(420), Result.Ok(69)),
            Settings);

    [Fact]
    public Task ShouldMergeIEnumerableSuccessesWithValue() =>
        Verify(
            Result.Merge((IEnumerable<Result<int>>)[Result.Ok(420), Result.Ok(69)]),
            Settings);

    [Fact]
    public Task ShouldMergeMixedResults() =>
        Verify(
            Result.Merge(Result.Ok(), Result.Ok(420), Result.Fail("Error 3")),
            Settings)
            .ScrubMember<Result<IEnumerable<int>>>(r => r.Value);

    [Fact]
    public Task ShouldHandleEmptyInput() =>
        Verify(Result.Merge(Enumerable.Empty<ResultBase>()), Settings);

    [Fact]
    public void ShouldThrow_WhenInputIsNull() =>
        Should.Throw<ArgumentNullException>(() => Result.Merge(null!));
}
