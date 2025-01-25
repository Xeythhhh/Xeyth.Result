using System.Collections.ObjectModel;

using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Abstract;

/// <summary>Defines the base interface for result objects, enabling structured access to result components such as <see cref="IReason"/>s, <see cref="IError"/>s, and <see cref="ISuccess"/>s.</summary>
/// <remarks>This interface provides a unified contract for result objects, facilitating <see cref="IError"/> and <see cref="ISuccess"/> handling, as well as reasoning for result states.</remarks>
public interface IResultBase
{
    /// <summary>Gets a value indicating whether the result contains at least one <see cref="IError"/>.</summary>
    /// <value><see langword="true"/> if the result has one or more <see cref="IError"/>s; otherwise, <see langword="false"/>.</value>
    bool IsFailed { get; }

    /// <summary>Gets a value indicating whether the result contains no <see cref="IError"/>s.</summary>
    /// <value><see langword="true"/> if the result has no <see cref="IError"/>s; otherwise, <see langword="false"/>.</value>
    bool IsSuccess { get; }

    /// <summary>Gets a collection of all <see cref="IError"/>s contained in the result.</summary>
    /// <value>A <see cref="List{T}"/> containing all <see cref="IError"/>s associated with the result.</value>
    List<IError> Errors { get; }

    /// <summary>Gets a collection of all <see cref="ISuccess"/>s contained in the result.</summary>
    /// <value>A <see cref="List{T}"/> containing all <see cref="ISuccess"/>s associated with the result.</value>
    List<ISuccess> Successes { get; }

    /// <summary>Gets a collection of all <see cref="IReason"/>s in the result, including both <see cref="IError"/>s and <see cref="ISuccess"/>s.</summary>
    /// <value>A <see cref="List{T}"/> containing all <see cref="IReason"/>s associated with the result.</value>
    ObservableCollection<IReason> Reasons { get; }
}
