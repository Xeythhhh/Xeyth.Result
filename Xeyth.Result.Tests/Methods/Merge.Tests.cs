using Xeyth.Result.Base;

namespace Xeyth.Result.Tests.Methods
{
    public class Merge : SnapshotTestBase
    {
        [Fact]
        public Task ShouldMergeFailures() =>
            Verify(
                Result.Merge(Result.Fail("Error 1"), Result.Fail("Error 2")),
                Settings);

        [Fact]
        public Task ShouldMergeSuccesses() =>
            Verify(
                Result.Merge(Result.Ok(), Result.Ok()),
                Settings);

        [Fact]
        public Task ShouldMergeSuccessesWithValue() =>
            Verify(
                Result.Merge(Result.Ok(420), Result.Ok(69)),
                Settings);

        [Fact]
        public Task ShouldMergeMixedResults() =>
            Verify(
                Result.Merge(Result.Ok(), Result.Ok(420), Result.Fail("Error 3")),
                Settings)
                .ScrubMember<Result<IEnumerable<int>>>(r => r.Value);

        [Fact]
        public Task ShouldHandleEmptyInput() =>
            Verify(Result.Merge(Enumerable.Empty<ResultBase>()),Settings);

        [Fact]
        public Task ShouldThrowForNullInput() =>
            Throws(() => Result.Merge(null!), Settings)
                .IgnoreStackTrace();
    }
}
