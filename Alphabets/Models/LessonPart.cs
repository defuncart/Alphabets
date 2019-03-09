using Alphabets.Enums;

namespace Alphabets.Models
{
    /// <summary>
    /// A model representing a part (i.e. visible screen) of a lesson.
    /// </summary>
    public class LessonPart
    {
        /// <summary>
        /// The type of the lesson part.
        /// </summary>
        public LessonPartType LessonPartType { get; }

        /// <summary>
        /// The main letter (i.e being taught/quized).
        /// </summary>
        public int Letter { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.LessonPart"/> class.
        /// </summary>
        public LessonPart(LessonPartType lessonPartType, int letter)
        {
            LessonPartType = lessonPartType;
            Letter = letter;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.LessonPart"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.LessonPart"/>.</returns>
        public override string ToString()
        {
            return $"LessonPart has {LessonPartType} type with letter (index) {Letter}";
        }
    }
}
