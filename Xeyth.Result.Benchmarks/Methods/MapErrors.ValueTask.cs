using BenchmarkDotNet.Attributes;

using Xeyth.Result.Benchmarks.Extensions;
using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Benchmarks.Methods;

/// <summary>Benchmarks for <see cref="IError"/> mapping with <see cref="ValueTask"/> in Xeyth.Result.</summary>
[MemoryDiagnoser]
public class MapErrorsValueTaskBenchmarks
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Result _result;
    private Func<IError, ValueTask<IError>> _allSyncMapper;
    private Func<IError, ValueTask<IError>> _allAsyncMapper;
    private Func<IError, ValueTask<IError>> _allAsyncDelayed10Mapper;
    private Func<IError, ValueTask<IError>> _allAsyncDelayed1000Mapper;
    //private Func<IError, ValueTask<IError>> _partialSyncMapper;
    //private Func<IError, ValueTask<IError>> _partialAsyncMapper;
    private Func<IError, ValueTask<IError>> _halfSyncMapper;
    private Func<IError, ValueTask<IError>> _halfSyncDelayed10Mapper;
    private Func<IError, ValueTask<IError>> _halfSyncDelayed1000Mapper;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [Params(1, 5, 10, 100)]
    public int TaskCount;

    [GlobalSetup]
    public void Setup()
    {
        _result = Result.Ok()
            .WithErrors(Enumerable.Range(0, TaskCount).Select(i => new Error($"Error {i}")));

        _allSyncMapper = error => ValueTask.FromResult<IError>(new Error($"Mapped {error.Message}"));

        _allAsyncMapper = async error =>
        {
            await Task.Delay(TimeSpan.FromMicroseconds(1));
            return new Error($"Mapped {error.Message}");
        };

        _allAsyncDelayed10Mapper = async error =>
        {
            await Task.Delay(TimeSpan.FromMicroseconds(10));
            return new Error($"Mapped {error.Message}");
        };

        _allAsyncDelayed1000Mapper = async error =>
        {
            await Task.Delay(TimeSpan.FromMicroseconds(1000));
            return new Error($"Mapped {error.Message}");
        };

        //_partialSyncMapper = CreateMapperFromDistribution(GenerateDistribution(TaskCount, 0.25));
        //_partialAsyncMapper = CreateMapperFromDistribution(GenerateDistribution(TaskCount, 0.75));

        _halfSyncMapper = CreateMapperFromDistribution(GenerateDistribution(TaskCount, 0.5));
        _halfSyncDelayed10Mapper = CreateMapperFromDistribution(GenerateDistribution(TaskCount, 0.5), 10);
        _halfSyncDelayed1000Mapper = CreateMapperFromDistribution(GenerateDistribution(TaskCount, 0.5), 1000);
    }

    private static List<bool> GenerateDistribution(int count, double syncRatio)
    {
        int syncCount = (int)(count * syncRatio);
        List<bool> distribution = Enumerable.Repeat(true, syncCount)
            .Concat(Enumerable.Repeat(false, count - syncCount))
            .ToList();
        distribution.Shuffle();

        return distribution;
    }

    private static Func<IError, ValueTask<IError>> CreateMapperFromDistribution(List<bool> distribution, int asyncDurationInMicroseconds = 1)
    {
        int index = 0;
        return async error =>
        {
            bool isSync = distribution[index++ % distribution.Count];
            if (isSync)
                return new Error($"Mapped {error.Message}");
            await Task.Delay(TimeSpan.FromMicroseconds(asyncDurationInMicroseconds));
            return new Error($"Mapped {error.Message}");
        };
    }

    // All Sync

    [Benchmark]
    public async Task<Result> MapErrors_AllSync_Auto()
    {
        return await _result.MapErrors(_allSyncMapper);
    }

    //[Benchmark]
    //public async Task<Result> MapErrors_AllSync_Parallel()
    //{
    //    return await _result.MapErrors(_allSyncMapper, true);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_AllSync_Sequential()
    //{
    //    return await _result.MapErrors(_allSyncMapper, false);
    //}

    // 25% Sync

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentSync_Auto()
    //{
    //    return await _result.MapErrors(_partialSyncMapper);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentSync_Parallel()
    //{
    //    return await _result.MapErrors(_partialSyncMapper, true);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentSync_Sequential()
    //{
    //    return await _result.MapErrors(_partialSyncMapper, false);
    //}

    // 25% Async

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentAsync_Auto()
    //{
    //    return await _result.MapErrors(_partialAsyncMapper);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentAsync_Parallel()
    //{
    //    return await _result.MapErrors(_partialAsyncMapper, true);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_25PercentAsync_Sequential()
    //{
    //    return await _result.MapErrors(_partialAsyncMapper, false);
    //}

    // Half Sync

    [Benchmark]
    public async Task<Result> MapErrors_HalfSync_Auto()
    {
        return await _result.MapErrors(_halfSyncMapper);
    }

    //[Benchmark]
    //public async Task<Result> MapErrors_HalfSync_Parallel()
    //{
    //    return await _result.MapErrors(_halfSyncMapper, true);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_HalfSync_Sequential()
    //{
    //    return await _result.MapErrors(_halfSyncMapper, false);
    //}

    // Half Sync Delayed 10

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed10_Auto()
    {
        return await _result.MapErrors(_halfSyncDelayed10Mapper);
    }

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed10_Parallel()
    {
        return await _result.MapErrors(_halfSyncDelayed10Mapper, true);
    }

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed10_Sequential()
    {
        return await _result.MapErrors(_halfSyncDelayed10Mapper, false);
    }

    // Half Sync Delayed 1000

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed1000_Auto()
    {
        return await _result.MapErrors(_halfSyncDelayed1000Mapper);
    }

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed1000_Parallel()
    {
        return await _result.MapErrors(_halfSyncDelayed1000Mapper, true);
    }

    [Benchmark]
    public async Task<Result> MapErrors_HalfSyncDelayed1000_Sequential()
    {
        return await _result.MapErrors(_halfSyncDelayed1000Mapper, false);
    }

    // All Async

    [Benchmark]
    public async Task<Result> MapErrors_AllAsync_Auto()
    {
        return await _result.MapErrors(_allAsyncMapper);
    }

    //[Benchmark]
    //public async Task<Result> MapErrors_AllAsync_Parallel()
    //{
    //    return await _result.MapErrors(_allAsyncMapper, true);
    //}

    //[Benchmark]
    //public async Task<Result> MapErrors_AllAsync_Sequential()
    //{
    //    return await _result.MapErrors(_allAsyncMapper, false);
    //}

    // All Async Delayed 10

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed10_Auto()
    {
        return await _result.MapErrors(_allAsyncDelayed10Mapper);
    }

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed10_Parallel()
    {
        return await _result.MapErrors(_allAsyncDelayed10Mapper, true);
    }

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed10_Sequential()
    {
        return await _result.MapErrors(_allAsyncDelayed10Mapper, false);
    }

    // All Async Delayed 1000

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed1000_Auto()
    {
        return await _result.MapErrors(_allAsyncDelayed1000Mapper);
    }

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed1000_Parallel()
    {
        return await _result.MapErrors(_allAsyncDelayed1000Mapper, true);
    }

    [Benchmark]
    public async Task<Result> MapErrors_AllAsyncDelayed1000_Sequential()
    {
        return await _result.MapErrors(_allAsyncDelayed1000Mapper, false);
    }
}
