using Alphabets.Helpers;
using Alphabets.Views;
using DeFuncArt.Localization;
using DeFuncArt.Localization.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Alphabets
{
    /// <summary>
    /// A cross platform app.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.App"/> class.
        /// </summary>
        public App()
        {
            //initialize all components
            InitializeComponent();

            //if this is the first launch, initialize the display language
            if (!UserSettings.AlreadyLaunched)
            {
                //select the user's device language, if supported. otherwise the default ("en") will be kept
                if (LocalizationHelper.IsValidLanguage(LocalizationManager.DeviceCultureInfo.TwoLetterISOLanguageName))
                {
                    UserSettings.Language = LocalizationManager.DeviceCultureInfo.TwoLetterISOLanguageName;
                }
                //set a flag that initial setup is complete
                UserSettings.AlreadyLaunched = true;
            }

            //update localization depending on user's setting
            LocalizationManager.SetLangauge(UserSettings.Language);

            //set the initial page
            MainPage = new MainPage();
        }
    }
}
