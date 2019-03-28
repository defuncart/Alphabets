using System.Collections.Generic;

namespace Alphabets.Models.JSON
{
    /// <summary>
    /// A model representing the save data for a player.
    /// </summary>
    public class PlayerData
    {
        /// <summary>
        /// A dictionary of CoursesData reference by id.
        /// </summary>
        public Dictionary<string, CourseData> CoursesData;
    }

    /// <summary>
    /// A model representing the save data for a course.
    /// </summary>
    public class CourseData
    {
        /// <summary>
        /// A dictionary of LetterData reference by id.
        /// </summary>
        public Dictionary<string, LetterData> LettersData;
    }

    /// <summary>
    /// A model representing the save data for a letter.
    /// </summary>
    public class LetterData
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
