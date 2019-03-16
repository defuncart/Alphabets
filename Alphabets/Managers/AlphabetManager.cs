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
    /// Alphabet manager.
    /// </summary>
    public static class AlphabetManager
    {
        /// <summary>
        /// An array of alphabets.
        /// </summary>
        private static readonly Alphabet[] alphabets;

        /// <summary>
        /// The current alphabet.
        /// </summary>
        public static Alphabet Alphabet => alphabets[UserSettings.CurrentCourseIndex];

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.AlphabetManager"/> class.
        /// </summary>
        static AlphabetManager()
        {
            //get an array of alphabet types (i.e. Georgian, Russian)
            AlphabetType[] alphabetTypes = (AlphabetType[])System.Enum.GetValues(typeof(AlphabetType));

            //create all alphabets
            alphabets = new Alphabet[alphabetTypes.Length];
            for (int i = 0; i < alphabets.Length; i++)
            {
                alphabets[i] = CreateAlphabet(alphabetTypes[i]);
            }
        }

        /// <summary>
        /// Creates an alphabet from json using a given AlphabetType.
        /// </summary>
        private static Alphabet CreateAlphabet(AlphabetType alphabetType)
        {
            //determine resource id
            string resourceId = $"Alphabets.Resources.Database.{alphabetType}.Alphabet.json";

            //get json string
            string json = FileHelper.TextResourceToString(resourceId);

            //deserialize json data
            AlphabetImport alphabetImport = JsonConvert.DeserializeObject<AlphabetImport>(json);

            //create letters
            List<Letter> letters = new List<Letter>();
            foreach (LetterImport letterImport in alphabetImport.Letters)
            {
                letters.Add(new Letter(cases: letterImport.Cases, transCases: letterImport.TransCases, pronounciationTips: letterImport.PronounciationTips, resourceId: letterImport.ResourceId));
            }

            //create alphabet
            return new Alphabet(name: alphabetImport.Name, letters: letters.ToArray());
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine($"There are {alphabets.Length} alphabet(s)");
            foreach (Alphabet alphabet in alphabets)
            {
                Debug.WriteLine(alphabet);
            }
        }
    }
}
