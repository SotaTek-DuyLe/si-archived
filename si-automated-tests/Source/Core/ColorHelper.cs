using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace si_automated_tests.Source.Core
{
    public class ColorHelper
    {
        public static bool IsRedColor(string cssColor)
        {
            Color color = ParseColor(cssColor);
            float hue = color.GetHue();
            return (0 <= hue && hue <= 30) || (hue >= 330 && hue <= 360);
        }

        public static bool IsDarkColor(string cssColor)
        {
            Color col = ParseColor(cssColor);
            return col.R * 0.2126 + col.G * 0.7152 + col.B * 0.0722 < 255 / 2;
        }

        public static bool IsGreenColor(string cssColor)
        {
            Color color = ParseColor(cssColor);
            float hue = color.GetHue();
            return (90 <= hue && hue <= 150);
        }

        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }
    }
}
