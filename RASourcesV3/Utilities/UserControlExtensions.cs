using System;
using System.Drawing;

namespace RiskApps3.Utilities
{
    public static class UserControlExtensions
    {
        /// <summary>
        /// Return a new instance of this <code>Font</code> with the FontSize scaled by the given factor.
        /// </summary>
        /// <param name="font">a reference to current object</param>
        /// <param name="scalingFactor">amount to adjust by</param>
        /// <returns>a new <code>Font</code> object</returns>
        public static Font Scale(this Font font, float scalingFactor)
        {
            if (scalingFactor <= 0)
                scalingFactor = 1;

            return new Font(font.FontFamily, font.Size*scalingFactor, font.Style);
        }

        /// <summary>
        /// Scale a <code>Size</code> object by a given factor.
        /// </summary>
        /// <param name="size">original <code>Size</code></param>
        /// <param name="scalingFactor">rational number to scale up by</param>
        /// <returns>a new size</returns>
        public static Size Scale(this Size size, float scalingFactor = 1)
        {
            const int buffer = 20;  //used to accomodate font style changes that don't scale linearly

            return new Size(
                (int)Math.Round(size.Width * scalingFactor) + buffer,
                (int)Math.Round(size.Height * scalingFactor) + buffer);
        }
    }
}
