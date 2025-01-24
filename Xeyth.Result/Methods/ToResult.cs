namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Converts the current <see cref="Result"/> to a generic <see cref="Result{TValue}"/> with no value.</summary>
    /// <typeparam name="TValue">The type of the value for the new <see cref="Result{TValue}"/>.</typeparam>
    /// <returns>A new <see cref="Result{TValue}"/> containing the reasons from the current <see cref="Result"/> and the <see langword="default"/> value for <typeparamref name="TValue"/>.</returns>
    /// <remarks>Use <see cref="WithValue{TValue}(TValue, Func{TValue, bool}?)"/> if you want to provide a <see langword="value"/>.</remarks>
    public Result<TValue> ToResult<TValue>() =>
        Ok<TValue>(default!)
            .WithReasons(Reasons);
}
