using Shouldly;

using Xeyth.Result.Exceptions;

namespace Xeyth.Result.Tests;

public class Value
{
    [Fact]
    public void ShouldThrow_WhenResultIsFailed() => Should.Throw<FailedResultValueAccessException>(() => Result.Fail<int>("Error").Value);
}
