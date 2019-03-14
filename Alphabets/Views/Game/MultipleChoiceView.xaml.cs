using Alphabets.Models;
using DeFuncArt.Extensions;
using System;
using System.Threading.Tasks;
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

        private Random random;

        private int correctAnswerButtonIndex;

        //TODO hardcoded
        private const int NUM_ANSWER_BUTTONS = 4;

        private Button[] answerButtons;

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

        public void Setup(Letter letter, Letter[] quizLetters, bool isAlphabetToTrans)
        {
            //determine correct answer index
            correctAnswerButtonIndex = random.Next(NUM_ANSWER_BUTTONS);

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
        async private void OnAnswerButtonClicked(int index)
        {
            bool correct = index == correctAnswerButtonIndex;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i == correctAnswerButtonIndex)
                {
                    answerButtons[i].Style = ResourceDictionary.GetStyle("Style.Button.MultipleChoice.Correct");
                }
                else if (!correct && i == index)
                {
                    answerButtons[i].Style = ResourceDictionary.GetStyle("Style.Button.MultipleChoice.Incorrect");
                }
                else
                {
                    answerButtons[i].IsEnabled = answerButtons[i].IsVisible = false;
                }
            }

            await Task.Delay(2000);

            OnProceed?.Invoke();
        }

        #endregion
    }
}
