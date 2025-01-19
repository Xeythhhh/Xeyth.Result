using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    public Result OnSuccess(Action action)
    {
        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the result's value if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute with the result's value if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    public Result<TValue> OnSuccess(Action<TValue> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="func"/> if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="func">The asynchronous function to execute if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    public async Task<Result<TValue>> OnSuccess(Func<Task> func)
    {
        if (IsSuccess) await func();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="func"/> with the result's value if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="func">The asynchronous function to execute with the result's value if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    public async Task<Result<TValue>> OnSuccess(Func<TValue, Task> func)
    {
        if (IsSuccess) await func(Value);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the successes if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute with the successes if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    public Result<TValue> OnSuccess(Action<IEnumerable<ISuccess>> action)
    {
        if (IsSuccess) action(Successes);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="func"/> with the successes if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="func">The asynchronous function to execute with the successes if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    public async Task<Result<TValue>> OnSuccess(Func<IEnumerable<ISuccess>, Task> func)
    {
        if (IsSuccess) await func(Successes).ConfigureAwait(false);
        return this;
    }
}
