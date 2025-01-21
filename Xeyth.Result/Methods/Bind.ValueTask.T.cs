namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Binds the <see cref="Task{TResult}"/> to another result without returning a value via the <paramref name="bind"/> <see cref="ValueTask"/> returning a <see cref="ValueTask"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result> Bind(Func<TValue, ValueTask<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Result.Try((Func<ValueTask<Result>>)(async () =>
            IsFailed ? ToResult()
                : (await bind(Value).ConfigureAwait(false))
                    .WithReasons(Reasons)));
    }

    /// <summary>Binds the <see cref="Task{TResult}"/> to another result via the <paramref name="bind"/> <see cref="ValueTask"/> returning a <see cref="ValueTask"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value in the new result.</typeparam>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public async ValueTask<Result<TNewValue>> Bind<TNewValue>(Func<TValue, ValueTask<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return await Result.Try((Func<ValueTask<Result<TNewValue>>>)(async () =>
            IsFailed ? ToResult<TNewValue>()
                : (await bind(Value).ConfigureAwait(false))
                    .WithReasons(Reasons)));
    }
}