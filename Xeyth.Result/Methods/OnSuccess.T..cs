using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result OnSuccess(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the result's value if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The action to execute with the result's value if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result<TValue> OnSuccess(Action<TValue> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) action(Value);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The asynchronous function to execute if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result<TValue>> OnSuccess(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) await action();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> with the result's value if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The asynchronous function to execute with the result's value if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result<TValue>> OnSuccess(Func<TValue, Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) await action(Value);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the successes if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute with the successes if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result<TValue> OnSuccess(Action<IEnumerable<ISuccess>> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) action(Successes);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> with the successes if the <see cref="Result{TValue}"/> is successful. Returns the calling <see cref="Result{TValue}"/>.</summary>
    /// <param name="action">The asynchronous function to execute with the successes if the <see cref="Result{TValue}"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result{TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result<TValue>> OnSuccess(Func<IEnumerable<ISuccess>, Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) await action(Successes).ConfigureAwait(false);
        return this;
    }
}
