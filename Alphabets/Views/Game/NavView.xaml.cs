using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used as a navigation bar in GamePage.
    /// </summary>
    public partial class NavView : ContentView
    {
        #region Properties

        /// <summary>
        /// The exit button.
        /// </summary>
        public Button ExitButton => exitButton;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.NavView"/> class.
        /// </summary>
        public NavView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Animates the progress to a given value over a given duration with a given easing.
        /// Can be awaited on.
        /// </summary>
        public async Task<bool> AnimateProgressTo(double value, uint duration, Easing easing)
        {
            return await progressBar.ProgressTo(value, duration, easing);
        }

        /// <summary>
        /// Sets the progress and automatically sets the progress bar to be visible.
        /// </summary>
        public void SetProgress(double progress)
        {
            progressBar.Progress = progress;
            progressBar.IsVisible = true; label.IsVisible = false;
        }

        /// <summary>
        /// Sets the text and automatically sets the label to be visible.
        /// </summary>
        public void SetText(string text)
        {
            label.Text = text;
            progressBar.IsVisible = false; label.IsVisible = true;
        }

        #endregion
    }
}
