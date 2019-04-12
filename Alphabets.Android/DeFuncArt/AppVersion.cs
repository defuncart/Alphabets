using Android.Content;
using Android.Content.PM;
using DeFuncArt.PlatformSpecific;

[assembly: Xamarin.Forms.Dependency(typeof(DeFuncArt.Droid.AppVersion))]
namespace DeFuncArt.Droid
{
    /// <summary>
    /// Android implementation of IAppVersion to retrieve the app's version (x.x.xx) and build number (xxx).
    /// </summary>
    public class AppVersion : IAppVersion
    {
        /// <summary>The package info.</summary>
        private PackageInfo packageInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DeFuncArt.Droid.AppVersion"/> class.
        /// </summary>
        public AppVersion()
        {
            //determine the package info
            Context context = global::Android.App.Application.Context;
            PackageManager packageManager = context.PackageManager;
            packageInfo = packageManager.GetPackageInfo(context.PackageName, 0);
        }

        /// <summary>
        /// Gets the app version (of the form x.x.xx).
        /// </summary>
        public string GetVersion()
        {
            return packageInfo.VersionName;
        }

        /// <summary>
        /// Gets the app build number (iOS) / version code (Android) (of the form xxx).
        /// </summary>
        public int GetBuildNumber()
        {
            return packageInfo.VersionCode;
        }
    }
}