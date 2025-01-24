using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> asynchronous function.</summary>
    /// <param name="bind">The asynchronous function to transform the current result's <see cref="Value"/> into a new <see cref="Result"/>.</param>
    /// <returns>A <see cref="Task"/> containing the new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    /// <remarks>If you want to keep the value, use <see cref="BindAndKeepValue(Func{TValue, Task{Result}})"/>.</remarks>
    [OverloadResolutionPriority(1)]
    public async Task<Result> Bind(Func<TValue, Task<Result>> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return await Result.Try(async () => (await bind(Value).ConfigureAwait(false))
                .WithReasons(Reasons))
            .ConfigureAwait(false);
    }

    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> asynchronous function.</summary>
    /// <param name="bind">The asynchronous function to transform the current result's <see cref="Value"/> into a new <see cref="Result"/>.</param>
    /// <returns>A <see cref="Task"/> containing the new <see cref="Result{TValue}"/> produced by the <paramref name="bind"/> function with the original <typeparamref name="TValue"/> value if the current result is successful;
    /// otherwise, the current result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    /// <remarks>This method delegates to <see cref="Bind(Func{TValue, Task{Result}})"/>.</remarks>
    [OverloadResolutionPriority(1)]
    public async Task<Result<TValue>> BindAndKeepValue(Func<TValue, Task<Result>> bind) =>
        (await Bind(bind).ConfigureAwait(false))
            .WithValue(IsSuccess ? Value : default!);

    /// <summary>Binds the current result to another <see cref="Result{TNewValue}"/> using the specified <paramref name="bind"/> asynchronous function with the current <see cref="Value"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The asynchronous function to transform the current result's <see cref="Value"/> into a new <see cref="Result{TNewValue}"/>.</param>
    /// <returns>A <see cref="Task"/> containing the new <see cref="Result{TNewValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result{TNewValue}"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> bind)
    {
        if (IsFailed) return ToResult<TNewValue>();
        ArgumentNullException.ThrowIfNull(bind);

        return await Result.Try(async () => (await bind(Value).ConfigureAwait(false))
                .WithReasons(Reasons))
            .ConfigureAwait(false);
    }
}