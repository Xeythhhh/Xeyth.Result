using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Gets or sets the threshold for the number of <see cref="ISuccess"/> instances that will trigger parallel processing during success mapping with <see cref="ValueTask"/>s.
    /// <para>This property controls whether <see cref="ISuccess"/> instances will be processed sequentially or in parallel based on the number of <see cref="ISuccess"/> instances.
    /// If the total number of <see cref="ISuccess"/> instances exceeds the threshold value, parallel processing will be used to map the <see cref="ISuccess"/> instances.
    /// Otherwise, the <see cref="ISuccess"/> instances will be mapped sequentially. This allows users to optimize performance by processing
    /// smaller <see cref="ISuccess"/> sets sequentially and larger <see cref="ISuccess"/> sets in parallel to take advantage of multi-core processing.</para>
    /// <para>The <see langword="default"/> value of this property is <c>5</c>, meaning that if the <see cref="ISuccess"/> count exceeds 5, parallel processing
    /// will be used. You can configure this value to suit the performance characteristics of your application and the
    /// size of the <see cref="ISuccess"/> sets you expect to process.</para>
    /// <example>The following example demonstrates how to configure the threshold for parallel processing:
    /// <code><see cref="SuccessMappingParallelThreshold"/> = 10;</code></example></summary>
    /// <remarks>This property is intended to be configurable by users to optimize <see cref="ISuccess"/> mapping performance based on the
    /// size of the <see cref="ISuccess"/> set. Users can adjust the threshold value to experiment with performance and choose an
    /// optimal setting for their specific scenario. For small <see cref="ISuccess"/> sets, sequential processing may be more efficient,
    /// while for larger <see cref="ISuccess"/> sets, parallel processing can provide a significant performance boost.
    ///<para>If the processInParallel flag of the method is supplied, this property is ignored.</para></remarks>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter
    public static int SuccessMappingParallelThreshold { get; set; } = 5;
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter

    /// <summary>Asynchronously maps all <see cref="ISuccess"/> instances in the <see cref="Result{TValue}"/> using the specified <paramref name="successMapper"/>.</summary>
    /// <param name="successMapper">An asynchronous function to transform each <see cref="ISuccess"/> in the result to <typeparamref name="TSuccess"/>.</param>
    /// <param name="processInParallel">Specifies whether to process the successes in parallel or sequentially.
    /// <para>Set to <see langword="true"/> for parallel processing. Use this if you expect most <see cref="ValueTask"/>s to complete asynchronously.</para>
    /// <para>Set to <see langword="false"/> for sequential processing. Use this if you expect most <see cref="ValueTask"/>s to complete synchronously or very fast.</para>
    /// <para>The default is automatically computed based on the input. Supplying a value based on your scenario will result in better performance.</para></param>
    /// <returns>A <see cref="ValueTask"/> representing the operation, containing a new <see cref="Result{TValue}"/> with mapped successes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="successMapper"/> is <see langword="null"/>.</exception>
    public async ValueTask<Result<TValue>> MapSuccesses<TSuccess>(Func<ISuccess, ValueTask<TSuccess>> successMapper, bool? processInParallel = null)
        where TSuccess : ISuccess
    {
        ArgumentNullException.ThrowIfNull(successMapper);

        processInParallel ??= Successes.Count > SuccessMappingParallelThreshold;

        return processInParallel.Value
            ? await MapSuccessesInParallel(successMapper).ConfigureAwait(false)
            : await MapSuccessesSequentially(successMapper).ConfigureAwait(false);

        async ValueTask<Result<TValue>> MapSuccessesInParallel(Func<ISuccess, ValueTask<TSuccess>> successMapper)
        {
            IEnumerable<TSuccess> successesMappedInParallel =
                await Task.WhenAll(
                    Successes.ConvertAll(async success => await successMapper(success)
                        .ConfigureAwait(false)))
                    .ConfigureAwait(false);

            return new Result<TValue>()
                .WithErrors(Errors)
                .WithSuccesses(successesMappedInParallel.Cast<ISuccess>())
                .WithValue(GetLastSuccessfulValueOrDefault());
        }

        async ValueTask<Result<TValue>> MapSuccessesSequentially(Func<ISuccess, ValueTask<TSuccess>> successMapper)
        {
            List<ISuccess> successesMappedSequentially = [];
            foreach (ISuccess success in Successes)
                successesMappedSequentially.Add(await successMapper(success).ConfigureAwait(false));

            return new Result<TValue>()
                .WithErrors(Errors)
                .WithSuccesses(successesMappedSequentially)
                .WithValue(GetLastSuccessfulValueOrDefault());
        }
    }
}
