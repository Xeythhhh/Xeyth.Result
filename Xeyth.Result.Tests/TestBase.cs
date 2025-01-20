namespace Xeyth.Result.Tests;
public abstract class TestBase
{
    internal VerifySettings Settings { get; set; }

    protected TestBase()
    {
        Settings = new VerifySettings();
        Settings.UseDirectory(".Snapshots");
    }
}