using Xeyth.Result.Reasons;

namespace Xeyth.Result;
public partial class Result
{
    /// <summary>Creates a failed <see cref="Result"/> with the specified error message.</summary>
    /// <param name="errorMessage">The error message to include in the failure.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Fail(string errorMessage) =>
        Fail(Error.DefaultFactory(errorMessage));

    /// <summary>Creates a failed <see cref="Result"/> with the specified error.</summary>
    /// <param name="error">The <see cref="IError"/> to include in the failure.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Fail(IError error) =>
        new Result().WithError(error);

    /// <summary>Creates a failed <see cref="Result"/> with the specified error messages.</summary>
    /// <param name="errorMessages">The error messages to include in the failure.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Fail(IEnumerable<string> errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        return Fail(errorMessages.Select(Error.DefaultFactory));
    }

    /// <summary>Creates a failed <see cref="Result"/> with the specified errors.</summary>
    /// <param name="errors">The collection of <see cref="IError"/> instances to include in the failure.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Fail(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result().WithErrors(errors);
    }

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error message.</summary>
    /// <typeparam name="TValue">The type of the value associated with the <see cref="Result{TValue}"/>.</typeparam>
    /// <param name="errorMessage">The error message to include in the failure.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Fail<TValue>(string errorMessage) =>
        Fail<TValue>(Error.DefaultFactory(errorMessage));

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error.</summary>
    /// <typeparam name="TValue">The type of the value associated with the <see cref="Result{TValue}"/>.</typeparam>
    /// <param name="error">The <see cref="IError"/> to include in the failure.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Fail<TValue>(IError error) =>
        new Result<TValue>().WithError(error);

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified error messages.</summary>
    /// <typeparam name="TValue">The type of the value associated with the <see cref="Result{TValue}"/>.</typeparam>
    /// <param name="errorMessages">The error messages to include in the failure.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Fail<TValue>(IEnumerable<string> errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        return Fail<TValue>(errorMessages.Select(Error.DefaultFactory));
    }

    /// <summary>Creates a failed <see cref="Result{TValue}"/> with the specified errors.</summary>
    /// <typeparam name="TValue">The type of the value associated with the <see cref="Result{TValue}"/>.</typeparam>
    /// <param name="errors">The collection of <see cref="IError"/> instances to include in the failure.</param>
    /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Fail<TValue>(IEnumerable<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result<TValue>().WithErrors(errors);
    }
}
