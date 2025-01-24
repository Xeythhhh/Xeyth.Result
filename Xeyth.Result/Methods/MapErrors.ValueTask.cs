using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Gets or sets the threshold for the number of <see cref="IError"/>s that will trigger parallel processing during error mapping with <see cref="ValueTask"/>s.
    /// <para>This property controls whether <see cref="IError"/>s will be processed sequentially or in parallel based on the number of <see cref="IError"/>s.
    /// If the total number of <see cref="IError"/>s exceeds the threshold value, parallel processing will be used to map the <see cref="IError"/>s.
    /// Otherwise, the <see cref="IError"/>s will be mapped sequentially. This allows users to optimize performance by processing
    /// smaller <see cref="IError"/> sets sequentially and larger <see cref="IError"/> sets in parallel to take advantage of multi-core processing.</para>
    /// <para>The <see langword="default"/> value of this property is <c>5</c>, meaning that if the <see cref="IError"/> count exceeds 10, parallel processing
    /// will be used. You can configure this value to suit the performance characteristics of your application and the
    /// size of the <see cref="IError"/> sets you expect to process.</para>
    /// <example>The following example demonstrates how to configure the threshold for parallel processing:
    /// <code><see cref="ErrorParallelMappingThreshold"/> = 69;</code></example></summary>
    /// <remarks>This property is intended to be configurable by users to optimize <see cref="IError"/> mapping performance based on the
    /// size of the <see cref="IError"/> set. Users can adjust the threshold value to experiment with performance and choose an
    /// optimal setting for their specific scenario. For small <see cref="IError"/> sets, sequential processing may be more efficient,
    /// while for larger <see cref="IError"/> sets, parallel processing can provide a significant performance boost.
    ///<para>If the processInParallel flag of the method is supplied, this property is ignored.</para></remarks>
    public static int ErrorParallelMappingThreshold { get; set; } = 5;

    /// <summary>Asynchronously maps all <see cref="IError"/> instances in the <see cref="Result"/> using the specified <paramref name="errorMapper"/>.</summary>
    /// <param name="errorMapper">An asynchronous function to transform each <see cref="IError"/> in the result to <typeparamref name="TError"/>.</param>
    /// <param name="processInParallel">Specifies whether to process the errors in parallel or sequentially.
    /// <para>Set to <see langword="true"/> for parallel processing. Use this if you expect most <see cref="ValueTask"/>s to complete asynchronously.</para>
    /// <para>Set to <see langword="false"/> for sequential processing. Use this if you expect most <see cref="ValueTask"/>s to complete synchronously or very fast</para>
    /// <para>The default is automatically computed based on the input. Supplying a value based on your scenario will result in better performance.</para></param>
    /// <returns>A <see cref="ValueTask"/> representing the operation, containing a new <see cref="Result"/> with mapped errors.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="errorMapper"/> is <see langword="null"/>.</exception>
    public async ValueTask<Result> MapErrors<TError>(Func<IError, ValueTask<TError>> errorMapper, bool? processInParallel = null)
        where TError : IError
    {
        if (IsSuccess) return this;
        ArgumentNullException.ThrowIfNull(errorMapper);

        processInParallel ??= Errors.Count > ErrorParallelMappingThreshold;

        return processInParallel.Value
            ? await MapErrorsInParallel(errorMapper).ConfigureAwait(false)
            : await MapErrorsSequentially(errorMapper).ConfigureAwait(false);

        async ValueTask<Result> MapErrorsInParallel(Func<IError, ValueTask<TError>> errorMapper)
        {
            IEnumerable<TError> errorsMappedInParallel =
                await Task.WhenAll(
                    Errors.ConvertAll(async error => await errorMapper(error)
                        .ConfigureAwait(false)))
                    .ConfigureAwait(false);

            return new Result()
                .WithErrors(errorsMappedInParallel.Cast<IError>())
                .WithSuccesses(Successes);
        }

        async ValueTask<Result> MapErrorsSequentially(Func<IError, ValueTask<TError>> errorMapper)
        {
            List<IError> errorsMappedSequentially = [];
            foreach (IError error in Errors)
                errorsMappedSequentially.Add(await errorMapper(error).ConfigureAwait(false));

            return new Result()
                .WithErrors(errorsMappedSequentially)
                .WithSuccesses(Successes);
        }
    }
}
