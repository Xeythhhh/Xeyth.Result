using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Reasons;

/// <summary>Represents an <see cref="ISuccess"/> that does not cause a result to fail.</summary>
public class Success : ISuccess
{
    /// <summary>Default factory</summary>
    /// <exception cref="ArgumentNullException">Thrown when the message is <see langword="null"/></exception>
    private static Func<string, ISuccess> _factory = message => new Success(message);

    /// <summary>The factory function used to generate an <see cref="ISuccess"/>.
    /// If not explicitly set, it defaults to creating an instance of <see cref="Success"/>.</summary>
    /// <exception cref="ArgumentNullException">Throw when trying to set and <paramref name="value"/> is <see langword="null"/></exception>
    /// <exception cref="ArgumentNullException">Thrown by the default factory when the message is <see langword="null"/></exception>
    /// <remarks>When modifying this factory, <see langword="lock"/> the <see cref="FactoryLock"/>.</remarks>
    public static Func<string, ISuccess> Factory
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

    /// <summary>The message describing the success.</summary>
    public string Message { get; protected set; } = "Missing Success Message.";

    /// <summary>Metadata providing additional context about the success.</summary>
    public Dictionary<string, object> Metadata { get; }

    /// <summary>Initializes a new instance of the <see cref="Success"/> class.</summary>
    protected Success() => Metadata = [];

    /// <summary>Initializes a new instance of the <see cref="Success"/> class with a specific message.</summary>
    /// <param name="message">The success message.</param>
    public Success(string message) : this() => Message = message;

    /// <summary>Adds metadata to this success and returns the current instance as <see cref="Success"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TSuccess}(string, object)"/> method.</summary>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    public Success WithMetadata(string metadataName, object metadataValue) => WithMetadata<Success>(metadataName, metadataValue);

    /// <summary>Adds multiple metadata items to this success and returns the current instance as <see cref="Success"/>.
    /// This method delegates to the generic <see cref="WithMetadata{TSuccess}(Dictionary{string, object})"/> method.</summary>
    /// <param name="metadata">The metadata to add.</param>
    public Success WithMetadata(Dictionary<string, object> metadata) => WithMetadata<Success>(metadata);

    /// <summary>Adds metadata to this success and returns the current instance.</summary>
    /// <typeparam name="TSuccess">The type of the current instance.</typeparam>
    /// <param name="metadataName">The name of the metadata.</param>
    /// <param name="metadataValue">The value of the metadata.</param>
    /// <returns>The current instance as <typeparamref name="TSuccess"/>.</returns>
    public TSuccess WithMetadata<TSuccess>(string metadataName, object metadataValue) where TSuccess : Success
    {
        Metadata[metadataName] = metadataValue;
        return (TSuccess)this;
    }

    /// <summary>Adds multiple metadata items to this success and returns the current instance.</summary>
    /// <typeparam name="TSuccess">The type of the current instance.</typeparam>
    /// <param name="metadata">The metadata to add.</param>
    /// <returns>The current instance as <typeparamref name="TSuccess"/>.</returns>
    public TSuccess WithMetadata<TSuccess>(Dictionary<string, object> metadata) where TSuccess : Success
    {
        foreach ((string key, object value) in metadata)
        {
            Metadata[key] = value;
        }

        return (TSuccess)this;
    }

    /// <summary>Returns a string representation of this success, including its type, message, and metadata.</summary>
    public override string ToString() =>
        new ReasonStringBuilder()
            .WithReasonType(GetType())
            .WithInfo(nameof(Message), Message)
            .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
            .Build();
}
