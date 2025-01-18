namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Converts the current <see cref="Result"/> to a generic <see cref="Result{TNewValue}"/> with no value.</summary>
    /// <typeparam name="TNewValue">The type of the value for the new <see cref="Result{TNewValue}"/>.</typeparam>
    /// <returns>A new <see cref="Result{TNewValue}"/> containing the reasons from the current <see cref="Result"/>.</returns>
    public Result<TNewValue> ToResult<TNewValue>() =>
        new Result<TNewValue>().WithReasons(Reasons);

    /// <summary>Converts the current <see cref="Result"/> to a generic <see cref="Result{TNewValue}"/> with an associated value.</summary>
    /// <typeparam name="TNewValue">The type of the value for the new <see cref="Result{TNewValue}"/>.</typeparam>
    /// <param name="newValue">The value to associate with the new <see cref="Result{TNewValue}"/>.</param>
    /// <param name="validator">An optional function to validate the <paramref name="newValue"/>.</param>
    /// <returns>A new <see cref="Result{TNewValue}"/> containing the <paramref name="newValue"/> if valid, or a failed <see cref="Result{TNewValue}"/> 
    /// if the current <see cref="Result"/> is failed or the <paramref name="validator"/> fails.</returns>
    public Result<TNewValue> ToResult<TNewValue>(TNewValue newValue, Func<TNewValue, bool>? validator = null) =>
        IsFailed || validator != null && !validator(newValue)
            ? ToResult<TNewValue>()
            : ToResult<TNewValue>()
                .WithValue(newValue);
}
