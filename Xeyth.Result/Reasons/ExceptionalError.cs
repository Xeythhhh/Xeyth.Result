using Xeyth.Result.Base;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Reasons;

/// <summary>Represents an <see cref="IError"/> that includes an associated <see cref="System.Exception"/> for additional context.</summary>
public class ExceptionalError(string message, Exception exception) : Error(message), IExceptionalError
{
    /// <summary>The <see cref="System.Exception"/> associated with this <see cref="IError"/>.</summary>
    public Exception Exception { get; } = exception;

    /// <summary>Initializes a new instance of the <see cref="ExceptionalError"/> class with a specific <see cref="System.Exception"/>.</summary>
    /// <param name="exception">The <see cref="System.Exception"/>to associate with this <see cref="IError"/>.</param>
    public ExceptionalError(Exception exception)
        : this(exception.Message, exception)
    { }

    /// <summary>Returns a string representation of this <see cref="IError"/>, including the <see cref="System.Exception"/> details.</summary>
    /// <returns>A string describing this <see cref="IError"/>.</returns>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ReasonsToString(Reasons))
            .WithInfo(nameof(Exception), Exception.ToString())
            .Build();
}
