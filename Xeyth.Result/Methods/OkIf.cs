using Xeyth.Result.Reasons;
using Xeyth.Result.Extensions.ReasonExtensions;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Represents the default <see cref="IError"/> generated when the predicate fails, and no custom error factory is provided.</summary>
    public sealed class OkIf_PredicateError() : Error("OkIf_Predicate returned false.");

    private static Func<IError> _okIfDefaultErrorFactory = () => new OkIf_PredicateError();

    /// <summary>The default factory function used to generate an <see cref="IError"/> when the <see cref="OkIf{TError}(Func{bool}, Func{TError})" /> predicate fails. Defaults to creating an instance of <see cref="OkIf_PredicateError"/>.</summary>
    public static Func<IError> OkIf_ErrorFactory
    {
        get => _okIfDefaultErrorFactory;
        set => _okIfDefaultErrorFactory = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>Creates a success or failure <see cref="Result"/> based on the given predicate.</summary>
    /// <typeparam name="TError">The type of the error produced if the result fails.</typeparam>
    /// <param name="predicate">A function that determines whether the result should be successful or failed.</param>
    /// <param name="errorFactory">A lazily evaluated factory function to generate the error if the result fails. Defaults to <see cref="OkIf_ErrorFactory"/>.</param>
    /// <returns>A success <see cref="Result"/> if the predicate returns <see langword="true"/>; otherwise, a failure result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the default error factory produces an incompatible error type.</exception>
    public static Result OkIf<TError>(Func<bool> predicate, Func<TError>? errorFactory = null)
        where TError : IError
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (predicate()) return Ok();

        errorFactory ??= typeof(TError).IsAssignableFrom(typeof(OkIf_PredicateError))
            ? () => OkIf_ErrorFactory().CastError<TError>()
            : throw new InvalidOperationException($"Default error is not compatible with {typeof(TError).Name}");

        return Fail(errorFactory());
    }

    /// <summary>Creates a success or failure <see cref="Result"/> based on <paramref name="isSuccess"/>.</summary>
    /// <param name="isSuccess">Indicates whether the result should be successful.</param>
    /// <param name="errorMessageFactory">A factory function to generate the error message if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="errorMessageFactory"/> is <see langword="null"/>.</exception>
    public static Result OkIf(bool isSuccess, Func<string> errorMessageFactory) =>
        OkIf(() => isSuccess, () => Error.DefaultFactory(errorMessageFactory()));

    /// <summary>Creates a success or failure <see cref="Result"/> based on <paramref name="isSuccess"/>.</summary>
    /// <param name="isSuccess">Indicates whether the result should be successful.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf(bool isSuccess, string errorMessage) =>
        OkIf(() => isSuccess, () => Error.DefaultFactory(errorMessage));

    /// <summary>Creates a success or failure <see cref="Result"/> based on <paramref name="isSuccess"/>.</summary>
    /// <typeparam name="TError">The type of the error produced if the result fails.</typeparam>
    /// <param name="isSuccess">Indicates whether the result should be successful.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf<TError>(bool isSuccess, TError error)
        where TError : IError =>
        OkIf(() => isSuccess, () => error);

    /// <summary>Creates a success or failure <see cref="Result"/> based on <paramref name="isSuccess"/>.</summary>
    /// <typeparam name="TError">The type of the error produced if the result fails.</typeparam>
    /// <param name="isSuccess">Indicates whether the result should be successful.</param>
    /// <param name="errorFactory">A lazily evaluated factory function to generate the error if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if <paramref name="isSuccess"/> is <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf<TError>(bool isSuccess, Func<TError> errorFactory)
        where TError : IError =>
        OkIf(() => isSuccess, errorFactory);

    /// <summary>Creates a success or failure <see cref="Result"/> based on the given <paramref name="predicate"/>.</summary>
    /// <param name="predicate">A function that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessageFactory">A factory function to generate the error message if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if the predicate returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf(Func<bool> predicate, Func<string> errorMessageFactory) =>
        OkIf(predicate, () => Error.DefaultFactory(errorMessageFactory()));

    /// <summary>Creates a success or failure <see cref="Result"/> based on the given <paramref name="predicate"/>.</summary>
    /// <param name="predicate">A function that determines whether the result should be successful or failed.</param>
    /// <param name="errorMessage">The error message to include if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if the predicate returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf(Func<bool> predicate, string errorMessage) =>
        OkIf(predicate, () => Error.DefaultFactory(errorMessage));

    /// <summary>Creates a success or failure <see cref="Result"/> based on the given <paramref name="predicate"/>.</summary>
    /// <typeparam name="TError">The type of the error produced if the result fails.</typeparam>
    /// <param name="predicate">A function that determines whether the result should be successful or failed.</param>
    /// <param name="error">The error to include if the result fails.</param>
    /// <returns>A success <see cref="Result"/> if the predicate returns <see langword="true"/>; otherwise, a failure result.</returns>
    public static Result OkIf<TError>(Func<bool> predicate, TError error)
        where TError : IError =>
        OkIf(predicate, () => error);
}
