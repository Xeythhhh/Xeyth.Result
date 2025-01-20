using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Attempts to execute the supplied asynchronous <paramref name="func"/> returning a <see cref="ValueTask{Result}"/>
    /// If an exception is thrown, the <paramref name="exceptionHandler"/> transforms the exception into an <see cref="IError"/>.</summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="exceptionHandler">A function to handle exceptions and transform them into an <see cref="IError"/>. Defaults to <see cref="Error.DefaultExceptionalErrorFactory"/>.</param>
    /// <returns>A <see cref="ValueTask{Result}"/> representing the asynchronous operation, containing a successful or failed <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="func"/> is <see langword="null"/>.</exception>
    public static async ValueTask<Result> Try(Func<ValueTask<Result>> func, Func<Exception, IError>? exceptionHandler = null)
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
