using System.Runtime.CompilerServices;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Binds the <see cref="Task{TResult}"/> to another result without returning a value via the <paramref name="bind"/> asynchronous function returning a <see cref="Task"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result> Bind(Func<TValue, Task<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Result.Try(async () => IsFailed ? ToResult()
                : (await bind(Value).ConfigureAwait(false))
                    .WithReasons(Reasons));
    }

    /// <summary>Binds the <see cref="Task{TResult}"/> to another result via the <paramref name="bind"/> asynchronous function returning a <see cref="Task{TResult}"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value in the new result.</typeparam>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Task{TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Result.Try(async () => IsFailed ? ToResult<TNewValue>()
                : (await bind(Value).ConfigureAwait(false))
                    .WithReasons(Reasons));
    }
}