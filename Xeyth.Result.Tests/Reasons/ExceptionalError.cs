using Xeyth.Result.Reasons;

namespace Xeyth.Result.Tests.Reasons;

public class ExceptionalErrorTests
{
    [Fact]
    public Task ShouldCreateExceptionalErrorWithMessageAndException() =>
        Verify(new ExceptionalError("Exceptional Error", new Exception("Exception")));

    [Fact]
    public Task ShouldCreateExceptionalErrorFromExceptionOnly() =>
        Verify(new ExceptionalError(new Exception("Exception")));

    [Fact]
    public Task ShouldHandleComplexNestedCauses() =>
        Verify(new ExceptionalError("Exceptional Error", new Exception("Exception"))
            .CausedBy<ExceptionalError>(new ExceptionalError("Inner Exceptional Error", new Exception("Inner Exception 1"))
                .CausedBy<ExceptionalError>(new Exception("Inner Exception 2 (Deep)"))));
}
