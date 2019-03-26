using Alphabets.Helpers;
using Alphabets.Managers;
using Alphabets.Models;
using Alphabets.Views.Game;
using DeFuncArt.Localization;
using Xamarin.Forms;

namespace Alphabets.Views
{
    /// <summary>
    /// The lesson page where the player learns letters and is quized on them.
    /// </summary>
    public partial class LessonPage : GamePage
    {
        #region Properties

        /// <summary>The navigation bar.</summary>
        protected override NavView NavBar => navBar;

        /// <summary>A ContentView where the current lesson part is rendered.</summary>
        protected override ContentView GameView => contentView;

        /// <summary>The current lesson part.</summary>
        protected override Lesson lesson => CourseManager.CurrentCourse.Lessons[UserSettings.CurrenLessonIndex];

        /// <summary>The results view title.</summary>
        protected override string ResultsViewTitle => string.Format(LocalizationManager.GetValue("main_lesson_namevalue"), UserSettings.CurrenLessonIndex + 1);

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.LessonPage"/> class.
        /// </summary>
        public LessonPage()
        {
            //initialize components
            InitializeComponent();

            //initialize game page
            InitializeGamePage();
        }
    }
}
