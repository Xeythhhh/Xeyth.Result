using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Reasons;
using Xeyth.Result.Tests.Abstract;
using Shouldly;

namespace Xeyth.Result.Tests;
public class ConversionGeneric : TestBase
{
    [Fact]
    public Task ShouldConvertSingleErrorExplicitly() => Verify((Result<int>)new Error("Error"));

    [Fact]
    public Task ShouldConvertSingleErrorImplicitly()
    {
        Result<int> result = new Error("Error");
        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertErrorListExplicitly() => Verify((Result<int>)new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        });

    [Fact]
    public Task ShouldConvertErrorListImplicitly()
    {
        Result<int> result = new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        };

        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertToNonGenericResultExplicitly() => Verify((Result)Result.Ok(420));

    [Fact]
    public Task ShouldConvertToNonGenericResultImplicitly()
    {
        Result result = Result.Ok(420);
        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertToGenericObjectResultExplicitly() => Verify((Result<object>)Result.Ok(420));

    [Fact]
    public Task ShouldConvertToGenericObjectResultImplicitly()
    {
        Result<object> result = Result.Ok(420);
        return Verify(result);
    }

    [Fact]
    public Task ShouldConvertValueToGenericResultExplicitly() => Verify((Result<int>)420);

    [Fact]
    public Task ShouldConvertValueToGenericResultImplicitly()
    {
        Result<int> result = 420;
        return Verify(result);
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnSameResultInstance_WhenInputIsResult()
    {
        // Arrange
        Result<int> expectedResult = Result.Ok(42);

        // Act
        Result<int> result = expectedResult;

        // Assert
        result.ShouldBe(expectedResult);
    }
}
