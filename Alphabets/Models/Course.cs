namespace Alphabets.Models
{
    /// <summary>
    /// A model representing an alphabet.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The course's id.
        /// </summary>
        public string Id { get; }

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
        /// An array of practice words.
        /// </summary>
        public Word[] Words { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Course"/> class.
        /// </summary>
        public Course(string id, string title, Alphabet alphabet, Lesson[] lessons, Word[] words)
        {
            Id = id;
            Title = title;
            Alphabet = alphabet;
            Lessons = lessons;
            Words = words;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Course"/>.</returns>
        public override string ToString()
        {
            return $"Course \"{Title}\" has {Lessons.Length} lessons and {Words.Length} practice words.";
        }
    }
}
