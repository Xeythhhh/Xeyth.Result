using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests;

public class ConversionGeneric
{
    [Fact]
    public void ShouldConvertSingleErrorExplicitly() => Verify((Result<int>)new Error("Error"));

    [Fact]
    public void ShouldConvertSingleErrorImplicitly()
    {
        Result<int> result = new Error("Error");
        Verify(result);
    }

    [Fact]
    public void ShouldConvertErrorListExplicitly() => Verify((Result<int>)new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        });

    [Fact]
    public void ShouldConvertErrorListImplicitly()
    {
        Result<int> result = new List<IError>()
        {
            new Error("Error 1"),
            new CustomError("Error 2")
        };

        Verify(result);
    }

    [Fact]
    public void ShouldConvertToNonGenericResultExplicitly() => Verify((Result)Result.Ok(420));

    [Fact]
    public void ShouldConvertToNonGenericResultImplicitly()
    {
        Result result = Result.Ok(420);
        Verify(result);
    }

    [Fact]
    public void ShouldConvertToGenericObjectResultExplicitly() => Verify((Result<object>)Result.Ok(420));

    [Fact]
    public void ShouldConvertToGenericObjectResultImplicitly()
    {
        Result<object> result = Result.Ok(420);
        Verify(result);
    }

    [Fact]
    public void ShouldConvertValueToGenericResultExplicitly() => Verify((Result<int>)420);

    [Fact]
    public void ShouldConvertValueToGenericResultImplicitly()
    {
        Result<int> result = 420;
        Verify(result);
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
