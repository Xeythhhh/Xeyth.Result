using Xeyth.Result.Abstract;
using Xeyth.Result.Exceptions;

namespace Xeyth.Result;

/// <summary>Represents a <see cref="Result{TValue}"/> that may contain a <see cref="Value"/>. A failed result does not have a value.</summary>
/// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
public interface IResult<out TValue> : IResultBase
{
    /// <summary>Gets the value of the <see cref="Result{TValue}"/>.</summary>
    /// <exception cref="FailedResultValueAccessException">Thrown if the result is failed.</exception>
    TValue Value { get; }
}
