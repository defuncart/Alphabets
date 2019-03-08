using System.IO;
using System.Reflection;

namespace DeFuncArt.Helpers
{
    /// <summary>
    /// A static class of helper functions for dealing with files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Returns a string comprising of the contents of a text resource.
        /// </summary>
        public static string TextResourceToString(string resourceId)
        {
            //get a reference to the filestream
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FileHelper)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceId);

            if(stream != null)
            {   
                //create string text
                string text = "";
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

                //return string
                return text;

            }

            //otherwise return null
            return null;
        }
    }
}
