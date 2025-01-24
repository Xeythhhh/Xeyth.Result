using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the specified asynchronous <paramref name="action"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a successful or failed <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> Try(Func<Task> action, Func<Exception, IError>? exceptionHandler = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            await action().ConfigureAwait(false);
            return Ok();
        }
        catch (Exception exception)
        {
            exceptionHandler ??= exception => new ExceptionalError(exception);
            return Fail(exceptionHandler(exception));
        }
    }

    /// <summary>Attempts to execute the specified asynchronous <paramref name="action"/> that returns a <see cref="Result"/>. If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a successful or failed <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public static async Task<Result> Try(Func<Task<Result>> action, Func<Exception, IError>? exceptionHandler = null)
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
