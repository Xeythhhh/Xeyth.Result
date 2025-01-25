using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

/// <summary>Provides a generic base implementation for result objects, extending <see cref="ResultBase"/>.
/// This class supports operations for adding <see cref="IReason"/>s, including <see cref="ISuccess"/> and <see cref="IError"/>, to the result.</summary>
/// <typeparam name="TResult">The specific result type that inherits from this base class.
/// This ensures fluent APIs return the correct type for chaining methods.</typeparam>
public abstract partial class ResultBase<TResult> : ResultBase
    where TResult : ResultBase<TResult>
{
    /// <summary>Adds a <see cref="IReason"/> (<see cref="ISuccess"/> or <see cref="IError"/>) to the <typeparamref name="TResult"/>.</summary>
    /// <param name="reason">The <see cref="IReason"/> instance to add to the <typeparamref name="TResult"/>.
    /// This can represent a <see cref="ISuccess"/> or an <see cref="IError"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="reason"/> is <see langword="null"/>.</exception>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    public TResult WithReason(IReason reason)
    {
        ArgumentNullException.ThrowIfNull(reason);

        Reasons.Add(reason);

        return (TResult)this;
    }

    /// <summary>Adds multiple <see cref="IReason"/>s (<see cref="ISuccess"/> or <see cref="IError"/>) to the <typeparamref name="TResult"/>.</summary>
    /// <param name="reasons">A collection of <see cref="IReason"/> instances to add to the <typeparamref name="TResult"/>.
    /// Each item can represent a <see cref="ISuccess"/> or an <see cref="IError"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="reasons"/> collection is <see langword="null"/>.</exception>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    public TResult WithReasons(IEnumerable<IReason> reasons)
    {
        ArgumentNullException.ThrowIfNull(reasons);

        foreach (IReason reason in reasons)
            Reasons.Add(reason);

        return (TResult)this;
    }

    public override string ToString()
    {
        string stateString = IsSuccess ? "true" : "false";
        return $"{base.ToString()}, Successful:{stateString}";
    }
}
