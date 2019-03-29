using Xamarin.Forms;

namespace DeFuncArt.Extensions
{
    /// <summary>A collection of extention methods.</summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Sets whether the button is enabled and visible.
        /// </summary>
        public static void SetEnabledVisible(this VisualElement element, bool isEnabledVisible)
        {
            element.IsEnabled = element.IsVisible = isEnabledVisible;
        }
    }
}
