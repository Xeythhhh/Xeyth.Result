using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;
public partial class Result
{
    /// <summary>Converts the current <see cref="Result"/> to a generic <see cref="Result{TValue}"/> with an associated value.</summary>
    /// <typeparam name="TValue">The type of the value for the new <see cref="Result{TValue}"/>.</typeparam>
    /// <param name="newValue">The value to associate with the new <see cref="Result{TValue}"/>.</param>
    /// <param name="validator">An optional function to validate the <paramref name="newValue"/>.</param>
    /// <returns>A new <see cref="Result{TValue}"/> containing the <paramref name="newValue"/> if valid and successful, or a failed <see cref="Result{TValue}"/>
    /// if the current <see cref="Result"/> is failed or the <paramref name="validator"/> fails.</returns>
    /// <remarks>This keeps the original <see cref="IReason>"/>s.</remarks>
    public Result<TValue> WithValue<TValue>(TValue newValue, Func<TValue, bool>? validator = null) =>
        IsFailed || (validator != null && !validator(newValue))
            ? ToResult<TValue>()
            : ToResult<TValue>()
                .WithValue(newValue);
}
