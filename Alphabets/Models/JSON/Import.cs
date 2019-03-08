using System;
using System.Collections.Generic;

namespace Alphabets.Models.JSON
{
    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Alphabet"/>.
    /// </summary>
    [Serializable]
    public class AlphabetImport
    {
        /// <summary>
        /// The alphabet's name.
        /// </summary>
        public string Name;

        /// <summary>
        /// The alphabet's letters.
        /// </summary>
        public LetterImport[] Letters;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Letter"/>.
    /// </summary>
    [Serializable]
    public class LetterImport
    {
        /// <summary>
        /// The cases (i.e. upper and lower) for the letter.
        /// </summary>
        public string[] Cases;

        /// <summary>
        /// The transliterated cases (i.e. upper and lower) for the letter.
        /// </summary>
        public string[] TransCases;

        /// <summary>
        /// A dictionary of pronounciation tips per localized language.
        /// </summary>
        public Dictionary<string, string> PronounciationTips;
    }
}
