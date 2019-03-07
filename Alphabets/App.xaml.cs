using Alphabets.Views;
using Alphabets.Resources;
using Plugin.Multilingual;
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

            //setup localization
            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.DeviceCultureInfo;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            //set the initial page
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
