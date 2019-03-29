using System.Collections.Generic;
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

        #region Variables

        /// <summary>A list of lesson buttons.</summary>
        private List<Button> lessonButtons;

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

            System.Diagnostics.Debug.WriteLine($"PlayerDataManager.HighestLessonIndexCompleted: {PlayerDataManager.HighestLessonIndexCompleted}");
            System.Diagnostics.Debug.WriteLine($"PlayerDataManager.CurrentLessonIndex: {PlayerDataManager.CurrentLessonIndex}");

            //add lesson buttons
            lessonButtons = new List<Button>();
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

                //add button to list
                lessonButtons.Add(lessonButton);
            }
        }

        /// <summary>
        /// Updates whether buttons are enabled (depending on player progress).
        /// </summary>
        private void UpdateButtons()
        {
            for (int i = 0; i < lessonButtons.Count; i++)
            {
                //determine if the button is enabled
                lessonButtons[i].IsEnabled = i <= PlayerDataManager.HighestLessonIndexCompleted + 1;
            }

            //the practice button is available after the first lesson is completed
            practiceButton.IsEnabled = PlayerDataManager.HighestLessonIndexCompleted >= 0;
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

            System.Diagnostics.Debug.WriteLine("here");

            UpdateButtons();

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
            PlayerDataManager.CurrentLessonIndex = index;

            //start lesson
            Navigation.PushModalAsync(new LessonPage());
        }

        /// <summary>
        /// Callback when the practice button was clicked.
        /// </summary>
        private void OnPracticeButtonClicked(object sender, System.EventArgs eventArgs)
        {
            //disable all interaction
            IsEnabled = false;

            //start practice
            Navigation.PushModalAsync(new PracticePage());
        }

        /// <summary>
        /// Callback when the flashcard button was clicked.
        /// </summary>
        private void OnFlashcardsButtonClicked(object sender, System.EventArgs eventArgs)
        {
            //disable all interaction
            IsEnabled = false;

            //push an alphabet page
            Navigation.PushAsync(new AlphabetPage());
        }

        #endregion
    }
}
