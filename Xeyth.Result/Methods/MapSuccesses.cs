using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Maps all <see cref="ISuccess"/> instances in the <see cref="Result"/> using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">A function to transform each <see cref="ISuccess"/> in the result.</param>
    /// <returns>A new <see cref="Result"/> with mapped successes.</returns>
    public Result MapSuccesses(Func<ISuccess, ISuccess> successMapper) =>
        new Result()
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper));

    /// <summary>Asynchronously maps all <see cref="ISuccess"/> instances in the <see cref="Result"/> using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">An asynchronous function to transform each <see cref="ISuccess"/> in the result.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a new <see cref="Result"/> with mapped successes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="successMapper"/> is <see langword="null"/>.</exception>
    public async Task<Result> MapSuccessesAsync(Func<ISuccess, Task<ISuccess>> successMapper)
    {
        ArgumentNullException.ThrowIfNull(successMapper);

        List<ISuccess> mappedSuccesses = [];
        foreach (ISuccess success in Successes)
            mappedSuccesses.Add(await successMapper(success).ConfigureAwait(false));

        return new Result()
            .WithErrors(Errors)
            .WithSuccesses(mappedSuccesses);
    }
}
