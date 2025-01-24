using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Asynchronously maps all <see cref="ISuccess"/> instances in the <see cref="Result{TValue}"/> using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">An asynchronous function to transform each <see cref="ISuccess"/> in the result to <typeparamref name="TSuccess"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a new <see cref="Result{TValue}"/> with mapped successes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="successMapper"/> is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result<TValue>> MapSuccesses<TSuccess>(Func<ISuccess, Task<TSuccess>> successMapper)
        where TSuccess : ISuccess
    {
        ArgumentNullException.ThrowIfNull(successMapper);

        TSuccess[] mappedSuccesses = await Task.WhenAll(
            Successes.ConvertAll(async success => await successMapper(success).ConfigureAwait(false)))
            .ConfigureAwait(false);

        return new Result<TValue>()
            .WithErrors(Errors)
            .WithSuccesses(mappedSuccesses.Cast<ISuccess>())
            .WithValue(GetLastSuccessfulValueOrDefault());
    }
}
