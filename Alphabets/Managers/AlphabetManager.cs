﻿using Alphabets.Models;
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
        /// The current alphabet.
        /// </summary>
        public static Alphabet Alphabet { get; }

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.AlphabetManager"/> class.
        /// </summary>
        static AlphabetManager()
        {
            //get json string
            string json = FileHelper.TextResourceToString("Alphabets.Resources.Database.Georgian.json");

            //deserialize json data
            AlphabetImport alphabetImport = JsonConvert.DeserializeObject<AlphabetImport>(json);

            //create letters
            List<Letter> letters = new List<Letter>();
            foreach(LetterImport letterImport in alphabetImport.Letters)
            {
                letters.Add(new Letter(cases: letterImport.Cases, transCases: letterImport.TransCases, pronounciationTips: letterImport.PronounciationTips));
            }

            //create alphabet
            Alphabet = new Alphabet(name: alphabetImport.Name, letters: letters.ToArray());
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine(Alphabet);

            Debug.WriteLine(Alphabet.Letters[0]);
        }
    }
}
