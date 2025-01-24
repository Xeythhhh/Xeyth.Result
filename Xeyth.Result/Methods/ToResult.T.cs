using Xeyth.Result.Reasons.Abstract;
using Xeyth.Result.Base;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Converts the current generic <see cref="Result{TValue}"/> to a non-generic <see cref="Result"/> with no value.</summary>
    /// <returns>A new <see cref="Result"/> containing the <see cref="IReason"/>s from the current <see cref="Result{TValue}"/>.</returns>
    public Result ToResult() => Result.Ok().WithReasons(Reasons);

    /// <summary>Converts the current generic <see cref="Result{TValue}"/> to a generic <see cref="Result{TNewValue}"/>
    /// with a new value of type <typeparamref name="TNewValue"/> using <paramref name="valueConverter"/>.</summary>
    /// <typeparam name="TNewValue">The type of the value in the resulting <see cref="Result{TNewValue}"/>.</typeparam>
    /// <param name="valueConverter">A function to transform the current value of type <typeparamref name="TValue"/> into a value of type <typeparamref name="TNewValue"/>.
    /// <para>If the result is failed, the <paramref name="valueConverter"/> will not be invoked,
    /// and the value in the resulting <see cref="Result{TNewValue}"/> will be the <see langword="default"/> for <typeparamref name="TNewValue"/>.</para></param>
    /// <returns>A new <see cref="Result{TNewValue}"/> with the transformed value and original <see cref="IReason"/>s.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="valueConverter"/> is <see langword="null"/> when <see cref="ResultBase.IsSuccess"/> is <see langword="true"/>.</exception>
    public Result<TNewValue> ToResult<TNewValue>(Func<TValue, TNewValue> valueConverter = null!)
    {
        if (IsSuccess && valueConverter is null)
            throw new ArgumentException("If result is success then valueConverter should not be null");

        return Result.Ok(IsFailed ? default! : valueConverter(Value))
            .WithReasons(Reasons);
    }
}
