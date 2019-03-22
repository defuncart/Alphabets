using System;
using System.Collections.Generic;
using Alphabets.Enums;

namespace Alphabets.Models.JSON
{
    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Alphabet"/>.
    /// </summary>
    [Serializable]
    public class AlphabetImport
    {
        /// <summary>
        /// The alphabet's letters.
        /// </summary>
        public LetterImport[] Letters;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Letter"/>.
    /// </summary>
    [Serializable]
    public class LetterImport
    {
        /// <summary>
        /// The cases (i.e. upper and lower) for the letter.
        /// </summary>
        public string[] Cases;

        /// <summary>
        /// The transliterated cases (i.e. upper and lower) for the letter.
        /// </summary>
        public string[] TransCases;

        /// <summary>
        /// A dictionary of pronounciation tips per localized language.
        /// </summary>
        public Dictionary<string, string> PronounciationTips;

        /// <summary>
        /// The audio resource identifier.
        /// </summary>
        public string ResourceId;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Word"/>.
    /// </summary>
    [Serializable]
    public class WordImport
    {
        /// <summary>
        /// The original word (written in the alphabet to learn).
        /// </summary>
        public string Original;

        /// <summary>
        /// The transliteration (written using latin alphabet).
        /// </summary>
        public string Transliteration;

        /// <summary>
        /// A dictionary of tips per localized language.
        /// </summary>
        public Dictionary<string, string> Tips;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Course"/>.
    /// </summary>
    [Serializable]
    public class CourseImport
    {
        /// <summary>
        /// The course's title.
        /// </summary>
        public string Title;

        /// <summary>
        /// The alphabet.
        /// </summary>
        public AlphabetImport Alphabet;

        /// <summary>
        /// The courses's lessons.
        /// </summary>
        public LessonImport[] Lessons;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.Lesson"/>.
    /// </summary>
    [Serializable]
    public class LessonImport
    {
        /// <summary>
        /// An array of letters already learned and taught in this lesson.
        /// </summary>
        public int[] CumulativeLetters;

        /// <summary>
        /// An array of lesson parts.
        /// </summary>
        public LessonPartImport[] LessonParts;
    }

    /// <summary>
    /// An import json model for <see cref="T:Alphabets.Models.LessonPart"/>.
    /// </summary>
    [Serializable]
    public class LessonPartImport
    {
        /// <summary>
        /// The type of the lesson part.
        /// </summary>
        public LessonPartType LessonPartType;

        /// <summary>
        /// The main letter (i.e being taught/quized).
        /// </summary>
        public int Letter;
    }
}
