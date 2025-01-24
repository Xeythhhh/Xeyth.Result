using Xeyth.Result.Extensions;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Reasons;

/// <summary>Builds a string representation of a <see cref="IReason"/>, including its <see cref="Type"/> and associated information.</summary>
internal class ReasonStringBuilder
{
    private string _reasonType = string.Empty;
    private readonly List<string> _infos = [];

    /// <summary>Sets the <see cref="Type"/> of the <see cref="IReason"/>.</summary>
    /// <param name="type">The <see cref="Type"/> of the <see cref="IReason"/>.</param>
    /// <returns>The current instance of <see cref="ReasonStringBuilder"/>.</returns>
    public ReasonStringBuilder WithReasonType(Type type)
    {
        _reasonType = type.Name;
        return this;
    }

    /// <summary>Adds labeled information to the <see cref="IReason"/>.</summary>
    /// <param name="label">The label for the information.</param>
    /// <param name="value">The value associated with the label.</param>
    /// <returns>The current instance of <see cref="ReasonStringBuilder"/>.</returns>
    public ReasonStringBuilder WithInfo(string label, string value)
    {
        string infoString = value.ToLabelValueStringOrEmpty(label);

        if (!string.IsNullOrEmpty(infoString))
        {
            _infos.Add(infoString);
        }

        return this;
    }

    /// <summary>Builds the string representation of the <see cref="IReason"/>.</summary>
    /// <returns>A string describing the <see cref="IReason"/>, including its <see cref="Type"/> and associated information.</returns>
    public string Build() => _reasonType + (_infos.Count > 0 ? " with " + ReasonInfosToString(_infos) : string.Empty);

    /// <summary>Converts a <see cref="List{T}"/> of reason information <see cref="string"/>s into a single <see cref="string"/>.</summary>
    /// <param name="reasonInfos">The list of reason information strings.</param>
    /// <returns>A comma-separated <see cref="string"/> of <see cref="IReason"/> information.</returns>
    private static string ReasonInfosToString(List<string> reasonInfos) =>
        string.Join(", ", reasonInfos);
}
