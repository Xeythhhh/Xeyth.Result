namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Binds the current result to another <see cref="Result"/> using the specified <paramref name="bind"/> function.
    /// If the current result is failed, this method returns the current result.
    /// Otherwise, it invokes the <paramref name="bind"/> function to produce a new result.</summary>
    /// <param name="bind">The function to transform the current result into a new <see cref="Result"/>.</param>
    /// <returns>A new <see cref="Result"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, the current failed result with its original reasons.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result Bind(Func<Result> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return Try(() => IsFailed ? this
            : bind().WithReasons(Reasons));
    }

    /// <summary>Binds the current result to another <see cref="Result{TNewValue}"/> using the specified <paramref name="bind"/> function.
    /// If the current result is failed , this method returns a failed
    /// <see cref="Result{TNewValue}"/> with the same reasons.
    /// Otherwise, it invokes the <paramref name="bind"/> function to produce a new result of type <typeparamref name="TNewValue"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value encapsulated by the new result.</typeparam>
    /// <param name="bind">The function to transform the current result into a new <see cref="Result{TNewValue}"/>.</param>
    /// <returns>A new <see cref="Result{TNewValue}"/> produced by the <paramref name="bind"/> function if the current result is successful;
    /// otherwise, a failed <see cref="Result{TNewValue}"/> with the same reasons.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="bind"/> function is <see langword="null"/>.</exception>
    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);

        return Try(() => IsFailed ? ToResult<TNewValue>(default!)
            : bind().WithReasons(Reasons));
    }
}
