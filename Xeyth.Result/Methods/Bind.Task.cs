using System.Runtime.CompilerServices;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Binds the <see cref="Result"/> to another result without returning a value via the <paramref name="bind"/> asynchronous function returning a <see cref="Task"/>.</summary>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result> Bind(Func<Task<Result>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Try(async () => IsFailed ? this
            : (await bind().ConfigureAwait(false))
                .WithReasons(Reasons));
    }

    /// <summary>Binds the <see cref="Result"/> to another result via the <paramref name="bind"/> asynchronous function returning a <see cref="Task{TResult}"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value in the new result.</typeparam>
    /// <param name="bind">The binding function.</param>
    /// <returns>The new <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return await Try(async () => IsFailed ? ToResult<TNewValue>(default!)
            : (await bind().ConfigureAwait(false))
                .WithReasons(Reasons));
    }
}