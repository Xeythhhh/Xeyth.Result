using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Extensions.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorMessageFactory">A factory function to generate the error message if the result fails.</param>
    /// <returns>A success result if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue>(TValue value, bool isSuccess, Func<string> errorMessageFactory) =>
        OkIf(value, isSuccess, errorMessageFactory());

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A success result if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue>(TValue value, bool isSuccess, string errorMessage) =>
        OkIf(value, isSuccess, Error.Factory(errorMessage));

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A success result if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue, TError>(TValue value, bool isSuccess, TError error)
        where TError : IError =>
        OkIf(value, isSuccess, () => error);

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="isSuccess"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="isSuccess">Determines whether the result should be successful or failed.</param>
    /// <param name="errorFactory">A factory function to generate the error if the result fails. This is lazily evaluated.</param>
    /// <returns>A success result if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue, TError>(TValue value, bool isSuccess, Func<TError> errorFactory)
        where TError : IError =>
        OkIf(value, () => isSuccess, errorFactory);

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="predicate"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="predicate">A function to determine whether the result should be successful or failed.</param>
    /// <param name="errorMessageFactory">A factory function to generate the error message if the result fails.</param>
    /// <returns>A success result if <paramref name="predicate"/> returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue>(TValue value, Func<bool> predicate, Func<string> errorMessageFactory) =>
        OkIf(value, predicate, errorMessageFactory());

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="predicate"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="predicate">A function to determine whether the result should be successful or failed.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A success result if <paramref name="predicate"/> returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue>(TValue value, Func<bool> predicate, string errorMessage) =>
        OkIf(value, predicate, Error.Factory(errorMessage));

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="predicate"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="predicate">A function to determine whether the result should be successful or failed.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A success result if <paramref name="predicate"/> returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result<TValue> OkIf<TValue, TError>(TValue value, Func<bool> predicate, TError error)
        where TError : IError =>
        OkIf(value, predicate, () => error);

    /// <summary>Creates a success or failure <see cref="Result{TValue}"/> depending on the <paramref name="predicate"/> parameter.</summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>   /// <typeparam name="TError">The type of the error returned if the result fails.</typeparam>
    /// <param name="value">The value to include in the result if successful.</param>
    /// <param name="predicate">A function to determine whether the result should be successful or failed.</param>
    /// <param name="errorFactory">A factory function to generate the error if the result fails. This is lazily evaluated.</param>
    /// <returns>A success result if <paramref name="predicate"/> returns <see langword="true"/>; otherwise, a failure result.</returns>
    /// <remarks>By default, the <see cref="OkIf_ErrorFactory"/> generates an instance of <see cref="OkIf_PredicateError"/>.
    /// This behavior can be customized by assigning a different factory to <see cref="OkIf_ErrorFactory"/>.</remarks>
    /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the default error factory produces an error type incompatible with<typeparamref name="TError"/>.</exception>
    public static Result<TValue> OkIf<TValue, TError>(TValue value, Func<bool> predicate, Func<TError>? errorFactory = null)
        where TError : IError
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (predicate()) return Ok(value);

        errorFactory ??= typeof(TError).IsAssignableFrom(typeof(OkIf_PredicateError))
            ? () => OkIf_ErrorFactory().CastError<TError>()
            : throw new InvalidOperationException($"Default error is not compatible with {typeof(TError).Name}");

        return Fail(errorFactory());
    }
}
