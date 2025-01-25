using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Xeyth.FunctionalExtensions;

/// <summary>This class serves no functional purpose and exists only to ensure the assembly produces a valid artifact.</summary>
public static class FunctionalExtensionsInfo
{
    /// <summary>A meaningless constant.</summary>
    public const string Purpose = "This package only references other packages.";

    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255:The 'ModuleInitializer' attribute should not be used in libraries", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
    public static void Initialize() { }
}
