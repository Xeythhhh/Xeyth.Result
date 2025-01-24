using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the supplied asynchronous function. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value returned by the function.</typeparam>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing a successful or failed <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static async ValueTask<Result<TValue>> Try<TValue>(
        Func<ValueTask<TValue>> action,
        Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            return Ok(await action().ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            exceptionHandler ??= exception => new ExceptionalError(exception);
            return Fail(exceptionHandler(exception));
        }
    }

    /// <summary>Attempts to execute the supplied asynchronous function that returns a <see cref="Result{TValue}"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Result{TValue}"/> returned by the function.</typeparam>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing a successful or failed <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static async ValueTask<Result<TValue>> Try<TValue>(
        Func<ValueTask<Result<TValue>>> action,
        Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            return await action().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            exceptionHandler ??= exception => new ExceptionalError(exception);
            return Fail(exceptionHandler(exception));
        }
    }
}
