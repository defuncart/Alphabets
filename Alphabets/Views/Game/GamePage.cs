using Alphabets.Enums;
using Alphabets.Managers;
using Alphabets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// An abstract game page from which LessonPage and PracticePage can inherit from.
    /// </summary>
    public abstract class GamePage : ContentPage
    {
        #region Constants

        /// <summary>The duration of the progress bar.</summary>
        protected const int PROGRESS_BAR_DURATION = 250;

        /// <summary>Animation delays for lesson part types.</summary>
        protected readonly Dictionary<LessonPartType, int> ANIMATION_DELAYS = new Dictionary<LessonPartType, int>()
        {
            { LessonPartType.Learning, 0 },
            { LessonPartType.MCAlphabetToTransliteration, 1750 },
            { LessonPartType.MCTransliterationToAlphabet, 1750 },
            { LessonPartType.WordAlphabetToTransliteration, 1750 }
        };

        #endregion

        #region Variables

        /// <summary>An instantiated learn view used to teach a new letter.</summary>
        protected LearnView learnView;

        /// <summary>An instantiated multiple choice view used to quiz a learned letter.</summary>
        protected MultipleChoiceView multipleChoiceView;

        /// <summary>An instantiated typing view used to quiz a group of learned letters.</summary>
        protected TypingView typingView;

        /// <summary>An instantiated results view used at the end of a lesson.</summary>
        protected ResultsView resultsView;

        /// <summary>The index of the current lesson part.</summary>
        protected int lessonPartIndex;

        /// <summary>A list of letters learned this lesson.</summary>
        protected List<Letter> learnedLetters;

        #endregion

        #region Properties

        /// <summary>The navigation bar.</summary>
        protected abstract NavView NavBar { get; }

        /// <summary>A ContentView where the current lesson part is rendered.</summary>
        protected abstract ContentView GameView { get; }

        /// <summary>The current alphabet.</summary>
        protected Alphabet alphabet => CourseManager.CurrentCourse.Alphabet;

        /// <summary>The current lesson.</summary>
        protected abstract Lesson lesson { get; }

        /// <summary>The current lesson part.</summary>
        protected LessonPart lessonPart => lesson.LessonParts[lessonPartIndex];

        /// <summary>The current lesson part type.</summary>
        protected LessonPartType lessonPartType => lessonPart.LessonPartType;

        /// <summary>Whether the current lesson part is for a letter or word.</summary>
        protected bool isLessonPartLetter => lessonPartType == LessonPartType.Learning || lessonPartType == LessonPartType.MCAlphabetToTransliteration || lessonPartType == LessonPartType.MCTransliterationToAlphabet;

        /// <summary>The total numner of lesson parts in this lesson.</summary>
        protected int numberLessonParts => lesson.LessonParts.Length;

        /// <summary>The results view title.</summary>
        protected abstract string ResultsViewTitle { get; }

        #endregion

        /// <summary>
        /// Initializes the game page.
        /// </summary>
        protected void InitializeGamePage()
        {
            //create subviews
            learnView = new LearnView();
            multipleChoiceView = new MultipleChoiceView();
            typingView = new TypingView();
            resultsView = new ResultsView();

            //delegates
            NavBar.ExitButton.Clicked += (object sender, System.EventArgs e) => NavBar_OnExitButtonClicked();
            learnView.OnProceed += OnProceedToNextLessonPart;
            multipleChoiceView.OnProceed += OnProceedToNextLessonPart;
            typingView.OnProceed += OnProceedToNextLessonPart;
            resultsView.OnProceed += ResultsView_OnProceed;

            //instantiate variables
            learnedLetters = new List<Letter>();

            //reset the game
            Reset();
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        protected virtual void Reset()
        {
            //initialize variables
            lessonPartIndex = 0;
            learnedLetters.Clear();

            //initialize ui
            NavBar.SetProgress(0);

            //setup the next lesson part
            SetupNextLessonPart();
        }

        /// <summary>
        /// Setups the next lesson part.
        /// </summary>
        private void SetupNextLessonPart()
        {
            if (isLessonPartLetter)
            {
                //determine current letter
                Letter letter = alphabet.Letters[lessonPart.Index];
                if (!learnedLetters.Contains(letter)) { learnedLetters.Add(letter); }

                //setup view
                switch (lessonPartType)
                {
                    case LessonPartType.Learning:
                        GameView.Content = learnView;
                        learnView.Setup(letter);
                        break;

                    case LessonPartType.MCAlphabetToTransliteration:
                        GameView.Content = multipleChoiceView;
                        multipleChoiceView.Setup(letter: letter, quizLetters: lesson.CumulativeLetters, isAlphabetToTrans: true);
                        break;

                    case LessonPartType.MCTransliterationToAlphabet:
                        GameView.Content = multipleChoiceView;
                        multipleChoiceView.Setup(letter: letter, quizLetters: lesson.CumulativeLetters, isAlphabetToTrans: false);
                        break;
                }
            }
            else
            {
                Word word = CourseManager.CurrentCourse.Words[lessonPart.Index];
                GameView.Content = typingView;
                typingView.Setup(word: word, quizLetters: lesson.CumulativeLetters, isAlphabetToTrans: true);
            }
        }

        #region Callbacks

        /// <summary>
        /// Callback when the next lesson part should be proceeded to.
        /// </summary>
        async private void OnProceedToNextLessonPart()
        {
            //update progress bar
            double progress = (lessonPartIndex + 1) / (numberLessonParts * 1.0);
            await NavBar.AnimateProgressTo(value: progress, duration: PROGRESS_BAR_DURATION, easing: Easing.Linear);

            //add an delay if needed
            await Task.Delay(ANIMATION_DELAYS[lessonPartType]);

            //determine if the lesson is complete
            if (++lessonPartIndex < numberLessonParts)
            {
                SetupNextLessonPart();
            }
            else
            {
                //HACK need to set content first so that the size of the list's rows can be determined
                NavBar.SetText(ResultsViewTitle);
                GameView.Content = resultsView;
                resultsView.SetUp(learnedLetters);
            }
        }

        /// <summary>
        /// Callback when the user wants to proceed from results view.
        /// </summary>
        private void ResultsView_OnProceed()
        {
            //if there are more lessons, preceed to next one
            if (PlayerDataManager.CurrentLessonIndex < CourseManager.CurrentCourse.Lessons.Length - 1)
            {
                PlayerDataManager.CurrentLessonIndex++;
                PlayerDataManager.WriteToDisk();
                Reset();
            }
            else //otherwise return to MainPage
            {
                NavBar_OnExitButtonClicked();
            }
        }

        /// <summary>
        /// Callback when the exit button the the nav bar is pressed.
        /// </summary>
        private void NavBar_OnExitButtonClicked()
        {
            //TODO display warning popup

            PlayerDataManager.WriteToDisk();
            Navigation.PopModalAsync();
        }

        #endregion
    }
}
