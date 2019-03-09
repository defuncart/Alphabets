using Alphabets.Views.Game;
using Xamarin.Forms;

namespace Alphabets.Views
{
    /// <summary>
    /// The game page where the player learns letters and is quized on them.
    /// </summary>
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();

            //create subviews
            LearnView learnView = new LearnView();
            MultipleChoiceView multipleChoiceView = new MultipleChoiceView();

            //TODO debug
            //contentView.Content = learnView;
            contentView.Content = multipleChoiceView;
        }
    }
}
