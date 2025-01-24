using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains any <see cref="ISuccess"/>.</summary>
    /// <returns><see langword="true"/> if any successes were found.</returns>
    public bool HasSuccess() => Successes.Count > 0;

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <returns><see langword="true"/> if any <typeparamref name="TSuccess"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasSuccess{TSuccess}(out IEnumerable{TSuccess})"/>.</remarks>
    public bool HasSuccess<TSuccess>()
        where TSuccess : ISuccess =>
        HasSuccess<TSuccess>(out _);

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/>
    /// and outputs the matching <paramref name="errors"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <param name="successes">A collection with the matching <typeparamref name="TSuccess"/>s</param>
    /// <returns><see langword="true"/> if any <typeparamref name="TSuccess"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasSuccess{TSuccess}(Func{TSuccess, bool}, out IEnumerable{TSuccess})"/>.</remarks>
    public bool HasSuccess<TSuccess>(out IEnumerable<TSuccess> successes)
        where TSuccess : ISuccess =>
        HasSuccess(_ => true, out successes);

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/>
    /// that matches the specified <paramref name="predicate"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/>.</param>
    /// <returns><see langword="true"/> if any matching <typeparamref name="TSuccess"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasSuccess{TSuccess}(Func{TSuccess, bool}, out IEnumerable{TSuccess})"/>.</remarks>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate)
        where TSuccess : ISuccess =>
        HasSuccess(predicate, out _);

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> that matches the specified <paramref name="predicate"/>.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/>.</param>
    /// <returns><see langword="true"/> if any matching <see cref="ISuccess"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasSuccess(Func{ISuccess, bool}, out IEnumerable{ISuccess})"/>.</remarks>
    public bool HasSuccess(Func<ISuccess, bool> predicate) =>
        HasSuccess(predicate, out _);

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> that matches the specified <paramref name="predicate"/>
    /// and outputs the matching <see cref="ISuccess"/>s.</summary>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/>.</param>
    /// <param name="successes">A collection with the matching <see cref="ISuccess"/>s</param>
    /// <returns><see langword="true"/> if any matching <see cref="ISuccess"/>s were found.</returns>
    /// <remarks>This method delegates to <see cref="HasSuccess{TSuccess}(Func{TSuccess, bool}, out IEnumerable{TSuccess})"/>.</remarks>
    public bool HasSuccess(Func<ISuccess, bool> predicate, out IEnumerable<ISuccess> successes) =>
        HasSuccess<ISuccess>(predicate, out successes);

    /// <summary>Checks if the result contains any <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/> that matches the specified <paramref name="predicate"/>
    /// and outputs the matching <paramref name="errors"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to filter by.</typeparam>
    /// <param name="predicate">The condition to evaluate for each <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/>.</param>
    /// <param name="successes">A collection with the matching <typeparamref name="TSuccess"/>s</param>
    /// <returns><see langword="true"/> if any matching <typeparamref name="TSuccess"/>s were found.</returns>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate, out IEnumerable<TSuccess> successes)
        where TSuccess : ISuccess
    {
        successes = GetSuccesses(predicate);
        return successes.Any();
    }
}
