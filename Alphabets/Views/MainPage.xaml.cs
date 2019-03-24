using Alphabets.Helpers;
using Alphabets.Managers;
using DeFuncArt.Localization;
using Xamarin.Forms;
using ResourceDictionary = Alphabets.Helpers.ResourceDictionary;

namespace Alphabets.Views
{
    /// <summary>
    /// The main page where the player lands after the loading screen.
    /// Here the player can choose which lesson to play, goto settings etc.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        #region Constants

        /// <summary>The number elements per lesson row.</summary>
        private const int NUMBER_ELEMENTS_PER_LESSON_ROW = 2;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            //initialize all components
            InitializeComponent();

            //TODO debug
            CourseManager.Log();
            PlayerDataManager.Log();

            //constuct UI
            Title = CourseManager.CurrentCourse.Title;

            //add lesson buttons
            for (int i = 0; i < CourseManager.CurrentCourse.Lessons.Length; i++)
            {
                Button lessonButton = new Button();
                lessonButton.Text = string.Format(LocalizationManager.GetValue("main_lesson_namevalue"), i + 1);
                lessonButton.Style = ResourceDictionary.GetStyle("Style.Button.MainPage");

                int temp = i;
                lessonButton.Clicked += (object sender, System.EventArgs e) => OnLesssonButtonClicked(temp);

                //determine (row, column) position
                int row = i / NUMBER_ELEMENTS_PER_LESSON_ROW; int column = i % NUMBER_ELEMENTS_PER_LESSON_ROW;

                //add button to grid
                gridLayout.Children.Add(lessonButton, column, row);
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback before the page becomes visible.
        /// </summary>
        protected override void OnAppearing()
        {
            //call base implementaiton
            base.OnAppearing();

            //enabled all interaction
            IsEnabled = true;
        }

        /// <summary>
        /// Callback when a lesson button was clicked.
        /// </summary>
        /// <param name="index">The button's index.</param>
        private void OnLesssonButtonClicked(int index)
        {
            //disable all interaction
            IsEnabled = false;

            //update current lesson index
            UserSettings.CurrenLessonIndex = index;

            //start lesson
            Navigation.PushModalAsync(new LessonPage());
        }

        #endregion
    }
}
