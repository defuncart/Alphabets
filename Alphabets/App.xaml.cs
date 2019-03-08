using Alphabets.Helpers;
using Alphabets.Resources;
using Alphabets.Views;
using Plugin.Multilingual;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Alphabets
{
    public partial class App : Application
    {
        public App()
        {
            //initialize all components
            InitializeComponent();

            //if this is the first launch, try to initialize the display language to be the same as the device language
            if (!UserSettings.AlreadyLaunched)
            {
                UserSettings.Language = CrossMultilingual.Current.DeviceCultureInfo.TwoLetterISOLanguageName; //if the device language is invalid, the default ("en") will be kept
                UserSettings.AlreadyLaunched = true;
            }

            //update localization depending on user's setting
            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.TwoLetterISOLanguageName == UserSettings.Language);
            //AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            //set the initial page
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
