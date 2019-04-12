namespace DeFuncArt.PlatformSpecific
{
    /// <summary>
    /// An interface with platform specific methods to retrieve the app's version (x.x.xx) and build number (xxx).
    /// </summary>
    public interface IAppVersion
    {
        /// <summary>Gets the app version (of the form x.x.xx).</summary>
        string GetVersion();

        /// <summary>Gets the app build number (iOS) / version code (Android) (of the form xxx).</summary>
        int GetBuildNumber();
    }
}
