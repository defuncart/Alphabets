using System.IO;
using System.Reflection;

namespace DeFuncArt.Helpers
{
    /// <summary>
    /// A static class of helper functions for dealing with files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>A reference to the assembly.</summary>
        private static readonly Assembly assembly;

        /// <summary>
        /// Initializes the <see cref="T:DeFuncArt.Helpers.FileHelper"/> class.
        /// </summary>
        static FileHelper()
        {
            assembly = IntrospectionExtensions.GetTypeInfo(typeof(FileHelper)).Assembly;
        }

        /// <summary>
        /// The name of the assembly.
        /// </summary>
        public static string AssemblyName => assembly.GetName().Name;

        /// <summary>
        /// Returns the stream for a given resource id.
        /// </summary>
        public static Stream GetStream(string resourceId)
        {
            return assembly.GetManifestResourceStream(resourceId);
        }

        /// <summary>
        /// Returns a string comprising of the contents of a text resource.
        /// </summary>
        public static string TextResourceToString(string resourceId)
        {
            Stream stream = GetStream(resourceId);
            if (stream != null)
            {
                //create string text
                string text = "";
                using (StreamReader reader = new StreamReader(stream))
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
