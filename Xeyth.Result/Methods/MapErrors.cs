using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Maps all <see cref="IError"/> instances in the <see cref="Result"/> using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">A function to transform each <see cref="IError"/> in the result.</param>
    /// <returns>A new <see cref="Result"/> with mapped errors.</returns>
    public Result MapErrors(Func<IError, IError> errorMapper) =>
        IsSuccess ? this : new Result()
            .WithErrors(Errors.Select(errorMapper))
            .WithSuccesses(Successes);

    /// <summary>Asynchronously maps all <see cref="IError"/> instances in the <see cref="Result"/> using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">An asynchronous function to transform each <see cref="IError"/> in the result.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a new <see cref="Result"/> with mapped errors.</returns>
    public async Task<Result> MapErrorsAsync(Func<IError, Task<IError>> errorMapper)
    {
        if (IsSuccess) return this;
        ArgumentNullException.ThrowIfNull(errorMapper);

        List<IError> mappedErrors = [];
        foreach (IError error in Errors)
            mappedErrors.Add(await errorMapper(error).ConfigureAwait(false));

        return new Result()
            .WithErrors(mappedErrors)
            .WithSuccesses(Successes);
    }
}
