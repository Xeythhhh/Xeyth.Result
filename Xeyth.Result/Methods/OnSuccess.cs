using Xeyth.Result.Reasons;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result OnSuccess(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) action();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The asynchronous function to execute if the <see cref="Result"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result> OnSuccess(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) await action().ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the successes if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute with the successes if the <see cref="Result"/> is successful.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result OnSuccess(Action<IEnumerable<ISuccess>> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) action(Successes);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> with the successes if the <see cref="Result"/> is successful. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The asynchronous function to execute with the successes if the <see cref="Result"/> is successful.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result> OnSuccess(Func<IEnumerable<ISuccess>, Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsSuccess) await action(Successes).ConfigureAwait(false);
        return this;
    }
}
