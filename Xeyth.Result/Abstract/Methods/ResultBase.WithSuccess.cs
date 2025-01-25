using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;
public abstract partial class ResultBase<TResult>
{
    /// <summary>Adds a <see cref="ISuccess"/> message to the <typeparamref name="TResult"/>.</summary>
    /// <remarks>The provided <paramref name="successMessage"/> is converted using the <see cref="Success.DefaultFactory"/>.</remarks>
    /// <param name="successMessage">The success message to create a <see cref="ISuccess"/> instance.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithSuccess(ISuccess)"/>.</remarks>
    public TResult WithSuccess(string successMessage) =>
        WithSuccess(Success.Factory(successMessage));

    /// <summary>Adds a <see cref="ISuccess"/> of type <typeparamref name="TSuccess"/> to the <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TSuccess">The type of <see cref="ISuccess"/> to create and add.</typeparam>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithSuccess(ISuccess)"/>.</remarks>
    public TResult WithSuccess<TSuccess>()
        where TSuccess : Success, new() =>
        WithSuccess(new TSuccess());

    /// <summary>Adds a <see cref="ISuccess"/> to the <typeparamref name="TResult"/> using the <paramref name="successFactory"/>.</summary>
    /// <param name="successFactory">A factory function that creates a <see cref="ISuccess"/> instance.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithSuccess(ISuccess)"/>.</remarks>
    public TResult WithSuccess(Func<ISuccess> successFactory) =>
        WithSuccess(successFactory());

    /// <summary>Adds a <see cref="ISuccess"/> to the <typeparamref name="TResult"/>.</summary>
    /// <param name="success">The <see cref="ISuccess"/> instance to add to the <typeparamref name="TResult"/>.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithReason(IReason)"/>.</remarks>
    public TResult WithSuccess(ISuccess success) =>
        WithReason(success);

    /// <summary>Adds multiple <see cref="ISuccess"/>s to the <typeparamref name="TResult"/>.</summary>
    /// <param name="successMessages">A collection of success messages to create <see cref="ISuccess"/> instances.</param>
    /// <remarks>The provided <paramref name="successMessages"/> are converted using the <see cref="Success.DefaultFactory"/>.</remarks>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithSuccesses(IEnumerable{ISuccess})"/>.</remarks>
    public TResult WithSuccesses(IEnumerable<string> successMessages) =>
        WithSuccesses(successMessages.Select(Success.Factory));

    /// <summary>Adds multiple <see cref="ISuccess"/>s to the <typeparamref name="TResult"/>.</summary>
    /// <param name="successes">A collection of <see cref="ISuccess"/> instances to add to the <typeparamref name="TResult"/>.</param>
    /// <returns>The <typeparamref name="TResult"/> for chained invocation.</returns>
    /// <remarks>This method delegates to <see cref="WithReasons(IEnumerable{IReason})"/>.</remarks>
    public TResult WithSuccesses(IEnumerable<ISuccess> successes) =>
        WithReasons(successes);
}
