using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Returns a new failure <see cref="Result"/> if the <paramref name="predicate"/> is <see langword="false"/>. Otherwise, returns the starting result.</summary>
    /// <param name="predicate">The predicate function to evaluate.</param>
    /// <param name="errorMessage">The <see cref="IError"/> message to return if the <paramref name="predicate"/> is <see langword="false"/>.</param>
    /// <returns>The original result or a new failure result.</returns>
    public Result Ensure(Func<bool> predicate, string errorMessage) =>
        Ensure(predicate, Error.Factory(errorMessage));

    /// <summary>Returns a new failure <see cref="Result"/> if the <paramref name="predicate"/> is <see langword="false"/>. Otherwise, returns the starting result.</summary>
    /// <typeparam name="TError">The type of the <see cref="IError"/> returned if the <paramref name="predicate"/> is <see langword="false"/>.</typeparam>
    /// <param name="predicate">The predicate function to evaluate.</param>
    /// <param name="error">The <see cref="IError"/> to return if the <paramref name="predicate"/> is <see langword="false"/>.</param>
    /// <returns>The original result or a new failure result.</returns>
    public Result Ensure<TError>(Func<bool> predicate, TError error)
        where TError : IError =>
        Ensure(predicate, () => error);

    /// <summary>Returns a new failure <see cref="Result"/> if the <paramref name="predicate"/> is <see langword="false"/>. Otherwise, returns the starting result.</summary>
    /// <typeparam name="TError">The type of the <see cref="IError"/> returned if the <paramref name="predicate"/> is <see langword="false"/>.</typeparam>
    /// <param name="predicate">The predicate function to evaluate.</param>
    /// <param name="errorFactory">The function that generates an <see cref="IError"/> if the <paramref name="predicate"/> is <see langword="false"/>.</param>
    /// <returns>The original result or a new failure result.</returns>
    public Result Ensure<TError>(Func<bool> predicate, Func<TError> errorFactory)
        where TError : IError =>
        Ensure(() => OkIf(predicate(), errorFactory()));

    /// <summary>Returns a new failure <see cref="Result"/> if the <paramref name="predicate"/> is a failure <see cref="Result"/>. Otherwise, returns the starting result.</summary>
    /// <param name="predicate">The predicate function that returns a <see cref="Result"/>.</param>
    /// <returns>The original result or a new failure result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
    public Result Ensure(Func<Result> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (IsFailed) return this;
        Result result = predicate();
        return result.IsFailed ? result.WithReasons(Reasons) : this;
    }
}
