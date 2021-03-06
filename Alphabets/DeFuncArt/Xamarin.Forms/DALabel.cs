﻿using System;
using Xamarin.Forms;

namespace DeFuncArt.Xamarin.Forms
{
    /// <summary>
    /// An extension of Xamarin.Forms.Label which adds the ability to autosize the font (between optional min and max values).
    /// </summary>
    public class DALabel : Label
    {
        /// <summary>The minimum font size allowed.</summary>
        private const double MIN_FONT_SIZE = 6;

        /// <summary>
        /// The minimum font size property.
        /// </summary>
        public static readonly BindableProperty MinFontSizeProperty = BindableProperty.CreateAttached("MinFontSize", typeof(double), typeof(DALabel), MIN_FONT_SIZE, validateValue: (bindable, value) => (double)value >= MIN_FONT_SIZE);

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
        public static readonly BindableProperty MaxFontSizeProperty = BindableProperty.CreateAttached("MaxFontSize", typeof(double), typeof(DALabel), MAX_FONT_SIZE, validateValue: (bindable, value) => (double)value <= MAX_FONT_SIZE);

        /// <summary>
        /// Gets the specified maximum font size.
        /// </summary>
        public static double GetMaxFontSize(BindableObject bindable)
        {
            return (double)bindable.GetValue(MaxFontSizeProperty);
        }

        //TODO MaxFontSizeProperty, MinFontSizeProperty don't assure against MinFontSize > MaxFontSize

        /// <summary>
        /// The label's text.
        /// </summary>
        new public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); AutoFontSize(); }
        }

        /// <summary>
        /// The label's formatted text.
        /// </summary>
        new public FormattedString FormattedText
        {
            get { return (FormattedString)GetValue(FormattedTextProperty); }
            set { SetValue(FormattedTextProperty, value); AutoFontSize(); }
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
                //determine current average font size
                double fontSize = (lowerFontSize + upperFontSize) / 2;

                //calculate corresponding text height
                double textHeight; 
                if(FormattedText == null) //if there is no formatted text, simply set the overall font size
                {
                    FontSize = fontSize;
                }
                else //otherwise set the font size per span component
                {
                    foreach (Span span in FormattedText.Spans)
                    {
                        span.FontSize = fontSize;
                    }
                }
                textHeight = OnMeasure(Width, Double.PositiveInfinity).Request.Height;

                //if the calculated height is out of bounds, update upper size, otherwise update lower size
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
    }
}
