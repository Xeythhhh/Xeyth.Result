using Xeyth.Result.Reasons;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Xeyth.Result.Tests.TypesForTesting;

public sealed class CustomErrorWithEmptyConstructor() : Error("Custom Error for testing with empty constructor");

public sealed class CustomError(string message) : Error(message)
{
    public string SomeCustomErrorField { get; set; } = "This is a custom Error implementation for testing";
}

public sealed class CustomSuccessWithEmptyConstructor() : Success("Custom Success for testing with empty constructor");

public sealed class CustomSuccess(string message) : Success(message)
{
    public string SomeCustomSuccessField { get; set; } = "This is a custom Success implementation for testing";
}
