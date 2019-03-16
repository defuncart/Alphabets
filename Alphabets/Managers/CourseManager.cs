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
        private static readonly Course[] courses;

        /// <summary>
        /// The current course.
        /// </summary>
        public static Course Course => courses[UserSettings.CurrentCourseIndex];

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.CourseManager"/> class.
        /// </summary>
        static CourseManager()
        {
            //get an array of alphabet types (i.e. Georgian, Russian)
            AlphabetType[] alphabetTypes = (AlphabetType[])System.Enum.GetValues(typeof(AlphabetType));

            //create all courses
            courses = new Course[alphabetTypes.Length];
            for (int i = 0; i < courses.Length; i++)
            {
                courses[i] = CreateCourse(alphabetTypes[i]);
            }
        }

        /// <summary>
        /// Creates a course from json using a given AlphabetType.
        /// </summary>
        private static Course CreateCourse(AlphabetType alphabetType)
        {
            //determine resource id
            string resourceId = $"Alphabets.Resources.Database.{alphabetType}.Course.json";

            //get json string
            string json = FileHelper.TextResourceToString(resourceId);

            //deserialize json data
            CourseImport courseImport = JsonConvert.DeserializeObject<CourseImport>(json);

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
            return new Course(alphabetType: AlphabetType.Georgian, lessons: lessons.ToArray());
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine($"There are {courses.Length} course(s)");
            foreach (Course course in courses)
            {
                Debug.WriteLine(course);
            }
        }
    }
}
