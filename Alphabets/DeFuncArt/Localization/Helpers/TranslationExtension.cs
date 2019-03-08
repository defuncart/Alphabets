using DeFuncArt.Localization.Managers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeFuncArt.Localization.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
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

            //other return the translation
            return LocalizationManager.GetValue(Text);
        }
    }
}
