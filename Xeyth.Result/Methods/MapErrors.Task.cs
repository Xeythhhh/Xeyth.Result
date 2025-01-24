using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Asynchronously maps all <see cref="IError"/> instances in the <see cref="Result"/> using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">An asynchronous function to transform each <see cref="IError"/> in the result to <typeparamref name="TError"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a new <see cref="Result"/> with mapped errors.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="errorMapper"/> is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result> MapErrors<TError>(Func<IError, Task<TError>> errorMapper)
        where TError : IError
    {
        if (IsSuccess) return this;
        ArgumentNullException.ThrowIfNull(errorMapper);

        TError[] mappedErrors = await Task.WhenAll(
            Errors.ConvertAll(async error => await errorMapper(error).ConfigureAwait(false)))
            .ConfigureAwait(false);

        return new Result()
            .WithErrors(mappedErrors.Cast<IError>())
            .WithSuccesses(Successes);
    }
}
