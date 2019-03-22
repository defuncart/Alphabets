using Alphabets.Helpers;
using System.Collections.Generic;

namespace Alphabets.Models
{
    /// <summary>
    /// A model representing a word.
    /// </summary>
    public class Word
    {
        #region Variables

        /// <summary>
        /// A dictionary of tips per localized language.
        /// This is stored here instead of in localized resources as each alphabet has various words etc.
        /// </summary>
        private Dictionary<string, string> tips;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Word"/> class.
        /// </summary>
        public Word(string original, string transliteration, Dictionary<string, string> tips)
        {
            Original = original;
            Transliteration = transliteration;
            this.tips = tips;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The original word (written in the alphabet to learn).
        /// </summary>
        public string Original { get; }

        /// <summary>
        /// The transliteration (written using latin alphabet).
        /// </summary>
        public string Transliteration { get; }

        /// <summary>
        /// A localized tip.
        /// </summary>
        public string Tip => tips[UserSettings.Language];

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Letter"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Letter"/>.</returns>
        public override string ToString()
        {
            return $"word = [{Original}, {Transliteration}, {Tip}]";
        }

        #endregion
    }
}
