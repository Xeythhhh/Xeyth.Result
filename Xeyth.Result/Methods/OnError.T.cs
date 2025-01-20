using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result{TValue}"/> is a failure. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result{TValue}"/> is a failure.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    public Result<TValue> OnError(Action action)
    {
        if (IsFailed) action();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> if the <see cref="Result{TValue}"/> is a failure. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The asynchronous function to execute if the <see cref="Result{TValue}"/> is a failure.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    public async Task<Result<TValue>> OnError(Func<Task> action)
    {
        if (IsFailed) await action().ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the errors if the <see cref="Result{TValue}"/> is a failure. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute with the errors if the <see cref="Result{TValue}"/> is a failure.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    public Result<TValue> OnError(Action<IEnumerable<IError>> action)
    {
        if (IsFailed) action(Errors);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> with the errors if the <see cref="Result{TValue}"/> is a failure. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The asynchronous function to execute with the errors if the <see cref="Result{TValue}"/> is a failure.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    public async Task<Result<TValue>> OnError(Func<IEnumerable<IError>, Task> action)
    {
        if (IsFailed) await action(Errors).ConfigureAwait(false);
        return this;
    }
}
