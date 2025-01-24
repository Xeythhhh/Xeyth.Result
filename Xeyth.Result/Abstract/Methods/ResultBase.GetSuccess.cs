using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Gets all <see cref="ISuccess"/>s of type <typeparamref name="TSuccess"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <returns>A collection with the <typeparamref name="TSuccess"/>es.</returns>
    /// <remarks>This method delegates to <see cref="GetSuccesses{TSuccess}(Func{TSuccess, bool})"/>.</remarks>
    public IEnumerable<TSuccess> GetSuccesses<TSuccess>()
        where TSuccess : ISuccess =>
        GetSuccesses<TSuccess>(_ => true);

    /// <summary>Gets all <see cref="ISuccess"/>s that match the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/>.</param>
    /// <returns>A collection with the <see cref="ISuccess"/>es.</returns>
    /// <remarks>This method delegates to <see cref="GetSuccesses{TSuccess}(Func{TSuccess, bool})"/>.</remarks>
    public IEnumerable<ISuccess> GetSuccesses(Func<ISuccess, bool> predicate) =>
        GetSuccesses<ISuccess>(predicate);

    /// <summary>Gets all <see cref="ISuccess"/>s of type <typeparamref name="TSuccess"/> that match the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="predicate"/> is <see langword="null"/>.</exception>
    /// <returns>A collection with the matching <typeparamref name="TSuccess"/>es.</returns>
    public IEnumerable<TSuccess> GetSuccesses<TSuccess>(Func<TSuccess, bool> predicate)
        where TSuccess : ISuccess
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return Successes.OfType<TSuccess>().Where(predicate);
    }

    /// <summary>Gets all <see cref="ISuccess"/>s based on a specific <see cref="Success.Metadata"/> key-value pair.</summary>
    /// <param name="metadataKey">The key to search for in the <see cref="Success.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <see cref="ISuccess"/>es.</returns>
    /// <remarks>This method delegates to <see cref="GetSuccessesWithMetadata{TSuccess}(string, object)"/>.</remarks>
    public IEnumerable<ISuccess> GetSuccessesWithMetadata(string metadataKey, object metadataValue) =>
        GetSuccessesWithMetadata<ISuccess>(metadataKey, metadataValue);

    /// <summary>Gets all <typeparamref name="TSuccess"/>s matching a <see cref="Success.Metadata"/> key-value pair.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <param name="metadataKey">The key to search for in the <see cref="Success.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <typeparamref name="TSuccess"/>es.</returns>
    public IEnumerable<TSuccess> GetSuccessesWithMetadata<TSuccess>(string metadataKey, object metadataValue)
        where TSuccess : ISuccess =>
        GetSuccesses<TSuccess>(s =>
            s.Metadata.ContainsKey(metadataKey) &&
            s.Metadata[metadataKey].Equals(metadataValue));
}
