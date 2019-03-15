using Alphabets.Helpers;
using System.Collections.Generic;

namespace Alphabets.Models
{
    /// <summary>
    /// A model representing a letter.
    /// </summary>
    public class Letter
    {
        #region Variables

        /// <summary>
        /// The cases (i.e. upper and lower) for the letter.
        /// </summary>
        private string[] cases;

        /// <summary>
        /// The transliterated cases (i.e. upper and lower) for the letter.
        /// </summary>
        private string[] transCases;

        //TODO add string[] alternativeLetters, string[] cursiveCases

        /// <summary>
        /// A dictionary of pronounciation tips per localized language.
        /// This is stored here instead of in localized resources as each alphabet has various number of letters etc.
        /// </summary>
        private Dictionary<string, string> pronounciationTips;

        /// <summary>
        //  The audio resource identifier.
        /// </summary>
        private string resourceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Letter"/> class.
        /// </summary>
        public Letter(string[] cases, string[] transCases, Dictionary<string, string> pronounciationTips, string resourceId)
        {
            this.cases = cases;
            this.transCases = transCases;
            this.pronounciationTips = pronounciationTips;
            this.resourceId = resourceId;

            //initialize display strings
            Display = HasTwoCases ? $"{UpperCase}{LowerCase}" : $"{LowerCase}";
            TransDisplay = HasTwoCases ? $"{TransUpperCase}{TransLowerCase}" : $"{TransLowerCase}";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Whether the letter has two cases (i.e both upper and lower).
        /// </summary>
        public bool HasTwoCases => cases.Length > 1;

        /// <summary>
        /// The lower case version of the letter. This always has a value.
        /// </summary>
        public string LowerCase => cases[HasTwoCases ? 1 : 0];

        /// <summary>
        /// The upper case version of the letter. This may be null (eg Georgian).
        /// </summary>
        public string UpperCase => HasTwoCases ? cases[1] : null;

        /// <summary>
        /// The transliterated lower case version of the letter. This always has a value.
        /// </summary>
        public string TransLowerCase => transCases[HasTwoCases ? 1 : 0];

        /// <summary>
        /// The transliterated upper case version of the letter. This may be null (eg Georgian).
        /// </summary>
        public string TransUpperCase => HasTwoCases ? transCases[1] : null;

        /// <summary>
        /// The letter(s) is a display format, i.e. "A a".
        /// </summary>
        public string Display { get; }

        /// <summary>
        /// The transliterated letter(s) is a display format, i.e. "A a".
        /// </summary>
        public string TransDisplay { get; }

        /// <summary>
        /// A localized pronounciation tip.
        /// </summary>
        public string PronounciationTip => pronounciationTips[UserSettings.Language];

        /// <summary>
        /// The audio resource identifier.
        /// </summary>
        public string ResourceId => resourceId;

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Letter"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Letter"/>.</returns>
        public override string ToString()
        {
            return $"letter = [{Display}, {TransDisplay}, {PronounciationTip}]";
        }

        #endregion
    }
}
