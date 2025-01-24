using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the supplied asynchronous <paramref name="action"/> returning a <see cref="ValueTask{Result}"/>
    /// If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.ExceptionalFactory"/>.</param>
    /// <returns>A <see cref="ValueTask{Result}"/> representing the asynchronous operation, containing a successful or failed <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is <see langword="null"/>.</exception>
    public static async ValueTask<Result> Try(Func<ValueTask<Result>> action, Func<Exception, IError>? exceptionHandler = null)
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
