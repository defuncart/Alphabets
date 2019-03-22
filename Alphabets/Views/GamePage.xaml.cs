using Alphabets.Enums;
using Alphabets.Helpers;
using Alphabets.Managers;
using Alphabets.Models;
using Alphabets.Views.Game;
using DeFuncArt.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alphabets.Views
{
    /// <summary>
    /// The game page where the player learns letters and is quized on them.
    /// </summary>
    public partial class GamePage : ContentPage
    {
        #region Constants

        /// <summary>The duration of the progress bar.</summary>
        private const int PROGRESS_BAR_DURATION = 250;

        /// <summary>Animation delays for lesson part types.</summary>
        private readonly Dictionary<LessonPartType, int> ANIMATION_DELAYS = new Dictionary<LessonPartType, int>()
        {
            { LessonPartType.Learning, 0 },
            { LessonPartType.MCAlphabetToTransliteration, 1750 },
            { LessonPartType.MCTransliterationToAlphabet, 1750 }
        };

        #endregion

        #region Variables

        /// <summary>An instantiated learn view used to teach a new letter.</summary>
        private LearnView learnView;

        /// <summary>An instantiated multiple choice view used to quiz a learned letter.</summary>
        private MultipleChoiceView multipleChoiceView;

        /// <summary>An instantiated results view used at the end of a lesson.</summary>
        private ResultsView resultsView;

        /// <summary>The index of the current lesson part.</summary>
        private int lessonPartIndex;

        /// <summary>A list of letters learned this lesson.</summary>
        private List<Letter> learnedLetters;

        /// <summary>An array of quiz letters valid for this lesson.</summary>
        private Letter[] quizLetters;

        #endregion

        #region Properties

        //TODO refactor to GameManager?
        /// <summary>The current alphabet.</summary>
        private Alphabet alphabet => CourseManager.CurrentCourse.Alphabet;

        /// <summary>The current lesson.</summary>
        private Lesson lesson => CourseManager.CurrentCourse.Lessons[UserSettings.CurrenLessonIndex];

        /// <summary>The current lesson part.</summary>
        private LessonPart lessonPart => lesson.LessonParts[lessonPartIndex];

        /// <summary>The current lesson part type.</summary>
        private LessonPartType lessonPartType => lessonPart.LessonPartType;

        /// <summary>The total numner of lesson parts in this lesson.</summary>
        private int numberLessonParts => lesson.LessonParts.Length;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.GamePage"/> class.
        /// </summary>
        public GamePage()
        {
            //initialize components
            InitializeComponent();

            //create subviews
            learnView = new LearnView();
            multipleChoiceView = new MultipleChoiceView();
            resultsView = new ResultsView();

            //delegates
            navBar.ExitButton.Clicked += (object sender, System.EventArgs e) => NavBar_OnExitButtonClicked();
            learnView.OnProceed += OnProceedToNextLessonPart;
            multipleChoiceView.OnProceed += OnProceedToNextLessonPart;
            resultsView.OnProceed += ResultsView_OnProceed;

            //instantiate variables
            learnedLetters = new List<Letter>();

            //reset the game
            Reset();
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        private void Reset()
        {
            //initialize variables
            lessonPartIndex = 0;
            learnedLetters.Clear();

            //TODO linq
            //determine quiz letters
            quizLetters = new Letter[lesson.CumulativeLetters.Length];
            for (int i = 0; i < quizLetters.Length; i++)
            {
                quizLetters[i] = alphabet.Letters[lesson.CumulativeLetters[i]];
            }

            //initialize ui
            navBar.SetProgress(0);

            //setup the next lesson part
            SetupNextLessonPart();
        }

        /// <summary>
        /// Setups the next lesson part.
        /// </summary>
        private void SetupNextLessonPart()
        {
            //determine current letter
            Letter letter = alphabet.Letters[lessonPart.Letter];
            if (!learnedLetters.Contains(letter)) { learnedLetters.Add(letter); }

            //setup view
            switch (lessonPart.LessonPartType)
            {
                case LessonPartType.Learning:
                    learnView.Setup(letter);
                    contentView.Content = learnView;
                    break;

                case LessonPartType.MCAlphabetToTransliteration:
                    multipleChoiceView.Setup(letter: letter, quizLetters: quizLetters, isAlphabetToTrans: true);
                    contentView.Content = multipleChoiceView;
                    break;

                case LessonPartType.MCTransliterationToAlphabet:
                    multipleChoiceView.Setup(letter: letter, quizLetters: quizLetters, isAlphabetToTrans: false);
                    contentView.Content = multipleChoiceView;
                    break;
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
            await navBar.AnimateProgressTo(value: progress, duration: PROGRESS_BAR_DURATION, easing: Easing.Linear);

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
                navBar.SetText(string.Format(LocalizationManager.GetValue("main_lesson_namevalue"), UserSettings.CurrenLessonIndex + 1));
                contentView.Content = resultsView;
                resultsView.SetUp(learnedLetters);
            }
        }

        /// <summary>
        /// Callback when the user wants to proceed from results view.
        /// </summary>
        private void ResultsView_OnProceed()
        {
            //if there are more lessons, preceed to next one
            if (UserSettings.CurrenLessonIndex < CourseManager.CurrentCourse.Lessons.Length - 1)
            {
                PlayerDataManager.WriteToDisk();
                UserSettings.CurrenLessonIndex++;
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
