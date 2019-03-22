namespace Alphabets.Models
{
    /// <summary>
    /// A model representing an alphabet.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The course's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The alphabet.
        /// </summary>
        public Alphabet Alphabet { get; }

        /// <summary>
        /// The courses's lessons.
        /// </summary>
        public Lesson[] Lessons { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Course"/> class.
        /// </summary>
        public Course(string title, Alphabet alphabet, Lesson[] lessons)
        {
            Title = title;
            Alphabet = alphabet;
            Lessons = lessons;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.</returns>
        public override string ToString()
        {
            return $"Course for {Alphabet} has {Lessons.Length} lessons";
        }
    }
}
