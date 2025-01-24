using Shouldly;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Tests.Abstract;

namespace Xeyth.Result.Tests.Base;
public partial class ResultBaseTests
{
    public class HasSuccess : TestBase
    {
        private class TestUnusedSuccess(string successMessage) : Success(successMessage);

        [Fact]
        public void ShouldIdentifySuccessPresence()
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
                .WithSuccess(new CustomSuccess(successMessage));

            // Act and Assert

            result.HasSuccess().ShouldBeTrue();

            result.HasSuccess<ISuccess>().ShouldBeTrue();
            result.HasSuccess<Success>().ShouldBeTrue();
            result.HasSuccess<CustomSuccess>().ShouldBeTrue();
            result.HasSuccess<TestUnusedSuccess>().ShouldBeFalse();

            result.HasSuccess(success => success.Message == successMessage).ShouldBeTrue();
            result.HasSuccess<Success>(success => success.Message == successMessage).ShouldBeTrue();
            result.HasSuccess<CustomSuccess>(success => success.Message == successMessage).ShouldBeTrue();
            result.HasSuccess<TestUnusedSuccess>(success => success.Message == successMessage).ShouldBeFalse();
        }

        [Fact]
        public void NullPredicate_ShouldThrowArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => Result.Ok().HasSuccess(null!));
    }
}
