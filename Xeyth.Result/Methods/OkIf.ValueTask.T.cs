using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue>(TValue value, ValueTask<bool> isSuccess, string errorMessage) =>
        OkIf(value, await isSuccess.ConfigureAwait(false), errorMessage);

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue>(TValue value, bool isSuccess, ValueTask<string> errorMessage) =>
        OkIf(value, isSuccess, await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue>(TValue value, ValueTask<bool> isSuccess, ValueTask<string> errorMessage) =>
        OkIf(value, await isSuccess.ConfigureAwait(false), await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue, TError>(TValue value, ValueTask<bool> isSuccess, TError error)
        where TError : IError =>
        OkIf(value, await isSuccess.ConfigureAwait(false), error);

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue, TError>(TValue value, bool isSuccess, ValueTask<TError> error)
        where TError : IError =>
        OkIf(value, isSuccess, await error.ConfigureAwait(false));

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the value included in the result if successful.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    public static async ValueTask<Result<TValue>> OkIf<TValue, TError>(TValue value, ValueTask<bool> isSuccess, ValueTask<TError> error)
        where TError : IError =>
        OkIf(value, await isSuccess.ConfigureAwait(false), await error.ConfigureAwait(false));
}
