using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed. The operation's result is awaited.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf(Task<bool> isSuccess, string errorMessage) =>
        OkIf(await isSuccess.ConfigureAwait(false), errorMessage);

    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails. The result is awaited.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf(bool isSuccess, Task<string> errorMessage) =>
        OkIf(isSuccess, await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed. The operation's result is awaited.</param>
    /// <param name="errorMessage">An asynchronous operation that provides the error message if the result fails. The result is awaited.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf(Task<bool> isSuccess, Task<string> errorMessage) =>
        OkIf(await isSuccess.ConfigureAwait(false), await errorMessage.ConfigureAwait(false));

    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails. The error must implement <see cref="IError"/>.</typeparam>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed. The operation's result is awaited.</param>
    /// <param name="error">The error to include if the result fails. This can be a synchronous or asynchronous operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf<TError>(Task<bool> isSuccess, TError error)
        where TError : IError =>
        OkIf(await isSuccess.ConfigureAwait(false), error);

    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails. The error must implement <see cref="IError"/>.</typeparam>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails. The result is awaited.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf<TError>(bool isSuccess, Task<TError> error)
        where TError : IError =>
        OkIf(isSuccess, await error.ConfigureAwait(false));

    /// <summary>Creates a success or failure result depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TError">The type of the error returned if the result fails. The error must implement <see cref="IError"/>.</typeparam>
    /// <param name="isSuccess">An asynchronous operation that determines whether the result should be successful or failed. The operation's result is awaited.</param>
    /// <param name="error">An asynchronous operation that provides the error if the result fails. The result is awaited.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is either a success or failure result.</returns>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> OkIf<TError>(Task<bool> isSuccess, Task<TError> error)
        where TError : IError =>
        OkIf(await isSuccess.ConfigureAwait(false), await error.ConfigureAwait(false));
}
