using Alphabets.Models;
using Alphabets.Models.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Alphabets.Managers
{
    public static class PlayerDataManager
    {
        /// <summary>The PlayerData filename.</summary>
        private const string FILENAME = "PlayerData.json";

        /// <summary>The full filepath to save/load the PlayerData to/from.</summary>
        private static readonly string filepath;

        /// <summary>The player data.</summary>
        private static readonly PlayerData playerData;

        /// <summary>
        /// Initializes the <see cref="T:Alphabets.Managers.PlayerDataManager"/> class.
        /// </summary>
        static PlayerDataManager()
        {
            //determine the filepath (i.e. /data/user/0/com.defuncart.Alphabets/files/PlayerData.json) which isn't exposed to the user
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filepath = Path.Combine(documentsPath, FILENAME);

            //if the file exists, load into memory, otherwise create a new one
            if (File.Exists(filepath))
            {
                //load model from json
                string text = File.ReadAllText(filepath);
                playerData = JsonConvert.DeserializeObject<PlayerData>(text);

                Debug.WriteLine(text);
            }
            else
            {
                //no file exists, create player data
                playerData = CreatePlayerData();

                //and serialize to disk
                WriteToDisk();
            }
        }

        /// <summary>
        /// Creates the player data.
        /// </summary>
        private static PlayerData CreatePlayerData()
        {
            //firstly create [ string : LetterSaveData ]
            Dictionary<string, LetterSaveData> lettersSaveData = new Dictionary<string, LetterSaveData>();
            foreach (Alphabet alphabet in CourseManager.Courses.Select(x=>x.Alphabet))
            {
                foreach (Letter letter in alphabet.Letters)
                {
                    lettersSaveData[letter.ResourceId] = new LetterSaveData();
                }
            }

            //create PlayerData
            return new PlayerData { LettersSaveData = lettersSaveData };
        }

        /// <summary>
        /// Updates PlayerData when a letter was answered.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="correctly">Whether letter was answered correctly.</param>
        public static void UpdateLetter(Letter letter, bool correctly)
        {
            if (playerData.LettersSaveData.TryGetValue(letter.ResourceId, out LetterSaveData letterSaveData))
            {
                letterSaveData.Attempts++;
                if (correctly) { letterSaveData.Correct++; }
            }
        }

        /// <summary>
        /// Gets the answered correctly ratio for a given letter.
        /// </summary>
        public static double GetCorrectRatioForLetter(Letter letter)
        {
            if (playerData.LettersSaveData.TryGetValue(letter.ResourceId, out LetterSaveData letterSaveData))
            {
                return letterSaveData.Correct / (letterSaveData.Attempts * 1.0);
            }

            return 0;
        }

        /// <summary>
        /// Writes the PlayerData to disk.
        /// </summary>
        public static void WriteToDisk()
        {
            File.WriteAllText(filepath, JsonConvert.SerializeObject(playerData));
        }

        //TODO DEBUG
        public static void Log()
        {
            Debug.WriteLine(filepath);
        }
    }
}
