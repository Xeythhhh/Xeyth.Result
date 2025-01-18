namespace Xeyth.Result.Extensions;

internal static class ObjectExtensions
{
    internal static string ToLabelValueStringOrEmpty(this object value, string label) =>
        value.ToString()?.Length != 0
            ? $"{label}='{value}'"
            : string.Empty;
}