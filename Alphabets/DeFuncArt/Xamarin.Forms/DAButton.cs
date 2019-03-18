using System;
using Xamarin.Forms;

namespace DeFuncArt.Xamarin.Forms
{
    /// <summary>
    /// An extension of Xamarin.Forms.Button which adds the ability to autosize the font (between optional min and max values).
    /// </summary>
    public class DAButton : Button
    {
        /// <summary>The minimum font size allowed.</summary>
        private const double MIN_FONT_SIZE = 6;

        /// <summary>
        /// The minimum font size property.
        /// </summary>
        public static readonly BindableProperty MinFontSizeProperty = BindableProperty.CreateAttached("MinFontSize", typeof(double), typeof(DAButton), MIN_FONT_SIZE, validateValue: (bindable, value) => (double)value >= MIN_FONT_SIZE);

        /// <summary>
        /// Gets the specified minimum font size.
        /// </summary>
        public static double GetMinFontSize(BindableObject bindable)
        {
            return (double)bindable.GetValue(MinFontSizeProperty);
        }

        /// <summary>The maximum font size allowed.</summary>
        private const double MAX_FONT_SIZE = 500;

        /// <summary>
        /// The maximum font size property.
        /// </summary>
        public static readonly BindableProperty MaxFontSizeProperty = BindableProperty.CreateAttached("MaxFontSize", typeof(double), typeof(DAButton), MAX_FONT_SIZE, validateValue: (bindable, value) => (double)value <= MAX_FONT_SIZE);

        /// <summary>
        /// Gets the specified maximum font size.
        /// </summary>
        public static double GetMaxFontSize(BindableObject bindable)
        {
            return (double)bindable.GetValue(MaxFontSizeProperty);
        }

        //TODO MaxFontSizeProperty, MinFontSizeProperty don't assure against MinFontSize > MaxFontSize

        /// <summary>
        /// The button's text.
        /// </summary>
        new public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); AutoFontSize(); }
        }

        /// <summary>
        /// Callback when the size of the element is set during a layout cycle.
        /// </summary>
        protected override void OnSizeAllocated(double width, double height)
        {
            //call base implementation
            base.OnSizeAllocated(width, height);

            //update font size
            AutoFontSize();
        }

        /// <summary>
        /// Autosizes the label's font size with regards to it's container size.
        /// </summary>
        private void AutoFontSize()
        {
            //initialize the upper and lower font sizes
            double lowerFontSize = (double)GetValue(MinFontSizeProperty);
            double upperFontSize = (double)GetValue(MaxFontSizeProperty);

            //start a loop which'll find the optimal font size
            while (upperFontSize - lowerFontSize > 1)
            {
                //determine current average font size and calculate corresponding text height
                double fontSize = (lowerFontSize + upperFontSize) / 2;
                double textHeight = TextHeightForFontSize(fontSize);

                //if the calculated height is out of bounds, update upper size, else update lower size
                if (textHeight > Height)
                {
                    upperFontSize = fontSize;
                }
                else
                {
                    lowerFontSize = fontSize;
                }
            }

            //finally set the correct font size
            FontSize = lowerFontSize;
        }

        /// <summary>
        /// Determines the text height for the button with a given font size.
        /// </summary>
        private double TextHeightForFontSize(double fontSize)
        {
            FontSize = fontSize;
            return OnMeasure(Width, Double.PositiveInfinity).Request.Height;
        }
    }
}
