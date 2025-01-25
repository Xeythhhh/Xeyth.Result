using Xeyth.Result.Base;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Reasons;

/// <summary>Represents an <see cref="IError"/> that causes a result to fail. Errors can include <see cref="Metadata"/> and nested <see cref="Reasons"/> for additional context.</summary>
public class Error : IError
{
    /// <summary>Default factory</summary>
    /// <exception cref="ArgumentNullException">Thrown when the message is <see langword="null"/></exception>
    private static Func<string, IError> _factory = message =>
    {
        ArgumentNullException.ThrowIfNull(message);
        return new Error(message);
    };

    /// <summary>The factory function used to generate an <see cref="IError"/>.
    /// If not explicitly set, it defaults to creating an instance of <see cref="Error"/>.</summary>
    /// <exception cref="ArgumentNullException">Throw when trying to set and <paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="ArgumentNullException">Thrown by the default factory when the message is <see langword="null"/></exception>
    /// <remarks>When modifying this factory, <see langword="lock"/> the <see cref="FactoryLock"/>.</remarks>
    public static Func<string, IError> Factory
    {
        get
        {
            lock (FactoryLock)
            {
                return _factory;
            }
        }

        set
        {
            lock (FactoryLock)
            {
                _factory = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }

    /// <summary>A synchronization object used to control access to the <see cref="Factory"/> property.</summary>
    /// <remarks>When modifying the <see cref="Factory"/> property, use this lock to ensure thread safety.
    /// <para>You can omit locking if this happens during startup before the factory can be used.</para></remarks>
    public static Lock FactoryLock { get; } = new();

    /// <summary>The message describing the <see cref="Error"/>.</summary>
    public string Message { get; protected set; } = "Missing Error Message.";

    /// <summary>Metadata providing additional context about the <see cref="Error"/>.</summary>
    public Dictionary<string, object> Metadata { get; }

    /// <summary>The underlying <see cref="IReason"/>s contributing to this <see cref="Error"/>.</summary>
    public List<IError> Reasons { get; }

    /// <summary>Initializes a new instance of the <see cref="Error"/> class.</summary>
    /// <returns>A new instance of <see cref="Error"/>.</returns>
    protected Error()
    {
        Metadata = [];
        Reasons = [];
    }

    /// <summary>Initializes a new instance of the <see cref="Error"/> class with a specific <see cref="Message"/>.</summary>
    /// <param name="message">The error message.</param>
    /// <returns>A new instance of <see cref="Error"/>.</returns>
    public Error(string message) : this() => Message = message;

    /// <summary>Initializes a new instance of the <see cref="Error"/> class with a <see cref="Message"/> and a root cause.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="causedBy">The root cause of the <see cref="Error"/>.</param>
    /// <exception cref="ArgumentNullException">Throw when <paramref name="causedBy"/> is <see langword="null"/></exception>
    /// <returns>A new instance of <see cref="Error"/>.</returns>
    public Error(string message, IError causedBy) : this(message)
    {
        ArgumentNullException.ThrowIfNull(causedBy);
        Reasons.Add(causedBy);
    }

    /// <summary>Adds a message as a root cause to this <see cref="Error"/>.</summary>
    /// <param name="message">The error message to add as a root cause.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(string)"/>.</remarks>
    public Error CausedBy(string message) => CausedBy<Error>(message);

    /// <summary>Adds a message as a root cause to this <see cref="IError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="message">The error message to add as a root cause.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IError)"/>.</remarks>
    public TError CausedBy<TError>(string message)
        where TError : Error => CausedBy<TError>(Factory(message));

    /// <summary>Adds a root cause to this <see cref="Error"/>.</summary>
    /// <param name="error">The root cause to add.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IError)"/>.</remarks>
    public Error CausedBy(IError error) => CausedBy<Error>(error);

    /// <summary>Adds an exception as a root cause to this <see cref="Error"/>.</summary>
    /// <param name="exception">The exception to add as a root cause.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(Exception)"/>.</remarks>
    public Error CausedBy(Exception exception) => CausedBy<Error>(exception);

    /// <summary>Adds an <see cref="Exception"/> as a root cause to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="exception">The exception to add as a root cause.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IError)"/>.</remarks>
    public TError CausedBy<TError>(Exception exception)
        where TError : Error => CausedBy<TError>(new ExceptionalError(exception));

    /// <summary>Adds a root cause to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="error">The root cause to add.</param>
    /// <exception cref="ArgumentNullException">Throw when <paramref name="errors"/> is <see langword="null"/></exception>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(IError error)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);
        Reasons.Add(error);
        return (TError)this;
    }

    /// <summary>Adds multiple messages as root causes to this <see cref="Error"/>.</summary>
    /// <param name="errors">The error messages to add as root causes.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IEnumerable{string})"/>.</remarks>
    public Error CausedBy(IEnumerable<string> errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple messages as root causes to this <see cref="Error"/>.</summary>
    /// <param name="errors">The error messages to add as root causes.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IEnumerable{string})"/>.</remarks>
    public Error CausedBy(params string[] errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple messages as root causes to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="errors">The error messages to add as root causes.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IEnumerable{IError})"/>.</remarks>
    public TError CausedBy<TError>(IEnumerable<string> errors)
        where TError : Error => CausedBy<TError>(errors.Select(Factory));

    /// <summary>Adds multiple <see cref="IError"/>s as root causes to this <see cref="Error"/>.</summary>
    /// <param name="errors">The errors to add as root causes.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IEnumerable{IError})"/>.</remarks>
    public Error CausedBy(IEnumerable<IError> errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple <see cref="IError"/>s as root causes to this <see cref="Error"/>.</summary>
    /// <param name="errors">The errors to add as root causes.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="CausedBy{TError}(IEnumerable{IError})"/>.</remarks>
    public Error CausedBy(params IError[] errors) => CausedBy<Error>(errors);

    /// <summary>Adds multiple <see cref="IError"/> as root causes to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="errors">The errors to add as root causes.</param>
    /// <exception cref="ArgumentNullException">Throw when <paramref name="errors"/> is <see langword="null"/></exception>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError CausedBy<TError>(IEnumerable<IError> errors)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(errors);
        Reasons.AddRange(errors);
        return (TError)this;
    }

    /// <summary>Adds <see cref="Metadata"/> to this <see cref="Error"/>.</summary>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="WithMetadata{TError}(string, object)"/>.</remarks>
    public Error WithMetadata(string metadataName, object metadataValue) => WithMetadata<Error>(metadataName, metadataValue);

    /// <summary>Adds multiple <see cref="Metadata"/> items to this <see cref="Error"/>.</summary>
    /// <param name="metadata">The metadata to add.</param>
    /// <returns>The current instance as <see cref="Error"/>.</returns>
    /// <remarks>This method delegates to <see cref="WithMetadata{TError}(Dictionary{string, object})"/>.</remarks>
    public Error WithMetadata(Dictionary<string, object> metadata) => WithMetadata<Error>(metadata);

    /// <summary>Adds <see cref="Metadata"/> to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError WithMetadata<TError>(string metadataName, object metadataValue)
        where TError : Error
    {
        Metadata[metadataName] = metadataValue;
        return (TError)this;
    }

    /// <summary>Adds multiple <see cref="Metadata"/> items to this <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of the current instance.</typeparam>
    /// <param name="metadata">The metadata to add.</param>
    /// <returns>The current instance as <typeparamref name="TError"/>.</returns>
    public TError WithMetadata<TError>(Dictionary<string, object> metadata)
        where TError : Error
    {
        foreach ((string key, object value) in metadata)
        {
            Metadata[key] = value;
        }

        return (TError)this;
    }

    /// <summary>Returns a string representation of this <see cref="Error"/>, including its
    /// <list type="bullet">
    /// <item><see cref="Type"/></item>
    /// <item><see cref="Message"/></item>
    /// <item><see cref="Metadata"/></item>
    /// <item><see cref="Reasons"/></item></list></summary>
    /// <returns>A string describing this <see cref="Error"/>.</returns>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .WithInfo(nameof(Reasons), ResultBase.ReasonsToString(Reasons))
            .Build();
}
