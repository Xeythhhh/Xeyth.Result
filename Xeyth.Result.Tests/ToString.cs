namespace Xeyth.Result.Tests;
public static class ToString
{
    public class NonGeneric
    {
        [Fact]
        public Task ShouldPrintSuccess() => Verify(Result.Ok()
            .ToString());

        [Fact]
        public Task ShouldPrintFailure() => Verify(Result.Fail("Error message")
            .ToString());
    }

    public class Generic
    {
        [Fact]
        public Task ShouldPrintSuccess() => Verify(Result.Ok(420)
            .ToString());

        [Fact]
        public Task ShouldPrintFailure_ForValueType() => Verify(Result.Fail<int>("Error message")
            .ToString());

        [Fact]
        public Task ShouldPrintFailureWithValue_ForValueType() => Verify(Result.Ok(420).WithError("Error message")
            .ToString());

        [Fact]
        public Task ShouldPrintFailureWithoutValue_ForReferenceType() => Verify(Result.Fail<SomeReferenceType>("Error message")
            .ToString());

        [Fact]
        public Task ShouldPrintFailureWithValue_ForReferenceType() => Verify(Result.Ok(new SomeReferenceType()).WithError("Error message")
            .ToString());
    }

    public sealed class SomeReferenceType()
    {
        public override string ToString() => "Value for testing";
    }
}
