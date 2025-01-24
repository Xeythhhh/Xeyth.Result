using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Gets all <see cref="ExceptionalError"/>s containing <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    public IEnumerable<ExceptionalError> GetExceptions<TException>()
        where TException : Exception =>
        GetExceptionsRecursively<TException>(Errors);

    /// <summary>Gets all <see cref="ExceptionalError"/>s containing <typeparamref name="TException"/> matching the provided <paramref name="predicate"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <typeparamref name="TException"/>.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetExceptions{TException}(Func{IError, bool}, Func{TException, bool})"/>.</remarks>
    public IEnumerable<ExceptionalError> GetExceptions<TException>(Func<TException, bool> predicate)
        where TException : Exception =>
        GetExceptions(_ => true, predicate);

    /// <summary>Gets all <see cref="ExceptionalError"/>s matching the provided <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="ExceptionalError"/>.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetExceptions{TException}(Func{IError, bool}, Func{TException, bool})"/>.</remarks>
    public IEnumerable<ExceptionalError> GetExceptions(Func<IError, bool> predicate) =>
        GetExceptions<Exception>(predicate, _ => true);

    /// <summary>Gets all <see cref="ExceptionalError"/>s matching the provided <paramref name="errorPredicate"/> and <paramref name="exceptionPredicate"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="errorPredicate">The condition to evaluate for each <see cref="ExceptionalError"/>.</param>
    /// <param name="exceptionPredicate">The condition to evaluate for each <typeparamref name="TException"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="errorPredicate"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="exceptionPredicate"/> is <see langword="null"/>.</exception>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    public IEnumerable<ExceptionalError> GetExceptions<TException>(Func<IError, bool> errorPredicate, Func<TException, bool> exceptionPredicate)
        where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(errorPredicate);
        ArgumentNullException.ThrowIfNull(exceptionPredicate);
        return GetExceptionsRecursively(Errors, errorPredicate, exceptionPredicate);
    }

    /// <summary>Gets all <see cref="ExceptionalError"/>s based on a specific <see cref="Error.Metadata"/> key-value pair.</summary>
    /// <param name="metadataKey">The key to search for in the <see cref="Error.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    /// <remarks>This method delegates to <see cref="GetExceptionsWithMetadata{TException}(string, object)"/>.</remarks>
    public IEnumerable<ExceptionalError> GetExceptionsWithMetadata(string metadataKey, object metadataValue) =>
        GetExceptionsWithMetadata<Exception>(metadataKey, metadataValue);

    /// <summary>Gets all <see cref="ExceptionalError"/>s containing a specific <typeparamref name="TException"/> and matching a <see cref="Error.Metadata"/> key-value pair.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="metadataKey">The key to search for in the <see cref="Error.Metadata"/> dictionary.</param>
    /// <param name="metadataValue">The value associated with the <paramref name="metadataKey"/> to match.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    public IEnumerable<ExceptionalError> GetExceptionsWithMetadata<TException>(string metadataKey, object metadataValue)
        where TException : Exception =>
        GetExceptions<TException>(e =>
            e.Metadata.ContainsKey(metadataKey) &&
            e.Metadata[metadataKey].Equals(metadataValue), _ => true);

    /// <summary>Recursively checks if a collection of <see cref="IError"/> contains an <see cref="ExceptionalError"/> containing <typeparamref name="TException"/>
    /// that matches the <paramref name="errorPredicate"/> and <paramref name="exceptionPredicate"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="errors">The <see cref="IError"/>s to be checked.</param>
    /// <param name="errorPredicate">The condition to evaluate for each <see cref="ExceptionalError"/>.</param>
    /// <param name="exceptionPredicate">The condition to evaluate for each <typeparamref name="TException"/>.</param>
    /// <returns>A collection with the matching <see cref="ExceptionalError"/>s.</returns>
    private static IEnumerable<ExceptionalError> GetExceptionsRecursively<TException>(
        IEnumerable<IError> errors,
        Func<IError, bool>? errorPredicate = null,
        Func<TException, bool>? exceptionPredicate = null)
        where TException : Exception
    {
        IEnumerable<ExceptionalError> result = errors.OfType<ExceptionalError>().Where(e
            => (errorPredicate is null || errorPredicate(e))
            && e.Exception is TException exception
            && (exceptionPredicate is null || exceptionPredicate(exception)));

        foreach (IError error in errors)
            result = [.. result, .. GetExceptionsRecursively(error.Reasons, errorPredicate, exceptionPredicate)];

        return result;
    }
}
