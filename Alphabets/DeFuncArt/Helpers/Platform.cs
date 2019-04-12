using DeFuncArt.PlatformSpecific;
using Xamarin.Forms;

namespace DeFuncArt.Helpers
{
    public static class Platform
    {
        /// <summary>A backing variable for AppVersion.</summary>
        private static int appVersion = -1;

        /// <summary>
        /// The app version (x.x.xx.xxx) respresented as an integer.
        /// </summary>
        public static int AppVersion
        {
            get
            {
                if (appVersion == -1)
                {
                    //get the app version (i.e. 1.2.3) and build number (i.e. 99)
                    string version = DependencyService.Get<IAppVersion>().GetVersion();
                    int build = DependencyService.Get<IAppVersion>().GetBuildNumber();

                    //determine the major, minor and patch components - assumes string of the form x.x.xx (where x are numbers)
                    string[] components = version.Split('.');
                    int major = int.Parse(components[0]);
                    int minor = int.Parse(components[1]);
                    int patch = components.Length == 3 ? int.Parse(components[2]) : 0;

                    //determine app version as an int
                    appVersion = AppVersionAsInt(major, minor, patch, build);
                }

                return appVersion;
            }
        }

        /// <summary>
        /// Converts an app version x.x.xx with build number xxx into an integer.
        /// </summary> 
        private static int AppVersionAsInt(int major, int minor, int patch, int build)
        {
            return major * 1000000 + minor * 100000 + patch * 1000 + build;
        }
    }
}
