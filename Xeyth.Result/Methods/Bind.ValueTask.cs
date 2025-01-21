namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Binds the <see cref="Result"/> to another result without returning a value via the <paramref name="bind"/> <see cref="ValueTask"/> returning a <see cref="ValueTask"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result> Bind(Func<ValueTask<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Try((Func<ValueTask<Result>>)(async () =>
            IsFailed ? this
                : (await bind()).WithReasons(Reasons)));
    }

    /// <summary>Binds the <see cref="Result"/> to another result via the <paramref name="bind"/> <see cref="ValueTask"/> returning a <see cref="ValueTask"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value in the new result.</typeparam>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result<TNewValue>> Bind<TNewValue>(Func<ValueTask<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Try((Func<ValueTask<Result<TNewValue>>>)(async () =>
            IsFailed ? ToResult((TNewValue)default!)
                : (await bind()).WithReasons(Reasons)));
    }
}