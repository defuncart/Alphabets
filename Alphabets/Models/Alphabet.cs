namespace Alphabets.Models
{
    /// <summary>
    /// A model representing an alphabet.
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// The alphabet's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The alphabet's letters.
        /// </summary>
        public Letter[] Letters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Alphabet"/> class.
        /// </summary>
        public Alphabet(string name, Letter[] letters)
        {
            Letters = letters;
            Name = name;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Alphabet"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Alphabet"/>.</returns>
        public override string ToString()
        {
            return $"Alphabet {Name} has {Letters.Length} letters";
        }
    }
}
