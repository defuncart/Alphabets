using Alphabets.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Alphabets
{
    public partial class App : Application
    {
        public App()
        {
            //initialize all components and set the initial page
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
