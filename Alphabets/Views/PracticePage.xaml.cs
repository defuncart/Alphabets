using Alphabets.Enums;
using Alphabets.Models;
using Alphabets.Views.Game;
using Xamarin.Forms;

namespace Alphabets.Views
{
    /// <summary>
    /// The practice page where the player is quized on letters and words.
    /// </summary>
    public partial class PracticePage : GamePage
    {
        #region Variables

        /// <summary>A lesson constructed for the current practice session.</summary>
        private Lesson practiceLesson;

        #endregion

        #region Properties

        /// <summary>The navigation bar.</summary>
        protected override NavView NavBar => navBar;

        /// <summary>A ContentView where the current lesson part is rendered.</summary>
        protected override ContentView GameView => contentView;

        /// <summary>The current lesson part.</summary>
        protected override Lesson lesson => practiceLesson;

        /// <summary>The results view title.</summary>
        protected override string ResultsViewTitle => "TEMP";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.PracticePage"/> class.
        /// </summary>
        public PracticePage()
        {
            //initialize components
            InitializeComponent();

            //initialize game page
            InitializeGamePage();
        }
        /// <summary>
        /// Resets the game.
        /// </summary>
        protected override void Reset()
        {
            //TODO hardcoded for debug

            LessonPart[] lessonParts = new LessonPart[3];
            lessonParts[0] = new LessonPart(lessonPartType: LessonPartType.Learning, index: 0);
            lessonParts[1] = new LessonPart(lessonPartType: LessonPartType.MCAlphabetToTransliteration, index: 0);
            lessonParts[2] = new LessonPart(lessonPartType: LessonPartType.WordAlphabetToTransliteration, index: 0);

            //firstly create the lesson
            practiceLesson = new Lesson(new int[] { 0, 8, 10, 11 }, lessonParts);

            //then call base implementation
            base.Reset();
        }
    }
}
