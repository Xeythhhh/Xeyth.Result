using System.Runtime.CompilerServices;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result
{
    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute if the <see cref="Result"/> is a failure.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result OnError(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) action();
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The asynchronous action to execute if the <see cref="Result"/> is a failure.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    [OverloadResolutionPriority(1)]
    public async Task<Result> OnError(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) await action().ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The <see cref="ValueTask"/> action to execute if the <see cref="Result"/> is a failure.</param>
    /// <returns>A <see cref="ValueTask"/> representing the operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async ValueTask<Result> OnError(Func<ValueTask> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) await action().ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified <paramref name="action"/> with the errors if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The action to execute with the errors if the <see cref="Result"/> is a failure.</param>
    /// <returns>The calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public Result OnError(Action<IEnumerable<IError>> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) action(Errors);
        return this;
    }

    /// <summary>Executes the specified asynchronous <paramref name="action"/> with the errors if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The asynchronous function to execute with the errors if the <see cref="Result"/> is a failure.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async Task<Result> OnError(Func<IEnumerable<IError>, Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) await action(Errors).ConfigureAwait(false);
        return this;
    }

    /// <summary>Executes the specified  <paramref name="action"/> with the errors if the <see cref="Result"/> is a failure. Returns the calling <see cref="Result"/>.</summary>
    /// <param name="action">The <see cref="ValueTask"/> action to execute with the errors if the <see cref="Result"/> is a failure.</param>
    /// <returns>A <see cref="ValueTask"/> representing the operation, containing the calling <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="action"/> is <see langword="null"/>.</exception>
    public async ValueTask<Result> OnError(Func<IEnumerable<IError>, ValueTask> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (IsFailed) await action(Errors).ConfigureAwait(false);
        return this;
    }
}
