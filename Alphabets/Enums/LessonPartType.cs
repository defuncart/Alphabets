namespace Alphabets.Enums
{
    /// <summary>
    /// The types of parts in a lesson.
    /// </summary>
    public enum LessonPartType
    {
        /// <summary>
        /// Lesson part is learning a new letter.
        /// </summary>
        Learning,
        /// <summary>
        /// Lesson part is a multiple choice quiz where the letter is displayed in the original alphabet and a transliteration answer must be chosen.
        /// </summary>
        MCAlphabetToTransliteration,
        /// <summary>
        /// Lesson part is a multiple choice quiz where the letter is displayed as a transliteration and an answer in the original alphabe must be chosen.
        /// </summary>
        MCTransliterationToAlphabet,
        /// <summary>
        /// Lesson part is typing where a word is displayed in the original alphabet and a transliteration answer must be written.
        /// </summary>
        WordAlphabetToTransliteration
    }
}
