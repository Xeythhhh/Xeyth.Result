using System.Collections.ObjectModel;

using Xeyth.Result.Abstract;
using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Base;

/// <summary>Serves as the base class for result objects, managing collections of <see cref="IReason"/>s, including <see cref="IError"/>s and <see cref="ISuccess"/>s.</summary>
/// <remarks>This class provides foundational support for result-handling logic, including error and success tracking.
/// It is designed for use in derived result types, enabling fluent API patterns for constructing and managing results.</remarks>
public abstract partial class ResultBase : IResultBase
{
    /// <summary>Initializes a new instance of the <see cref="ResultBase"/> class.</summary>
    /// <remarks>The constructor initializes an empty collection of <see cref="IReason"/>s.</remarks>
    protected ResultBase()
    {
        Reasons.CollectionChanged += (sender, args) => UpdateFailedState();
    }

    private void UpdateFailedState()
    {
        _cachedErrors = Reasons.OfType<IError>().ToList();
        IsFailed = _cachedErrors.Count != 0;
    }

    private bool _isFailed;
    /// <inheritdoc/>
    public bool IsFailed
    {
        get => _isFailed;
        private set
        {
            if (_isFailed != value)
                _isFailed = value;
        }
    }

    /// <inheritdoc/>
    public bool IsSuccess => !IsFailed;

    /// <inheritdoc/>
    public ObservableCollection<IReason> Reasons { get; } = [];

    /// <inheritdoc/>
    /// <remarks>To include nested errors, use <see cref="GetErrors{TError}()"/> with <see cref="Error"/> as the type parameter.</remarks>
    public List<IError> Errors => _cachedErrors;
    private List<IError> _cachedErrors = [];

    /// <inheritdoc/>
    public List<ISuccess> Successes => Reasons.OfType<ISuccess>().ToList();

    /// <summary>Deconstructs the result into its success and failure states.</summary>
    /// <param name="isSuccess">Indicates whether the result is successful.</param>
    /// <param name="isFailed">Indicates whether the result has failed.</param>
    /// <remarks><example><code>
    /// <see cref="Result"/> result = GetResult();
    /// (<see cref="bool"/> isSuccess, <see cref="bool"/> isFailed) = result;
    /// if (isFailed) { HandleFailure(); }
    /// </code></example></remarks>
    public void Deconstruct(out bool isSuccess, out bool isFailed)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
    }

    /// <summary>Deconstructs the result into its success and failure states, along with a collection of errors.</summary>
    /// <param name="isSuccess">Indicates whether the result is successful.</param>
    /// <param name="isFailed">Indicates whether the result has failed.</param>
    /// <param name="errors">A collection of <see cref="IError"/> instances, if the result has failed; otherwise, an empty list.</param>
    /// <remarks><example><code>
    /// <see cref="Result"/> result = GetResult();
    /// (<see cref="bool"/> isSuccess, <see cref="bool"/> isFailed, <see cref="List{IError}"/> errors) = result;
    /// if (isFailed) { HandleFailure(); }
    /// </code></example></remarks>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out List<IError> errors)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        errors = Errors;
    }

    /// <summary>Converts a collection of <see cref="IReason"/> instances into a single string representation.</summary>
    /// <param name="reasons">The collection of <see cref="IReason"/> instances to convert.</param>
    /// <returns>A concatenated string of <see cref="IReason.Message"/>s.</returns>
    /// <remarks>Each <see cref="IReason.Message"/> is separated by a semicolon (<c>";"</c>).</remarks>
    internal static string ReasonsToString(IReadOnlyCollection<IReason> reasons) =>
        string.Join("; ", reasons);
}
