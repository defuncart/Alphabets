using Alphabets.Models;
using System;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used in GamePage to teach a new letter.
    /// </summary>
    public partial class LearnView : ContentView
    {
        #region Events

        /// <summary>
        /// An event which occurs when the player wants to proceed.
        /// </summary>
        public Action OnProceed;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.LearnView"/> class.
        /// </summary>
        public LearnView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setups the view for a given letter.
        /// </summary>
        public void Setup(Letter letter)
        {
            letterLabel.Text = letter.Display;
            transliterationLabel.Text = letter.TransDisplay;
            pronounciationLabel.Text = letter.PronounciationTip;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback when the proceed button is clicked.
        /// </summary>
        private void OnProceedButtonClicked(object sender, EventArgs e)
        {
            OnProceed?.Invoke();
        }

        #endregion
    }
}
