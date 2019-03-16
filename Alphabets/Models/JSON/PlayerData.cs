using System.Collections.Generic;

namespace Alphabets.Models.JSON
{
    /// <summary>
    /// A model representing the save data for a player.
    /// </summary>
    public class PlayerData
    {
        /// <summary>
        /// A dictionary of LetterSaveData reference by id.
        /// </summary>
        public Dictionary<string, LetterSaveData> LettersSaveData;
    }

    /// <summary>
    /// A model representing the save data for a letter.
    /// </summary>
    public class LetterSaveData
    {
        /// <summary>
        /// The times the letter has been attempted (multiple choice and writing).
        /// </summary>
        public int Attempts;

        /// <summary>
        /// The times the letter has been correctly answered (multiple choice and writing).
        /// </summary>
        public int Correct;
    }
}
