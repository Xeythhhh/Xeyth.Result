using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    public Result OnSuccess(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="func"/> if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="func">The asynchronous function to execute if the <see cref="Result"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    public async Task<Result> OnSuccess(Func<Task> func)
    {
        if (IsSuccess) await func().ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the successes if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute with the successes if the <see cref="Result"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    public Result OnSuccess(Action<IEnumerable<ISuccess>> action)
    {
        if (IsSuccess) action(Successes);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="func"/> with the successes if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="func">The asynchronous function to execute with the successes if the <see cref="Result"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    public async Task<Result> OnSuccess(Func<IEnumerable<ISuccess>, Task> func)
    {
        if (IsSuccess) await func(Successes).ConfigureAwait(false);
        return this;
    }
}
