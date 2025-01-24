using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains any <see cref="IError"/>.</summary>
    /// <returns><see langword="true"/> if any errors were found.</returns>
    public bool HasError() => Errors.Count > 0;

    /// <summary>Checks if the result contains any <see cref="IError"/> of type <typeparamref name="TError"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <returns><see langword="true"/> if any <typeparamref name="TError"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasError{TError}(out IEnumerable{TError})"/>.</remarks>
    public bool HasError<TError>()
        where TError : IError =>
        HasError<TError>(out _);

    /// <summary>Checks if the result contains any <see cref="IError"/> of type <typeparamref name="TError"/>
    /// and outputs the matching <paramref name="errors"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="errors">A collection with the matching <typeparamref name="TError"/>s</param>
    /// <returns><see langword="true"/> if any <typeparamref name="TError"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasError{TError}(Func{TError, bool}, out IEnumerable{TError})"/>.</remarks>
    public bool HasError<TError>(out IEnumerable<TError> errors)
        where TError : IError =>
        HasError(_ => true, out errors);

    /// <summary>Checks if the result contains any <see cref="IError"/> of type <typeparamref name="TError"/>
    /// that matches the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/> of type <typeparamref name="TError"/>.</param>
    /// <returns><see langword="true"/> if any matching <typeparamref name="TError"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasError{TError}(Func{TError, bool}, out IEnumerable{TError})"/>.</remarks>
    public bool HasError<TError>(Func<TError, bool> predicate)
        where TError : IError =>
        HasError(predicate, out _);

    /// <summary>Checks if the result contains any <see cref="IError"/> that matches the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/>.</param>
    /// <returns><see langword="true"/> if any matching <see cref="IError"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasError(Func{IError, bool}, out IEnumerable{IError})"/>.</remarks>
    public bool HasError(Func<IError, bool> predicate) =>
        HasError(predicate, out _);

    /// <summary>Checks if the result contains any <see cref="IError"/> that matches the specified <paramref name="predicate"/>
    /// and outputs the matching <see cref="IError"/>s.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/>.</param>
    /// <param name="errors">A collection with the matching <see cref="IError"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="IError"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasError{TError}(Func{TError, bool}, out IEnumerable{TError})"/>.</remarks>
    public bool HasError(Func<IError, bool> predicate, out IEnumerable<IError> errors) =>
        HasError<IError>(predicate, out errors);

    /// <summary>Checks if the result contains any <see cref="IError"/> of type <typeparamref name="TError"/> that matches the specified <paramref name="predicate"/>
    /// and outputs the matching <paramref name="errors"/>.</summary>
    /// <typeparam name="TError">The type of <see cref="IError"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="IError"/> of type <typeparamref name="TError"/>.</param>
    /// <param name="errors">A collection with the matching <typeparamref name="TError"/>s</param>
    /// <returns><see langword="true"/> if any matching <typeparamref name="TError"/>s were found.</returns>
    public bool HasError<TError>(Func<TError, bool> predicate, out IEnumerable<TError> errors)
        where TError : IError
    {
        errors = GetErrors(predicate);
        return errors.Any();
    }
}
