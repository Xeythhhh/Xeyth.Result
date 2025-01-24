using Xeyth.Result.Base;
using Xeyth.Result.Exceptions;
using Xeyth.Result.Reasons;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result;

public partial class Result<TValue> : ResultBase<Result<TValue>>, IResult<TValue>
{
    private TValue _value = default!;

    /// <inheritdoc/>
    /// <remarks>Use <see cref="GetLastSuccessfulValueOrDefault(TValue?)"/> to get the last known value or <see langword="default"/>(<typeparamref name="TValue"/>) for failed results.</remarks>
    public TValue Value
    {
        get
        {
            ThrowIfFailed();
            return _value;
        }
        private set => _value = value;
    }

    /// <summary>Gets the last successful <see cref="Value"/>, or a fallback value.
    /// <para>For reference types, it may return <see langword="null"/> if the result is failed and no <paramref name="fallbackValue"/> is provided.</para></summary>
    /// <param name="fallbackValue">The value to return if the result is failed or has a <see langword="default"/>(<typeparamref name="TValue"/>) value.</param>
    /// <remarks>If a value is not available and the <paramref name="fallbackValue"/> is not provided, the method will return <see langword="default"/>(<typeparamref name="TValue"/>).</remarks>
    /// <returns>The last successful <see langword="value"/> if available; otherwise, returns the <paramref name="fallbackValue"/>.</returns>
    public TValue GetLastSuccessfulValueOrDefault(TValue fallbackValue = default!) =>
        EqualityComparer<TValue>.Default.Equals(_value, default)
            ? fallbackValue
            : _value;

    public override string ToString()
    {
        string valueString = _value is not null ? $"Value:{_value}" : string.Empty;
        return $"{base.ToString()}, {valueString}";
    }

    public static implicit operator Result(Result<TValue> result) =>
       result.ToResult();

    public static implicit operator Result<TValue>(Result result) =>
        result.ToResult<TValue>();

    public static implicit operator Result<object>(Result<TValue> result) =>
        result.ToResult<object>(value => value!);

    public static implicit operator Result<TValue>(TValue value) =>
        value is Result<TValue> result ? result : Result.Ok(value);

    public static implicit operator Result<TValue>(Error error) =>
        Result.Fail(error);

    public static implicit operator Result<TValue>(List<IError> errors) =>
        Result.Fail(errors);

    /// <summary>Deconstruct Result</summary>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out TValue value)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        value = IsSuccess ? Value : default!;
    }

    /// <summary>Deconstruct Result</summary>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out TValue value, out List<IError> errors)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        value = IsSuccess ? Value : default!;
        errors = Errors;
    }

    private void ThrowIfFailed()
    {
        if (IsFailed)
            throw new FailedResultValueAccessException(Errors);
    }
}
