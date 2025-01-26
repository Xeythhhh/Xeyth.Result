using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Xeyth.FunctionalExtensions;

/// <summary>This class serves no functional purpose and exists only to ensure the assembly produces a valid artifact.</summary>
public static class FunctionalExtensionsInfo
{
    /// <summary>A meaningless constant.</summary>
    public const string Purpose = "This package only references other packages.";

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    [SuppressMessage("Roslynator", "RCS1213:Remove unused member declaration", Justification = "<Pending>")]
    private const int ArbitraryNumberToForceChangesOnGit = 69;

    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255:The 'ModuleInitializer' attribute should not be used in libraries", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
    public static void Initialize() { }
}
