namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> function.
    /// If the current result is failed, the function returns the current result.
    /// If successful, it invokes the <paramref name="bind"/> function with the current <see cref="Value"/>.</summary>
    /// <param name="bind">The function to transform the current result's value into a new <see cref="Result"/>.</param>
    /// <returns>A new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, the current failed result with its reasons.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result Bind(Func<TValue, Result> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return Result.Try(() => IsFailed ? ToResult()
            : bind(Value).WithReasons(Reasons));
    }

    /// <summary>Binds the current result to another <see cref="Result{TNewValue}"/> using the specified <paramref name="bind"/> function.
    /// If the current result is failed, the function returns the current result as a failed
    /// <see cref="Result{TNewValue}"/> with the same reasons
    /// If successful, it invokes the <paramref name="bind"/> function with the current <see cref="Value"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The function to transform the current result's value into a new <see cref="Result{TNewValue}"/>.</param>
    /// <returns>A new <see cref="Result{TNewValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, the current failed result as a failed <see cref="Result{TNewValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return Result.Try(() => IsFailed ? ToResult<TNewValue>()
            : bind(Value).WithReasons(Reasons));
    }
}
