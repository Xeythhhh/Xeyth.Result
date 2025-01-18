using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Creates a failure result if <paramref name="isFailure"/> is true, otherwise a success result.</summary>
    /// <param name="isFailure">Indicates whether to create a failure result.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A failure or success <see cref="Result"/> based on <paramref name="isFailure"/>.</returns>
    public static Result FailIf(bool isFailure, string error) =>
        isFailure ? Fail(error) : Ok();

    /// <summary>Creates a failure result if <paramref name="isFailure"/> is true, otherwise a success result.</summary>
    /// <param name="isFailure">Indicates whether to create a failure result.</param>
    /// <param name="error">The <see cref="IError"/> to include if the result fails.</param>
    /// <returns>A failure or success <see cref="Result"/> based on <paramref name="isFailure"/>.</returns>
    public static Result FailIf(bool isFailure, IError error) =>
        isFailure ? Fail(error) : Ok();

    /// <summary>Creates a failure result if <paramref name="isFailure"/> is true, otherwise a success result, with a lazily evaluated error message.</summary>
    /// <param name="isFailure">Indicates whether to create a failure result.</param>
    /// <param name="errorMessageFactory">A factory that generates the error message if the result fails.</param>
    /// <returns>A failure or success <see cref="Result"/> based on <paramref name="isFailure"/>.</returns>
    public static Result FailIf(bool isFailure, Func<string> errorMessageFactory) =>
        isFailure ? Fail(errorMessageFactory()) : Ok();

    /// <summary>Creates a failure result if <paramref name="isFailure"/> is true, otherwise a success result, with a lazily evaluated error.</summary>
    /// <param name="isFailure">Indicates whether to create a failure result.</param>
    /// <param name="errorFactory">A factory that generates the <see cref="IError"/> if the result fails.</param>
    /// <returns>A failure or success <see cref="Result"/> based on <paramref name="isFailure"/>.</returns>
    public static Result FailIf(bool isFailure, Func<IError> errorFactory) =>
        isFailure ? Fail(errorFactory()) : Ok();
}
