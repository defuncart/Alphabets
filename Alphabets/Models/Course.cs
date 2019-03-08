using Alphabets.Enums;

namespace Alphabets.Models
{
    /// <summary>
    /// A model representing an alphabet.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The alphabet type.
        /// </summary>
        public AlphabetType AlphabetType { get; }

        /// <summary>
        /// The courses's lessons.
        /// </summary>
        public Lesson[] Lessons { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Course"/> class.
        /// </summary>
        public Course(AlphabetType alphabetType, Lesson[] lessons)
        {
            AlphabetType = alphabetType;
            Lessons = lessons;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.</returns>
        public override string ToString()
        {
            return $"Course for {AlphabetType} has {Lessons.Length} lessons";
        }
    }
}
