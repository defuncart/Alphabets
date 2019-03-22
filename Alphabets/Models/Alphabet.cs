namespace Alphabets.Models
{
    /// <summary>
    /// A model representing an alphabet.
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// The alphabet's letters.
        /// </summary>
        public Letter[] Letters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Alphabets.Models.Alphabet"/> class.
        /// </summary>
        public Alphabet(Letter[] letters)
        {
            Letters = letters;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Alphabet"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Alphabets.Models.Alphabet"/>.</returns>
        public override string ToString()
        {
            return $"Alphabet has {Letters.Length} letters";
        }
    }
}
