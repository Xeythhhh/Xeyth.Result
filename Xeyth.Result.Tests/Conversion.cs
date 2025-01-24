using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests;

public class Conversion
{
    [Fact]
    public void ShouldConvertSingleErrorExplicitly() => Verify((Result)new Error("Error"));

    [Fact]
    public void ShouldConvertSingleErrorImplicitly()
    {
        Result result = new Error("Error");
        Verify(result);
    }

    [Fact]
    public void ShouldConvertErrorListExplicitly() => Verify((Result)new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        });

    [Fact]
    public void ShouldConvertErrorListImplicitly()
    {
        Result result = new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        };

        Verify(result);
    }

    [Fact]
    public void ShouldConvertToGenericResultExplicitly() => Verify((Result<int>)Result.Ok());

    [Fact]
    public void ShouldConvertToGenericResultImplicitly()
    {
        Result<int> result = Result.Ok();
        Verify(result);
    }
}
