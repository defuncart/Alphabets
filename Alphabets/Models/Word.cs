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
        public Word(string alphabet, string transliterated, Dictionary<string, string> tips)
        {
            Alphabet = alphabet;
            Transliterated = transliterated;
            this.tips = tips;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The word written in the alphabet to learn.
        /// </summary>
        public string Alphabet { get; private set; }

        /// <summary>
        /// The word transliterated (ie written using latin alphabet).
        /// </summary>
        public string Transliterated { get; private set; }

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
            return $"word = [{Alphabet}, {Transliterated}, {Tip}]";
        }

        #endregion
    }
}
