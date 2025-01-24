using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> <see cref="ValueTask"/> function.</summary>
    /// <param name="bind">The <see cref="ValueTask"/> function to transform the current result into a new <see cref="Result"/>.</param>
    /// <returns>A <see cref="ValueTask"/> containing the new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, the current result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result> Bind(Func<ValueTask<Result>> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return await Try((Func<ValueTask<Result>>)(async () =>
            (await bind().ConfigureAwait(false))
                .WithReasons(Reasons)))
            .ConfigureAwait(false);
    }

    /// <summary>Binds the current result to another <see cref="Result{TValue}"/> using the specified <paramref name="bind"/> <see cref="ValueTask"/> function.</summary>
    /// <typeparam name="TValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The <see cref="ValueTask"/> function to transform the current result into a new <see cref="Result{TValue}"/>.</param>
    /// <returns>A <see cref="ValueTask"/> containing the new <see cref="Result{TValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result{TValue}"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result<TValue>> Bind<TValue>(Func<ValueTask<Result<TValue>>> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return await Try((Func<ValueTask<Result<TValue>>>)(async () => (await bind().ConfigureAwait(false))
                .WithReasons(Reasons)))
            .ConfigureAwait(false);
    }
}