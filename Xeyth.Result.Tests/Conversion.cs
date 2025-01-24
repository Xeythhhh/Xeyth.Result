using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Reasons;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests;
public class Conversion : TestBase
{
    [Fact]
    public Task ShouldConvertSingleErrorExplicitly() => Verify((Result)new Error("Error"));

    [Fact]
    public Task ShouldConvertSingleErrorImplicitly()
    {
        Result result = new Error("Error");
        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertErrorListExplicitly() => Verify((Result)new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        });

    [Fact]
    public Task ShouldConvertErrorListImplicitly()
    {
        Result result = new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        };

        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertToGenericResultExplicitly() => Verify((Result<int>)Result.Ok());

    [Fact]
    public Task ShouldConvertToGenericResultImplicitly()
    {
        Result<int> result = Result.Ok();
        return Verify(result);
    }
}
