using Alphabets.Enums;
using Alphabets.Models;
using Alphabets.Models.JSON;
using DeFuncArt.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alphabets.Managers
{
    /// <summary>
    /// Course manager.
    /// </summary>
    public static class CourseManager
    {
        /// <summary>
        /// An array of courses.
        /// </summary>
        public static Course[] Courses { get; }

        /// <summary>
        /// The current course.
        /// </summary>
        public static Course CurrentCourse => Courses[PlayerDataManager.CurrentCourseIndex];

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.CourseManager"/> class.
        /// </summary>
        static CourseManager()
        {
            //get an array of alphabet types (i.e. Georgian, Russian)
            AlphabetType[] alphabetTypes = (AlphabetType[])System.Enum.GetValues(typeof(AlphabetType));

            //create all courses
            Courses = new Course[alphabetTypes.Length];
            for (int i = 0; i < Courses.Length; i++)
            {
                Courses[i] = CreateCourse(alphabetTypes[i]);
            }
        }

        /// <summary>
        /// Creates a course from json using a given AlphabetType.
        /// </summary>
        private static Course CreateCourse(AlphabetType alphabetType)
        {
            //determine resource id
            string resourceId = $"Alphabets.Resources.Database.{alphabetType}.json";

            //get json string
            string json = FileHelper.TextResourceToString(resourceId);

            //deserialize json data
            CourseImport courseImport = JsonConvert.DeserializeObject<CourseImport>(json);

            //create letters
            List<Letter> letters = new List<Letter>();
            foreach (LetterImport letterImport in courseImport.Alphabet.Letters)
            {
                letters.Add(new Letter(cases: letterImport.Cases, transCases: letterImport.TransCases, pronounciationTips: letterImport.PronounciationTips, resourceId: letterImport.ResourceId));
            }

            //create lessons
            List<Lesson> lessons = new List<Lesson>();
            foreach (LessonImport lessonImport in courseImport.Lessons)
            {
                //create lesson parts
                List<LessonPart> lessonParts = new List<LessonPart>();
                foreach (LessonPartImport lessonPartImport in lessonImport.LessonParts)
                {
                    lessonParts.Add(new LessonPart(lessonPartType: lessonPartImport.LessonPartType, index: lessonPartImport.Index));
                }

                //create cumulative letters (referernces to letters from indeces)
                Letter[] cumulativeLetters = new Letter[lessonImport.CumulativeLetters.Length];
                for (int i = 0; i < cumulativeLetters.Length; i++)
                {
                    cumulativeLetters[i] = letters[lessonImport.CumulativeLetters[i]];
                }

                lessons.Add(new Lesson(cumulativeLetters: cumulativeLetters, lessonParts: lessonParts.ToArray()));
            }

            //create words
            List<Word> words = new List<Word>();
            foreach (WordImport wordImport in courseImport.Words)
            {
                Letter[] wordLetters = WordLetterIndecesArrayToWordLetterArray(wordImport.Letters, letters);
                words.Add(new Word(letters: wordLetters, original: wordImport.Original, transliteration: wordImport.Transliteration, tips: wordImport.Tips, lesson: wordImport.Lesson));
            }

            //create course
            return new Course(id: alphabetType.ToString(), title: courseImport.Title, alphabet: new Alphabet(letters.ToArray()), lessons: lessons.ToArray(), words: words.ToArray());
        }

        /// <summary>
        /// Constructs a [Letter] from an [int] of the letter indeces for a given alphabet's letters.
        /// </summary>
        /// <param name="indeces">The letter indeces.</param>
        /// <param name="alphabetLetters">The alphabet's letters.</param>
        private static Letter[] WordLetterIndecesArrayToWordLetterArray(int[] indeces, List<Letter> alphabetLetters)
        {
            Letter[] wordLetters = new Letter[indeces.Length];
            for (int i = 0; i < wordLetters.Length; i++)
            {
                wordLetters[i] = alphabetLetters[indeces[i]];
            }
            return wordLetters;
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine($"There are {Courses.Length} course(s)");
            foreach (Course course in Courses)
            {
                Debug.WriteLine(course);
            }
        }
    }
}
