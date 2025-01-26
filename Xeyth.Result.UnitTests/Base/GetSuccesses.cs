using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.TypesForTesting;

namespace Xeyth.Result.Tests.Base;

public sealed class GetSuccesses(ITestOutputHelper output)
{
    private class TestUnusedSuccess(string successMessage) : Success(successMessage);

    [Fact]
    public void ShouldFilterSuccesssByType()
    {
        // Arrange

        const string successMessage = "Success for checking predicates";
        TestUnusedSuccess unusedSuccess = new(successMessage);

        Result result = Result.Ok()
            .WithSuccess("Success 1")
            .WithSuccess(successMessage)
            .WithSuccess(new Success("Success 2"))
            .WithSuccess(new Success(successMessage))
            .WithSuccess(new CustomSuccess("Success 3"))
            .WithSuccess(new CustomSuccess(successMessage))
            .WithSuccess(new CustomSuccess(successMessage));

        // Act

        //await Verify(result);

        result.GetSuccesses<Success>().Count().ShouldBe(7);

        IEnumerable<CustomSuccess> test = result.GetSuccesses<CustomSuccess>();
        foreach (CustomSuccess customSuccess in test)
        {
            output.WriteLine(customSuccess.ToString());
        }
        test.Count().ShouldBe(3);

        result.GetSuccesses<TestUnusedSuccess>().Count().ShouldBe(0);

        result.GetSuccesses(success => success.Message == successMessage).Count().ShouldBe(4);

        result.GetSuccesses<Success>(success => success.Message == successMessage).Count().ShouldBe(4);
        result.GetSuccesses<CustomSuccess>(success => success.Message == successMessage).Count().ShouldBe(2);
        result.GetSuccesses<TestUnusedSuccess>(success => success.Message == successMessage).Count().ShouldBe(0);
    }

    [Fact]
    public void WithMetadata_ShouldFilterBasedOnMetadata()
    {
        // Arrange

        const string existingKey1 = "Key 1";
        const string existingKey2 = "Key 2";
        const string unusedKey = "Key 3";
        const string value1 = "Value 1";
        const string value2 = "Value 2";
        const string unusedValue = "Value 3";

        Result result = Result.Ok()
            .WithSuccess(new Success("Success 1").WithMetadata(existingKey1, value1))
            .WithSuccess(new Success("Success 2").WithMetadata(existingKey1, value1))
            .WithSuccess(new Success("Success 3").WithMetadata(existingKey1, value1))
            .WithSuccess(new Success("Success 3").WithMetadata(existingKey1, value2))
            .WithSuccess(new Success("Success 3").WithMetadata(existingKey2, value1))
            .WithSuccess(new Success("Success 4").WithMetadata(existingKey2, value2));

        // Act

        IEnumerable<ISuccess> key1Value1Successs = result.GetSuccessesWithMetadata(existingKey1, value1);
        IEnumerable<ISuccess> key1Value2Successs = result.GetSuccessesWithMetadata(existingKey1, value2);
        IEnumerable<ISuccess> key2Value2Successs = result.GetSuccessesWithMetadata(existingKey1, value2);
        IEnumerable<ISuccess> noMatchingSuccesss1 = result.GetSuccessesWithMetadata(unusedKey, value1);
        IEnumerable<ISuccess> noMatchingSuccesss2 = result.GetSuccessesWithMetadata(existingKey2, unusedValue);

        // Assert

        key1Value1Successs.Count().ShouldBe(3);
        key1Value2Successs.Count().ShouldBe(1);
        key2Value2Successs.Count().ShouldBe(1);
        noMatchingSuccesss1.ShouldBeEmpty();
        noMatchingSuccesss2.ShouldBeEmpty();
    }

    [Fact]
    public void NullPredicate_ShouldThrowArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => Result.Ok().GetSuccesses(null!));
}
