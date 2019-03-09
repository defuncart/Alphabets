using System;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used as a navigation bar in GamePage.
    /// </summary>
    public partial class NavView : ContentView
    {
        /// <summary>
        /// An event which occurs when the exit button is clicked.
        /// </summary>
        public Action OnExitButtonClicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.NavView"/> class.
        /// </summary>
        public NavView()
        {
            //initialize components
            InitializeComponent();

            //delegates
            exitButton.Clicked += (object sender, EventArgs e) => OnExitButtonClicked?.Invoke();
        }
    }
}
