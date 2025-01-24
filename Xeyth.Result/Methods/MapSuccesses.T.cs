using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Maps all <see cref="ISuccess"/> instances in the <see cref="Result{TValue}"/> using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">A function to transform each <see cref="ISuccess"/> in the result to <typeparamref name="TSuccess"/>.</param>
    /// <returns>A new <see cref="Result{TValue}"/> with mapped successes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="successMapper"/> is <see langword="null"/>.</exception>
    public Result<TValue> MapSuccesses<TSuccess>(Func<ISuccess, TSuccess> successMapper)
        where TSuccess : ISuccess
    {
        ArgumentNullException.ThrowIfNull(successMapper);

        return new Result<TValue>()
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper).Cast<ISuccess>())
            .WithValue(GetLastSuccessfulValueOrDefault());
    }
}