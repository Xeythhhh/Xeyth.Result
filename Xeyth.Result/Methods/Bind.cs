using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> function.</summary>
    /// <param name="bind">The function to transform the current result into a new <see cref="Result"/>.</param>
    /// <returns>A new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, the current result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result Bind(Func<Result> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return Try(() => bind().WithReasons(Reasons));
    }

    /// <summary>Binds the current result to another <see cref="Result{TValue}"/> using the specified <paramref name="bind"/> function.</summary>
    /// <typeparam name="TValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The function to transform the current result into a new <see cref="Result{TValue}"/>.</param>
    /// <returns>A new <see cref="Result{TValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result{TValue}"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result<TValue> Bind<TValue>(Func<Result<TValue>> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return Try(() => bind().WithReasons(Reasons));
    }
}
