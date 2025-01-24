using Xeyth.Result.Base;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Maps all <see cref="IError"/> instances in <see cref="ResultBase.Errors"/> using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">A function to transform each <see cref="IError"/> in the result to <typeparamref name="TError"/>.</param>
    /// <returns>A new <see cref="Result"/> with mapped errors.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="errorMapper"/> is <see langword="null"/>.</exception>
    public Result MapErrors<TError>(Func<IError, TError> errorMapper)
        where TError : IError
    {
        if (IsSuccess) return this;
        ArgumentNullException.ThrowIfNull(errorMapper);

        return new Result()
            .WithErrors(Errors.Select(errorMapper).Cast<IError>())
            .WithSuccesses(Successes);
    }
}
