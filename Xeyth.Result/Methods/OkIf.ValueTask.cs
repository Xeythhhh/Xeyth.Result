using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf(ValueTask<bool> isSuccess, string errorMessage) =>
        OkIf(await isSuccess.ConfigureAwait(false), errorMessage);

    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf(bool isSuccess, ValueTask<string> errorMessage) =>
        OkIf(isSuccess, await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf(ValueTask<bool> isSuccess, ValueTask<string> errorMessage) =>
        OkIf(await isSuccess.ConfigureAwait(false), await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf<TError>(ValueTask<bool> isSuccess, TError error)
        where TError : IError =>
        OkIf(await isSuccess.ConfigureAwait(false), error);

    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf<TError>(bool isSuccess, ValueTask<TError> error)
        where TError : IError =>
        OkIf(isSuccess, await error.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result> OkIf<TError>(ValueTask<bool> isSuccess, ValueTask<TError> error)
        where TError : IError =>
        OkIf(await isSuccess.ConfigureAwait(false), await error.ConfigureAwait(false));
}
