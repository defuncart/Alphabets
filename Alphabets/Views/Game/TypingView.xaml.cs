using Alphabets.Models;
using DeFuncArt.Audio;
using DeFuncArt.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used when typing (ie transliterating a word).
    /// </summary>
    public partial class TypingView : ContentView
    {
        #region Events

        /// <summary>
        /// An event which occurs when the player wants to proceed.
        /// </summary>
        public Action OnProceed;

        #endregion

        #region Variables

        /// <summary>An array of answer buttons.</summary>
        private Button[] answerButtons;

        /// <summary>A random number generator.</summary>
        private Random random;

        /// <summary>The correct answer as a string.</summary>
        private string correctAnswerString;

        #endregion

        #region Properties

        /// <summary>The number of answer buttons.</summary>
        private int NumberAnswerButtons => answerButtons.Length;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.TypingView"/> class.
        /// </summary>
        public TypingView()
        {
            //initialize components
            InitializeComponent();

            //variables
            random = new Random();
            answerButtons = new Button[] { answerButton0, answerButton1, answerButton2, answerButton3, answerButton4, answerButton5, answerButton6, answerButton7 };

            //delegates
            for (int i = 0; i < NumberAnswerButtons; i++)
            {
                int temp = i;
                answerButtons[i].Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(temp);
            }
        }

        /// <summary>
        /// Setups the view for a given word.
        /// </summary>
        public void Setup(Word word, Letter[] quizLetters, bool isAlphabetToTrans)
        {
            //set variables
            correctAnswerString = isAlphabetToTrans ? word.Transliteration : word.Original;

            //update ui
            wordLabel.Text = isAlphabetToTrans ? word.Original : word.Transliteration;
            tipLabel.Text = word.Tip;
            typedLabel.Text = "";

            //determine the letters to display on screen
            Letter[] displayLetters = new Letter[NumberAnswerButtons];
            int index = 0;
            foreach (char c in word.Original)
            {
                displayLetters[index] = quizLetters.First(x => x.LowerCase[0] == c);
                index++;
            }
            for (int i = index; i < NumberAnswerButtons; i++)
            {
                displayLetters[i] = quizLetters[random.Next(quizLetters.Length)];
            }
            displayLetters.Shuffle();

            //update answer buttons
            for (int i = 0; i < NumberAnswerButtons; i++)
            {
                answerButtons[i].Text = isAlphabetToTrans ? displayLetters[i].TransLowerCase : displayLetters[i].LowerCase;
                answerButtons[i].IsEnabled = answerButtons[i].IsVisible = true;
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback when an answer button is pressed.
        /// </summary>
        /// <param name="index">The button index.</param>
        private void OnAnswerButtonClicked(int index)
        {
            typedLabel.Text += answerButtons[index].Text;
            answerButtons[index].IsEnabled = false;
            answerButtons[index].IsVisible = false;

            //check if correct answer
            if(typedLabel.Text == correctAnswerString)
            {
                //TODO
                bool correct = true;

                //audio feedback
                //TODO hardcoded volume
                AudioManager.Play(filename: correct ? "SFX.answerCorrect" : "SFX.answerIncorrect", volume: 0.15f);

                //proceed to next lesson part
                OnProceed?.Invoke();
            }
        }

        #endregion
    }
}
