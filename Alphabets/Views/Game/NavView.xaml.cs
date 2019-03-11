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

        /// <summary>
        /// The current progress (expressed between 0 and 1).
        /// </summary>
        public double Progress
        {
            get => progressBar.Progress;
            set => progressBar.Progress = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.NavView"/> class.
        /// </summary>
        public NavView()
        {
            //initialize components
            InitializeComponent();
        }

        #endregion
    }
}
