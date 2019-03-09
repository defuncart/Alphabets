using Xamarin.Forms;

namespace Alphabets.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Managers.AlphabetManager.Log();
            Managers.CourseManager.Log();
        }
    }
}
