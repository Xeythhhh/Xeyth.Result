namespace Xeyth.Result.Tests;
public abstract class SnapshotTestBase
{
    internal VerifySettings Settings { get; set; }

    protected SnapshotTestBase()
    {
        Settings = new VerifySettings();
        Settings.UseDirectory(".Snapshots");
    }
}