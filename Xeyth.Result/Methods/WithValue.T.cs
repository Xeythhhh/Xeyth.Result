namespace Xeyth.Result;

public partial class Result<TValue>
{
    /// <summary>Set value of the <see cref="Result{TValue}"/>.</summary>
    public Result<TValue> WithValue(TValue value)
    {
        Value = value;
        return this;
    }
}