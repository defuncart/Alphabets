using Alphabets.Models;
using DeFuncArt.Audio;
using System;
using Xamarin.Forms;
using ResourceDictionary = Alphabets.Helpers.ResourceDictionary;

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
            //update ui
            letterLabel.Text = letter.Display;
            transliterationLabel.Text = letter.TransDisplay;
            pronounciationLabel.FormattedText = FormatPronounciationTip(letter.PronounciationTip);

            //play letter pronounciation
            AudioManager.Play(letter.ResourceId, "mp3");
        }

        /// <summary>
        /// Formats the prounciation tip into a FormattedString.
        /// </summary>
        private FormattedString FormatPronounciationTip(string pronounciationTip)
        {
            FormattedString formattedString = new FormattedString();

            //loop through all sections of the string separated with *
            string[] sections = pronounciationTip.Split('*');
            for (int i = 0; i < sections.Length; i++)
            {
                Span span = new Span { Text = sections[i] };
                //format all text between * to be a different color and bold
                span.TextColor = i % 2 == 0 ? ResourceDictionary.GetColor("Color.MidGray") : ResourceDictionary.GetColor("Color.DarkGray1");
                span.FontAttributes = i % 2 == 0 ? FontAttributes.None : FontAttributes.Bold;
                formattedString.Spans.Add(span);
            }

            return formattedString;
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
