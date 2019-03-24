using Alphabets.Models;
using DeFuncArt.Audio;
using Xamarin.Forms;
using ResourceDictionary = Alphabets.Helpers.ResourceDictionary;

namespace Alphabets.Views.Common
{
    /// <summary>
    /// A view used to display info about a letter.
    /// </summary>
    public partial class LetterView : ContentView
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Common.LetterView"/> class.
        /// </summary>
        public LetterView()
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
    }
}
