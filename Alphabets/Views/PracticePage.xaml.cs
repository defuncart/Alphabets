using Alphabets.Enums;
using Alphabets.Models;
using Alphabets.Views.Game;
using DeFuncArt.Extensions;
using DeFuncArt.Localization;
using System.Collections.Generic;
using System.Linq;
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
        protected override string ResultsViewTitle => LocalizationManager.GetValue("main_practice_name");

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
            //determine weak letters
            //TODO hardcoded
            List<string> letterIds = Managers.PlayerDataManager.GetWeakestLetters(3);
            System.Diagnostics.Debug.WriteLine(DeFuncArt.Helpers.DebugHelper.EnumerableToString(letterIds));
            List<Letter> letters = new List<Letter>();
            foreach (string letterId in letterIds)
            {
                letters.Add(alphabet.Letters.First(x => x.ResourceId == letterId));
            }

            //determine words
            //TODO hardcoded
            System.Func<Word, bool> predicate = word => word.Lesson <= Managers.PlayerDataManager.HighestLessonIndexCompleted &&
                                                        (word.Letters.Contains(letters[0]) || word.Letters.Contains(letters[1]) || word.Letters.Contains(letters[2]));
            IEnumerable<Word> words = Managers.CourseManager.CurrentCourse.Words.Where(predicate);
            words = words.Shuffle().Take(4);

            //TODO hardcoded
            //construct lesson
            List<LessonPart> lessonParts = new List<LessonPart>();
            int index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[0]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.Learning, index: index));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[1]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.Learning, index: index));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[2]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.Learning, index: index));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Words.IndexOf(words.ElementAt(0));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.WordAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Words.IndexOf(words.ElementAt(1));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.WordAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[0]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCTransliterationToAlphabet, index: index));
            index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[1]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCTransliterationToAlphabet, index: index));
            index = Managers.CourseManager.CurrentCourse.Alphabet.Letters.IndexOf(letters[2]);
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.MCTransliterationToAlphabet, index: index));
            index = Managers.CourseManager.CurrentCourse.Words.IndexOf(words.ElementAt(2));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.WordAlphabetToTransliteration, index: index));
            index = Managers.CourseManager.CurrentCourse.Words.IndexOf(words.ElementAt(3));
            lessonParts.Add(new LessonPart(lessonPartType: LessonPartType.WordAlphabetToTransliteration, index: index));

            //firstly create the lesson
            //use the cummulativeLetters of the highest lesson index reached by the player
            practiceLesson = new Lesson(Managers.CourseManager.CurrentCourse.Lessons[Managers.PlayerDataManager.HighestLessonIndexCompleted].CumulativeLetters, lessonParts.ToArray());

            //then call base implementation
            base.Reset();
        }
    }
}
