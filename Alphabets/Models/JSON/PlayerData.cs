namespace Alphabets.Models.JSON
{
    /// <summary>
    /// A model representing the save data for a player.
    /// </summary>
    public class PlayerData
    {
        /// <summary>
        /// An array of alphabets.
        /// </summary>
        public AlphabetSaveData[] Alphabets;
    }

    /// <summary>
    /// A model representing the save data for an alphabet.
    /// </summary>
    public class AlphabetSaveData
    {
        /// <summary>
        /// An array of letters.
        /// </summary>
        public LetterSaveData[] Letters;
    }

    /// <summary>
    /// A model representing the save data for a letter.
    /// </summary>
    public class LetterSaveData
    {
        /// <summary>
        /// The letter's identifier.
        /// </summary>
        public string Id;

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
