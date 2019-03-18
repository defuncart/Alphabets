using Alphabets.Models;
using DeFuncArt.Audio;
using DeFuncArt.Extensions;
using System;
using Xamarin.Forms;
using ResourceDictionary = Alphabets.Helpers.ResourceDictionary;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used in GamePage to quiz a learned letter.
    /// </summary>
    public partial class MultipleChoiceView : ContentView
    {
        #region Events

        /// <summary>
        /// An event which occurs when the player wants to proceed.
        /// </summary>
        public Action OnProceed;

        #endregion

        #region Variables

        /// <summary>An animation delay after answer submitted.</summary>
        private const int ANIMATION_DELAY = 2000;

        #endregion

        #region Variables

        /// <summary>An array of answer buttons.</summary>
        private Button[] answerButtons;

        /// <summary>The index of the correct answer button.</summary>
        private int correctAnswerButtonIndex;

        /// <summary>A random number generator.</summary>
        private Random random;

        /// <summary>The current letter.</summary>
        private Letter letter;

        #endregion

        #region Properties

        /// <summary>The number of answer buttons.</summary>
        private int NumberAnswerButtons => answerButtons.Length;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.MultipleChoiceView"/> class.
        /// </summary>
        public MultipleChoiceView()
        {
            //initialize components
            InitializeComponent();

            //delegates
            answerButton0.Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(0);
            answerButton1.Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(1);
            answerButton2.Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(2);
            answerButton3.Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(3);

            //variables
            random = new Random();
            answerButtons = new Button[] { answerButton0, answerButton1, answerButton2, answerButton3 };
        }

        /// <summary>
        /// Sets up the view.
        /// </summary>
        /// <param name="letter">The letter to test.</param>
        /// <param name="quizLetters">An array of letters valid for this lesson.</param>
        /// <param name="isAlphabetToTrans">Whether to test from alphabet to transliterated or vice-versa.</param>
        public void Setup(Letter letter, Letter[] quizLetters, bool isAlphabetToTrans)
        {
            //cache the letter
            this.letter = letter;

            //determine correct answer index
            correctAnswerButtonIndex = random.Next(NumberAnswerButtons);

            //determine the incorrect indices
            int[] incorrectIndices = { -1, -1, -1, -1 };
            incorrectIndices[correctAnswerButtonIndex] = quizLetters.IndexOf(letter);
            for (int i = 0; i < incorrectIndices.Length; i++)
            {
                if (i != correctAnswerButtonIndex)
                {
                    int falseIndex;
                    do
                    {
                        falseIndex = random.Next(quizLetters.Length);
                    } while (incorrectIndices.Contains(falseIndex));
                    incorrectIndices[i] = falseIndex;
                }
            }

            //update ui
            questionLabel.Text = isAlphabetToTrans ? letter.Display : letter.TransDisplay;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].Text = isAlphabetToTrans ? quizLetters[incorrectIndices[i]].TransDisplay : quizLetters[incorrectIndices[i]].Display;
                answerButtons[i].Style = ResourceDictionary.GetStyle("Style.Button.MultipleChoice.Default");
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
            bool correct = index == correctAnswerButtonIndex;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i == correctAnswerButtonIndex) //mark correct answer button as correct
                {
                    answerButtons[i].Style = ResourceDictionary.GetStyle("Style.Button.MultipleChoice.Correct");
                }
                else if (!correct && i == index) //if answered incorrectly, mark button as incorrect
                {
                    answerButtons[i].Style = ResourceDictionary.GetStyle("Style.Button.MultipleChoice.Incorrect");
                }
                else //remove any other buttons from the screen
                {
                    answerButtons[i].IsEnabled = answerButtons[i].IsVisible = false;
                }
            }

            //TODO maybe move to GamePage?
            //update player data
            Managers.PlayerDataManager.UpdateLetter(letter, correct);

            //audio feedback
            //TODO hardcoded volume
            AudioManager.Play(filename: correct ? "SFX.answerCorrect" : "SFX.answerIncorrect", volume: 0.15f);

            //proceed to next lesson part
            OnProceed?.Invoke();
        }

        #endregion
    }
}
