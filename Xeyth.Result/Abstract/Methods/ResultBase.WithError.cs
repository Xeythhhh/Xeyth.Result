using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase<TResult>
{
    /// <summary>Adds an <see cref="IError"/> to the <typeparamref name="TResult"/>.</summary>
    /// <remarks>The provided <paramref name="errorMessage"/> is converted using the <see cref="Error.Factory"/>.</remarks>
    /// <param name="errorMessage">The error message to create an <see cref="IError"/> instance.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithError(IError)"/>.</remarks>
    public TResult WithError(string errorMessage) =>
        WithError(Error.Factory(errorMessage));

    /// <summary>Adds an <see cref="IError"/> of type <typeparamref name="TError"/> to the <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to create and add.</typeparam>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithError(IError)"/>.</remarks>
    public TResult WithError<TError>()
        where TError : IError, new() =>
        WithError(new TError());

    /// <summary>Adds a <see cref="IError"/> to the <typeparamref name="TResult"/> using the <paramref name="errorFactory"/>.</summary>
    /// <param name="errorFactory">A factory function that creates an <see cref="IError"/> instance.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithError(IError)"/>.</remarks>
    public TResult WithError(Func<IError> errorFactory) =>
        WithError(errorFactory());

    /// <summary>Adds an <see cref="IError"/> to the <typeparamref name="TResult"/>.</summary>
    /// <param name="error">The <see cref="IError"/> instance to add to the result.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithReason(IReason)"/>.</remarks>
    public TResult WithError(IError error) =>
        WithReason(error);

    /// <summary>Adds multiple <see cref="IError"/>s to the <typeparamref name="TResult"/>.</summary>
    /// <param name="errorMessages">A collection of error messages to create <see cref="IError"/> instances.</param>
    /// <remarks>The provided <paramref name="errorMessages"/> are converted using the <see cref="Error.Factory"/>.</remarks>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithErrors(IEnumerable{IError})"/>.</remarks>
    public TResult WithErrors(IEnumerable<string> errorMessages) =>
        WithErrors(errorMessages.Select(Error.Factory));

    /// <summary>Adds multiple <see cref="IError"/>s to the <typeparamref name="TResult"/>.</summary>
    /// <param name="errors">A collection of <see cref="IError"/> instances to add to the result.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithReasons(IEnumerable{IReason})"/>.</remarks>
    public TResult WithErrors(IEnumerable<IError> errors) =>
        WithReasons(errors);
}
