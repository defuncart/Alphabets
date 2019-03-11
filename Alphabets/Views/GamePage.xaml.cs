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

        private LearnView learnView;
        private MultipleChoiceView multipleChoiceView;

        private int lessonPartIndex = 0;
        private ContentView currentContentView;

        #endregion

        #region Properties

        //TODO refactor to GameManager?
        private Lesson lesson => CourseManager.Course.Lessons[UserSettings.CurrenLessonIndex];

        private LessonPart lessonPart => lesson.LessonParts[lessonPartIndex];

        private int numberLessonParts => lesson.LessonParts.Length;

        private Letter[] quizLetters;

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


        public void SetupNextLessonPart()
        {
            //dertermine current letter
            Letter letter = AlphabetManager.Alphabet.Letters[lessonPart.Letter];

            //
            switch (lessonPart.LessonPartType)
            {
                case LessonPartType.Learning:
                    learnView.Setup(letter);
                    currentContentView = learnView;
                    break;

                case LessonPartType.MCAlphabetToTransliteration:
                    multipleChoiceView.Setup(letter: letter, quizLetters: quizLetters, isAlphabetToTrans: true);
                    currentContentView = multipleChoiceView;
                    break;

                case LessonPartType.MCTransliterationToAlphabet:
                    multipleChoiceView.Setup(letter: letter, quizLetters: quizLetters, isAlphabetToTrans: false);
                    currentContentView = multipleChoiceView;
                    break;
            }

            //update view
            contentView.Content = currentContentView;
            navBar.Progress = lessonPartIndex / (numberLessonParts * 1.0);
        }

        #region Callbacks

        private void OnProceedToNextLessonPart()
        {
            if(++lessonPartIndex < numberLessonParts)
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
