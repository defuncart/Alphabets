using Plugin.Multilingual;
using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alphabets.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        /// <summary>A resource identifier (full path) to AppResources.</summary>
        private const string ResourceId = "Alphabets.Resources.AppResources";

        /// <summary>The resource manager.</summary>
        private  readonly Lazy<ResourceManager> resmgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        /// <summary>The text value.</summary>
        public string Text { get; set; }

        /// <summary>
        /// Provides the value.
        /// </summary>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //if the text is null, return an empty string
            if(Text == null)
            {
                return "";
            }

            //try to get the translation from the AppResources
            string translation = resmgr.Value.GetString(Text, CrossMultilingual.Current.CurrentCultureInfo);

            //if the translation is null, set the KEY to be the translation itself
            if(translation == null)
            {
                translation = Text;
            }

            //return the translation
            return translation;
        }
    }
}
