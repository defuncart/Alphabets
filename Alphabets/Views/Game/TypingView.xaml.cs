using Alphabets.Models;
using DeFuncArt.Audio;
using DeFuncArt.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>A list of buttons (i.e. letters) pressed.</summary>
        protected List<Button> buttonPressedList;

        #endregion

        #region Properties

        /// <summary>The number of answer buttons.</summary>
        private int NumberAnswerButtons => answerButtons.Length;

        /// <summary>Whether the current typed answer is correct.</summary>
        private bool IsTypedAnswerCorrect => typedLabel.Text == correctAnswerString;

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
            buttonPressedList = new List<Button>();

            //delegates
            for (int i = 0; i < NumberAnswerButtons; i++)
            {
                int temp = i;
                answerButtons[i].Clicked += (object sender, EventArgs e) => OnAnswerButtonClicked(temp);
            }
            backspaceButton.Clicked += (object sender, EventArgs e) => OnBackspaceButton();
            enterButton.Clicked += (object sender, EventArgs e) => OnEnterButton();
        }

        /// <summary>
        /// Setups the view for a given word.
        /// </summary>
        public void Setup(Word word, Letter[] quizLetters, bool isAlphabetToTrans)
        {
            //set variables
            correctAnswerString = isAlphabetToTrans ? word.Transliteration : word.Original;

            //reset variables
            buttonPressedList.Clear();

            //update ui
            wordLabel.Text = isAlphabetToTrans ? word.Original : word.Transliteration;
            tipLabel.Text = word.Tip;
            typedLabel.Text = "";
            backspaceButton.SetEnabledVisible(false);
            enterButton.SetEnabledVisible(true);

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
                answerButtons[i].SetEnabledVisible(true);
            }

            //finally set interaction to be enabled
            IsEnabled = true;
        }

        /// <summary>
        /// Updates the UI after the question was answered.
        /// </summary>
        private void AnsweredCorrectly(bool correctly)
        {
            //set interaction to be disabled
            IsEnabled = false;

            //if not correct, display correct answer
            if (!correctly) { typedLabel.Text = correctAnswerString; }

            //hide all buttons
            foreach (Button answerButton in answerButtons)
            {
                answerButton.IsVisible = false;
            }
            backspaceButton.IsVisible = enterButton.IsVisible = false;

            //TODO visual feedback that answer was correct/incorrect

            //audio feedback
            //TODO hardcoded volume
            AudioManager.Play(filename: correctly ? "SFX.answerCorrect" : "SFX.answerIncorrect", volume: 0.15f);

            //proceed to next lesson part
            OnProceed?.Invoke();
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback when an answer button is pressed.
        /// </summary>
        /// <param name="index">The button index.</param>
        private void OnAnswerButtonClicked(int index)
        {
            //remove the button and update typed text
            typedLabel.Text += answerButtons[index].Text;
            answerButtons[index].SetEnabledVisible(false);
            buttonPressedList.Add(answerButtons[index]);

            //if the answer is correct, automatically proceed
            if (IsTypedAnswerCorrect)
            {
                AnsweredCorrectly(true);
            }
            else //otherwise, ensure that the backspace button is visible
            {
                backspaceButton.SetEnabledVisible(true);
            }
        }

        /// <summary>
        /// Callback when the enter button is pressed.
        /// </summary>
        private void OnEnterButton()
        {
            AnsweredCorrectly(IsTypedAnswerCorrect);
        }

        /// <summary>
        /// Callback when the backspace button is pressed.
        /// </summary>
        private void OnBackspaceButton()
        {
            //reactivate the last pressed answerButton
            Button temp = buttonPressedList.Last();
            temp.SetEnabledVisible(true);
            buttonPressedList.RemoveAt(buttonPressedList.Count - 1);

            //re-calculate the typed word
            StringBuilder sb = new StringBuilder();
            foreach (Button button in buttonPressedList)
            {
                sb.Append(button.Text);
            }
            typedLabel.Text = sb.ToString();

            //set whether the backspace button should be displayed
            backspaceButton.SetEnabledVisible(buttonPressedList.Count > 0);
        }

        #endregion
    }
}
