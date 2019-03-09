using Alphabets.Views.Game;
using Xamarin.Forms;

namespace Alphabets.Views
{
    /// <summary>
    /// The game page where the player learns letters and is quized on them.
    /// </summary>
    public partial class GamePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.GamePage"/> class.
        /// </summary>
        public GamePage()
        {
            //initialize components
            InitializeComponent();

            //delegates
            navBar.OnExitButtonClicked += NavBar_OnExitButtonClicked;

            //create subviews
            LearnView learnView = new LearnView();
            MultipleChoiceView multipleChoiceView = new MultipleChoiceView();

            //TODO debug
            //contentView.Content = learnView;
            contentView.Content = multipleChoiceView;
        }

        /// <summary>
        /// Callback when the exit button the the nav bar is pressed.
        /// </summary>
        private void NavBar_OnExitButtonClicked()
        {
            //TODO display warning popup

            Navigation.PopModalAsync();
        }
    }
}
