using BenchmarkDotNet.Attributes;

using System.Diagnostics.CodeAnalysis;

namespace Xeyth.Result.Benchmarks;

[MemoryDiagnoser]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
[SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
public class ResultBenchmarks
{
    [Params(1, 5, 10, 100)]
    public int TaskCount;

    const string SuccessMessage = "Success";
    const string ErrorMessage = "Error";

    [Benchmark]
    public Result CreateNonGenericSuccessResult() =>
        Result.Ok();

    [Benchmark]
    public Result CreateGenericSuccessResult() =>
        Result.Ok(TaskCount);

    [Benchmark]
    public Result CreateNonGenericSuccessResultWithSuccess() =>
        Result.Ok().WithSuccess(SuccessMessage);

    [Benchmark]
    public Result CreateGenericSuccessResultWithSuccess() =>
        Result.Ok(TaskCount).WithSuccess(SuccessMessage);

    [Benchmark]
    public Result CreateNonGenericFailureResult() =>
        Result.Fail(ErrorMessage);

    [Benchmark]
    public Result CreateGenericFailureResult() =>
        Result.Fail<int>(ErrorMessage);
}
