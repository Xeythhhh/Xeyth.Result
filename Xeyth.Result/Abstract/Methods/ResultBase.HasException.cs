using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <see cref="Exception"/>.</summary>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}()"/>.</remarks>
    public bool HasException() =>
        HasException<Exception>();

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException<TException>()
        where TException : Exception =>
        HasException<TException>(out _);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>
    /// and outputs the matching errors.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="errors">A collection with the matching <see cref="ExceptionalError"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(Func{TException, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException<TException>(out IEnumerable<ExceptionalError> errors)
        where TException : Exception =>
        HasException<TException>(_ => true, out errors);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>
    /// that matches the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for the <typeparamref name="TException"/>.</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(Func{TException, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException<TException>(Func<TException, bool> predicate)
        where TException : Exception =>
        HasException(predicate, out _);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>
    /// that matches the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for the <typeparamref name="TException"/>.</param>
    /// <param name="result">A collection with the matching <see cref="ExceptionalError"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(Func{IError, bool}, Func{TException, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException<TException>(Func<TException, bool> predicate, out IEnumerable<ExceptionalError> result)
        where TException : Exception =>
        HasException(_ => true, predicate, out result);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> that matches the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for the <see cref="IError"/>.</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException(Func{IError, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException(Func<IError, bool> predicate) =>
        HasException(predicate, out _);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> that matches the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for the <see cref="IError"/>.</param>
    /// <param name="result">A collection with the matching <see cref="ExceptionalError"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(Func{IError, bool}, Func{TException, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException(Func<IError, bool> predicate, out IEnumerable<ExceptionalError> result) =>
        HasException<Exception>(predicate, _ => true, out result);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>
    /// that matches the specified <paramref name="exceptionPredicate"/> and outputs the matching errors.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="errorPredicate">The condition to evaluate for the <see cref="IError"/></param>
    /// <param name="exceptionPredicate">The condition to evaluate for the <typeparamref name="TException"/>.</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    /// <remarks>This method delegates to <see cref="HasException{TException}(Func{IError, bool}, Func{TException, bool}, out IEnumerable{ExceptionalError})"/>.</remarks>
    public bool HasException<TException>(
        Func<IError, bool> errorPredicate,
        Func<TException, bool> exceptionPredicate)
        where TException : Exception =>
        HasException(errorPredicate, exceptionPredicate, out _);

    /// <summary>Checks if the result contains an <see cref="ExceptionalError"/> containing a <typeparamref name="TException"/>
    /// that matches the specified <paramref name="exceptionPredicate"/> and outputs the matching errors.</summary>
    /// <typeparam name="TException">The type of <see cref="Exception"/> to filter by.</typeparam>
    /// <param name="errorPredicate">The condition to evaluate for the <see cref="IError"/></param>
    /// <param name="exceptionPredicate">The condition to evaluate for the <typeparamref name="TException"/>.</param>
    /// <param name="errors">A collection with the matching <see cref="ExceptionalError"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="ExceptionalError"/> were found.</returns>
    public bool HasException<TException>(
        Func<IError, bool> errorPredicate,
        Func<TException, bool> exceptionPredicate,
        out IEnumerable<ExceptionalError> errors)
        where TException : Exception
    {
        errors = GetExceptions(errorPredicate, exceptionPredicate);
        return errors.Any();
    }
}
