using Alphabets.Managers;
using Alphabets.Models;
using DeFuncArt.Extensions;
using System;
using Xamarin.Forms;

namespace Alphabets.Views
{
    public partial class AlphabetPage : ContentPage
    {
        #region Variables

        /// <summary>The current letter index.</summary>
        private int letterIndex;

        #endregion

        #region Properties

        /// <summary>The letters for the alphabet of the current course.</summary>
        private Letter[] Letters => CourseManager.CurrentCourse.Alphabet.Letters;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.AlphabetPage"/> class.
        /// </summary>
        public AlphabetPage()
        {
            //initialize all components
            InitializeComponent();

            //initialize ui
            Title = CourseManager.CurrentCourse.Title;
            leftButton.Text = "<";
            rightButton.Text = ">";
            UpdateView();
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        private void UpdateView()
        {
            //display current letter
            letterView.Setup(Letters[letterIndex]);

            //determine if left/right buttons should be displayed
            leftButton.SetEnabledVisible(letterIndex > 0);
            rightButton.SetEnabledVisible(letterIndex < Letters.Length - 1);
        }

        /// <summary>
        /// Changes the flashcard by either moving leftwards or rightwards.
        /// </summary>
        private void ChangeFlashcard(bool leftwardsDirection)
        {
            letterIndex = leftwardsDirection ? letterIndex - 1 : letterIndex + 1;
            UpdateView();
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback when the left button is clicked.
        /// </summary>
        private void OnLeftButtonClicked(object sender, EventArgs e)
        {
            ChangeFlashcard(leftwardsDirection: true);
        }

        /// <summary>
        /// Callback when the right button is clicked.
        /// </summary>
        private void OnRightButtonClicked(object sender, EventArgs e)
        {
            ChangeFlashcard(leftwardsDirection: false);
        }

        /// <summary>
        /// Callback when a swipe occured.
        /// </summary>
        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (letterIndex > 0 && e.Direction == SwipeDirection.Right)
            {
                ChangeFlashcard(leftwardsDirection: true);
            }
            else if (letterIndex < Letters.Length - 1 && e.Direction == SwipeDirection.Left)
            {
                ChangeFlashcard(leftwardsDirection: false);
            }
        }

        #endregion
    }
}
