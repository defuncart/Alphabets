using Alphabets.Enums;
using Alphabets.Managers;
using Alphabets.Models;
using Alphabets.Views.Game;
using Xamarin.Forms;
using Alphabets.Helpers;

namespace Alphabets.Views
{
    /// <summary>
    /// The game page where the player learns letters and is quized on them.
    /// </summary>
    public partial class GamePage : ContentPage
    {
        #region Variables

        /// <summary>An instantiated learn view used to teach a new letter.</summary>
        private LearnView learnView;

        /// <summary>An instantiated multiple choice view used to quiz a learned letter.</summary>
        private MultipleChoiceView multipleChoiceView;

        /// <summary>The index of the current lesson part.</summary>
        private int lessonPartIndex = 0;

        /// <summary>An array of quiz letters valid for this lesson.</summary>
        private Letter[] quizLetters;

        #endregion

        #region Properties

        //TODO refactor to GameManager?
        /// <summary>The current lesson.</summary>
        private Lesson lesson => CourseManager.Course.Lessons[UserSettings.CurrenLessonIndex];

        /// <summary>The current lesson part.</summary>
        private LessonPart lessonPart => lesson.LessonParts[lessonPartIndex];

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

            //delegates
            navBar.ExitButton.Clicked += (object sender, System.EventArgs e) => NavBar_OnExitButtonClicked();
            learnView.OnProceed += OnProceedToNextLessonPart;
            multipleChoiceView.OnProceed += OnProceedToNextLessonPart;

            //TODO linq
            //determine quiz letters
            quizLetters = new Letter[lesson.CumulativeLetters.Length];
            for (int i = 0; i < quizLetters.Length; i++)
            {
                quizLetters[i] = AlphabetManager.Alphabet.Letters[lesson.CumulativeLetters[i]];
            }

            //setup the next lesson part
            SetupNextLessonPart();
        }

        /// <summary>
        /// Setups the next lesson part.
        /// </summary>
        private void SetupNextLessonPart()
        {
            //determine current letter
            Letter letter = AlphabetManager.Alphabet.Letters[lessonPart.Letter];

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

            //update progress bar
            navBar.Progress = lessonPartIndex / (numberLessonParts * 1.0);
        }

        #region Callbacks

        /// <summary>
        /// Callback when the next lesson part should be proceeded to.
        /// </summary>
        private void OnProceedToNextLessonPart()
        {
            if (++lessonPartIndex < numberLessonParts)
            {
                SetupNextLessonPart();
            }
            else
            {
                //TODO

                NavBar_OnExitButtonClicked();
            }
        }

        /// <summary>
        /// Callback when the exit button the the nav bar is pressed.
        /// </summary>
        private void NavBar_OnExitButtonClicked()
        {
            //TODO display warning popup

            Navigation.PopModalAsync();
        }

        #endregion
    }
}
