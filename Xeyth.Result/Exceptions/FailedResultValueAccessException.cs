using Xeyth.Result.Base;
using Xeyth.Result.Reasons.Abstract;

namespace Xeyth.Result.Exceptions;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1194:Implement exception constructors", Justification = "<Pending>")]
public class FailedResultValueAccessException(List<IError> errors) :
    InvalidOperationException($"Result is in status failed. Value is not set. Having: {ResultBase.ReasonsToString(errors)}");
