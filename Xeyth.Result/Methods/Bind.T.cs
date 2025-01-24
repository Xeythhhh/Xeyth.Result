using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> function.</summary>
    /// <param name="bind">The function to transform the current result's <see cref="Value"/> into a new <see cref="Result"/>.</param>
    /// <returns>A new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    /// <remarks>If you want to keep the <see langword="value"/>, use <see cref="BindAndKeepValue(Func{TValue, Result})"/>.</remarks>
    public Result Bind(Func<TValue, Result> bind)
    {
        if (IsFailed) return this;
        ArgumentNullException.ThrowIfNull(bind);

        return Result.Try(() => bind(Value).WithReasons(Reasons));
    }

    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> function and keeps the <typeparamref name="TValue"/> value.</summary>
    /// <param name="bind">The function to transform the current result's <see cref="Value"/> into a new <see cref="Result"/>.</param>
    /// <returns>A new <see cref="Result{TValue}"/> produced by the <paramref name="bind"/> function with the original <typeparamref name="TValue"/> value if the current result is successful;
    /// otherwise, the current result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    /// <remarks>This method delegates to <see cref="Bind(Func{TValue, Result})"/>.</remarks>
    public Result<TValue> BindAndKeepValue(Func<TValue, Result> bind) => Bind(bind)
        .WithValue(IsSuccess ? Value : default!)
        .WithReasons(Reasons);

    /// <summary>Binds the current result to another <see cref="Result{TNewValue}"/> using the specified <paramref name="bind"/> function with the current <see cref="Value"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The function to transform the current result's <see cref="Value"/> into a new <see cref="Result{TNewValue}"/>.</param>
    /// <returns>A new <see cref="Result{TNewValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result{TNewValue}"/> with the same <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> bind)
    {
        if (IsFailed) return ToResult<TNewValue>();
        ArgumentNullException.ThrowIfNull(bind);

        return Result.Try(() => bind(Value).WithReasons(Reasons));
    }
}
