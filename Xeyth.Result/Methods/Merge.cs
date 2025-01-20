using Xeyth.Result.Base;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Merges multiple <see cref="Result"/> objects into a single <see cref="Result"/>.</summary>
    /// <param name="results">The <see cref="ResultBase"/> objects to merge.</param>
    /// <returns>A merged <see cref="Result"/> containing all reasons from the provided <paramref name="results"/>.</returns>
    public static Result Merge(params ResultBase[] results) =>
        MergeInternal(results);

    /// <summary>Merges multiple <see cref="Result"/> objects from an <see cref="IEnumerable{T}"/> into a single <see cref="Result"/>.</summary>
    /// <param name="results">The collection of <see cref="ResultBase"/> objects to merge.</param>
    /// <returns>A merged <see cref="Result"/> containing all reasons from the provided <paramref name="results"/>.</returns>
    public static Result Merge(IEnumerable<ResultBase> results) =>
        MergeInternal(results);

    /// <summary>Merges multiple <see cref="Result{TValue}"/> objects into a single <see cref="Result{TValue}"/> containing a list of values.</summary>
    /// <typeparam name="TValue">The type of the values contained in the <see cref="Result{TValue}"/> objects.</typeparam>
    /// <param name="results">The <see cref="Result{TValue}"/> objects to merge.</param>
    /// <returns>A merged <see cref="Result{TValue}"/> with a list of values and reasons from the provided <paramref name="results"/>.</returns>
    public static Result<IEnumerable<TValue>> Merge<TValue>(params Result<TValue>[] results) =>
        MergeInternalWithValue(results);

    /// <summary>Merges multiple <see cref="Result{TValue}"/> objects from an <see cref="IEnumerable{T}"/> into a single <see cref="Result{TValue}"/> containing a list of values.</summary>
    /// <typeparam name="TValue">The type of the values contained in the <see cref="Result{TValue}"/> objects.</typeparam>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> objects to merge.</param>
    /// <returns>A merged <see cref="Result{TValue}"/> with a list of values and reasons from the provided <paramref name="results"/>.</returns>
    public static Result<IEnumerable<TValue>> Merge<TValue>(IEnumerable<Result<TValue>> results) =>
        MergeInternalWithValue(results);

    /// <summary>Internally merges multiple <see cref="ResultBase"/> objects into a single <see cref="Result"/>.</summary>
    internal static Result MergeInternal(IEnumerable<ResultBase> results) =>
        Ok().WithReasons(results.SelectMany(result => result.Reasons));

    /// <summary>Internally merges multiple <see cref="Result{TValue}"/> objects into a single <see cref="Result{TValue}"/> containing a list of values.</summary>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="results"/> collection is <see langword="null"/>.</exception>
    internal static Result<IEnumerable<TValue>> MergeInternalWithValue<TValue>(
        IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        List<Result<TValue>> resultList = results.ToList();

        Result<IEnumerable<TValue>> finalResult = Ok<IEnumerable<TValue>>([])
            .WithReasons(resultList.SelectMany(result => result.Reasons));

        if (finalResult.IsSuccess)
            finalResult.WithValue(resultList.ConvertAll(r => r.Value));

        return finalResult;
    }
}