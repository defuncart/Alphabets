namespace Alphabets.Models
{
    /// <summary>
    /// A model representing a lesson.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// An array of letters already learned and taught in this lesson.
        /// Used for multiple choice (when choosing incorrect answers).
        /// </summary>
        public int[] CumulativeLetters { get; }

        /// <summary>
        /// An array of lesson parts.
        /// </summary>
        public LessonPart[] LessonParts { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Lesson"/> class.
        /// </summary>
        public Lesson(int[] cumulativeLetters, LessonPart[] lessonParts)
        {
            CumulativeLetters = cumulativeLetters;
            LessonParts = lessonParts;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Lesson"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Lesson"/>.</returns>
        public override string ToString()
        {
            return $"Lesson has {LessonParts.Length} lessons.";
        }
    }
}
