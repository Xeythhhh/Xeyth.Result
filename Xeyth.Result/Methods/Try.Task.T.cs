using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the supplied asynchronous function. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value returned by the function.</typeparam>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.DefaultExceptionalErrorFactory"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a successful or failed <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is <c>null</c>.</exception>
    [OverloadResolutionPriority(1)]
    public static async Task<Result<TValue>> Try<TValue>(Func<Task<TValue>> func, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return Ok(await func().ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            exceptionHandler ??= Error.DefaultExceptionalErrorFactory;
            return Fail(exceptionHandler(exception));
        }
    }

    /// <summary>Attempts to execute the supplied asynchronous function that returns a <see cref="Result{TValue}"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Result{TValue}"/> returned by the function.</typeparam>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.DefaultExceptionalErrorFactory"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a successful or failed <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is <c>null</c>.</exception>
    [OverloadResolutionPriority(1)]
    public static async Task<Result<TValue>> Try<TValue>(Func<Task<Result<TValue>>> func, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(func);

        try
        {
            return await func().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            exceptionHandler ??= Error.DefaultExceptionalErrorFactory;
            return Fail(exceptionHandler(exception));
        }
    }
}
