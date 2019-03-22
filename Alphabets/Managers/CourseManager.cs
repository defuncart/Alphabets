using Alphabets.Enums;
using Alphabets.Helpers;
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
        public static Course[] Courses { get; private set; }

        /// <summary>
        /// The current course.
        /// </summary>
        public static Course CurrentCourse => Courses[UserSettings.CurrentCourseIndex];

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
                    lessonParts.Add(new LessonPart(lessonPartType: lessonPartImport.LessonPartType, letter: lessonPartImport.Letter));
                }

                lessons.Add(new Lesson(cumulativeLetters: lessonImport.CumulativeLetters, lessonParts: lessonParts.ToArray()));
            }

            //create course
            return new Course(alphabet: new Alphabet(letters.ToArray()), lessons: lessons.ToArray());
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
