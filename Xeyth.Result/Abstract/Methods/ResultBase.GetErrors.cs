using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Gets all <see cref="IError"/>s of type <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <returns>A collection with the <typeparamref name="TError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetErrors{TError}(Func{TError, bool})"/>.</remarks>
    public IEnumerable<TError> GetErrors<TError>()
        where TError : IError =>
        GetErrors<TError>(_ => true);

    /// <summary>Gets all <see cref="IError"/>s that match the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/>.</param>
    /// <returns>A collection with the matching <see cref="IError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetErrors{TError}(Func{TError, bool})"/>.</remarks>
    public IEnumerable<IError> GetErrors(Func<IError, bool> predicate) =>
        GetErrors<IError>(predicate);

    /// <summary>Gets all <see cref="IError"/>s of type <typeparamref name="TError"/> that match the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="predicate"/> is <see langword="null"/>.</exception>
    /// <returns>A collection with the matching <typeparamref name="TError"/>s.</returns>
    public IEnumerable<TError> GetErrors<TError>(Func<TError, bool> predicate)
        where TError : IError
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return GetErrorsRecursively(Errors, predicate);
    }

    /// <summary>Gets all <see cref="IError"/>s based on a specific <see cref="Error.Metadata"/> key-value pair.</summary>
    /// <param name="metadataKey">The key to search for in the <see cref="Error.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetErrorsWithMetadata{TError}(string, object)"/>.</remarks>
    public IEnumerable<IError> GetErrorsWithMetadata(string metadataKey, object metadataValue) =>
        GetErrorsWithMetadata<IError>(metadataKey, metadataValue);

    /// <summary>Gets all <typeparamref name="TError"/>s matching a <see cref="Error.Metadata"/> key-value pair.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="metadataKey">The key to search for in the <see cref="Error.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <typeparamref name="TError"/>s.</returns>
    public IEnumerable<TError> GetErrorsWithMetadata<TError>(string metadataKey, object metadataValue)
        where TError : IError =>
        GetErrors<TError>(s =>
            s.Metadata.ContainsKey(metadataKey) &&
            s.Metadata[metadataKey].Equals(metadataValue));

    /// <summary>Recursively checks if a collection of <see cref="IError"/> contains an <see cref="IError"/> of type <typeparamref name="TError"/>
    /// that matches the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="errors">The <see cref="IError"/>s to be checked.</param>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/>.</param>
    /// <returns>A collection with the matching <typeparamref name="TError"/>s.</returns>
    private static IEnumerable<TError> GetErrorsRecursively<TError>(
        IEnumerable<IError> errors,
        Func<TError, bool> predicate)
        where TError : IError
    {
        IEnumerable<TError> result = errors.OfType<TError>().Where(predicate);

        foreach (IError error in errors)
            result = [.. result, .. GetErrorsRecursively(error.Reasons, predicate)];

        return result;
    }
}
