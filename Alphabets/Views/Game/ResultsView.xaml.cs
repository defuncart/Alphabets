using Alphabets.Managers;
using Alphabets.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Alphabets.Views.Game
{
    /// <summary>
    /// A view used in GamePage at the end of a lesson.
    /// </summary>
    public partial class ResultsView : ContentView
    {
        #region Events

        /// <summary>
        /// An event which occurs when the player wants to proceed.
        /// </summary>
        public Action OnProceed;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Views.Game.ResultsView"/> class.
        /// </summary>
        public ResultsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setups the view for a list of letters.
        /// </summary>
        public void SetUp(List<Letter> letters)
        {
            //create a collection of result models
            ObservableCollection<ResultModel> listContents = new ObservableCollection<ResultModel>();
            foreach(Letter letter in letters)
            {
                listContents.Add(new ResultModel {  LetterAlphabet = letter.Display, 
                                                    LetterTransliteration = letter.TransDisplay, 
                                                    Ratio = PlayerDataManager.GetCorrectRatioForLetter(letter)  });
            }

            //set the list contents
            listView.ItemsSource = listContents;

            //set the row height (list height divided equally by number of rows)
            listView.RowHeight = (int)(listView.Height / (listContents.Count * 1.0));
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Callback when the proceed button is clicked.
        /// </summary>
        private void OnProceedButtonClicked(object sender, EventArgs e)
        {
            OnProceed?.Invoke();
        }

        #endregion
    }

    /// <summary>
    /// A model used to display the results of a letter.
    /// </summary>
    public class ResultModel
    {
        /// <summary>The letter in the alphabet to learn (i.e. ა).</summary>
        public string LetterAlphabet { get; set; }

        /// <summary>The transliterated letter (i.e. a).</summary>
        public string LetterTransliteration { get; set; }

        /// <summary>The letter's correct ratio (between 0 and 1).</summary>
        public double Ratio { get; set; }
    }
}
