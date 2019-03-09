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
        /// The current course.
        /// </summary>
        public static Course Course { get; }

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.CourseManager"/> class.
        /// </summary>
        static CourseManager()
        {
            //determine current alphabet/course
            AlphabetType alphabetType = (AlphabetType)UserSettings.CurrentCourseIndex;

            //determine resource id
            string resourceId = $"Alphabets.Resources.Database.{alphabetType}.Course.json";

            //get json string
            string json = FileHelper.TextResourceToString(resourceId);

            //deserialize json data
            CourseImport courseImport = JsonConvert.DeserializeObject<CourseImport>(json);

            //create lessons
            List<Lesson> lessons = new List<Lesson>();
            foreach(LessonImport lessonImport in courseImport.Lessons)
            {
                //create lesson parts
                List<LessonPart> lessonParts = new List<LessonPart>();
                foreach(LessonPartImport lessonPartImport in lessonImport.LessonParts)
                {
                    lessonParts.Add(new LessonPart(lessonPartType: lessonPartImport.LessonPartType, letter: lessonPartImport.Letter));
                }

                lessons.Add(new Lesson(cumulativeLetters: lessonImport.CumulativeLetters, lessonParts: lessonParts.ToArray()));
            }

            //create course
            Course = new Course(alphabetType: AlphabetType.Georgian, lessons: lessons.ToArray());
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine(Course);

            Debug.WriteLine(Course.Lessons[0]);
        }
    }
}
