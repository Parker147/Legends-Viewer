using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LegendsViewer.Legends
{
    public static class Formatting
    {
        public static string InitCaps(string name, bool all = true)
        {
            if (name.Length == 0) return name;
            name = name.Trim();
            string[] parts = name.Split(new string[] { " " }, StringSplitOptions.None);
            string capName = "";
            if (all)
                foreach (string part in parts)
                {
                    if (capName.Length > 0) capName += " ";
                    if (((part != "the" && part != "of") || (capName.Length == 0)) && part.Length > 0)
                        capName += part.ToUpper()[0] + part.Substring(1, part.Length - 1);
                    else
                        capName += part.ToLower();
                }
            return capName;

        }

        public static string MakePopulationPlural(string population)
        {
            string ending = "";

            if (population.Contains(" of"))
            {
                ending = population.Substring(population.IndexOf(" of"), population.Length - population.IndexOf(" of"));
                population = population.Substring(0, population.IndexOf(" of"));
            }

            if (population.EndsWith("Men") || population.EndsWith("men") || population == "Humans") return population + ending;
            else if (population.EndsWith("s") && !population.EndsWith("ss")) return population + ending;

            if (population == "Human") population = "Humans";
            else if (population.EndsWith("Man")) population = population.Replace("Man", "Men");
            else if (population.EndsWith("man") && !population.Contains("Human")) population = population.Replace("man", "men");
            else if (population.EndsWith("Woman")) population = population.Replace("Woman", "Women");
            else if (population.EndsWith("woman")) population = population.Replace("woman", "women");
            else if (population.EndsWith("f")) population = population.Substring(0, population.Length - 1) + "ves";
            else if (population.EndsWith("x") || population.EndsWith("ch") || population.EndsWith("sh") || population.EndsWith("s")) population += "es";
            else if (population.EndsWith("y") && !population.EndsWith("ay") && !population.EndsWith("ey") && !population.EndsWith("iy") && !population.EndsWith("oy") && !population.EndsWith("uy")) population = population.Substring(0, population.Length - 1) + "ies";
            else if (!population.EndsWith("i") && !population.EndsWith("le")) population += "s";

            if (ending != "") population += ending;

            return population;
        }

        public static string FormatRace(string race)
        {
            if (race.Contains("DEMON")) return "Demon";
            if (race.Contains("FORGOTTEN")) return "Forgotten Beast";
            if (race.Contains("NIGHT_CREATURE")) return "Night Creature";
            if (race.Contains("TITAN")) return "Titan";
            return InitCaps(race.Replace('_', ' ').ToLower());

        }

        public static string ReplaceNonAscii(string name)
        {
            name = name.Replace("\u017D", "a");
            name = name.Replace("\u017E", "a");
            name = name.Replace("\u201E", "a");
            name = name.Replace("\u0192", "a");
            name = name.Replace("\u008F", "a");
            name = name.Replace("\u2020", "a");
            name = name.Replace("\u00A0", "a");
            name = name.Replace("\u2026", "a");
            name = name.Replace("\u02C6", "e");
            name = name.Replace("\u2030", "e");
            name = name.Replace("\u201A", "e");
            name = name.Replace("\u0220", "e");
            name = name.Replace("\u0160", "e");
            name = name.Replace("\u0090", "e");
            name = name.Replace("\u2039", "i");
            name = name.Replace("\u00A1", "i");
            name = name.Replace("\u008D", "i");
            name = name.Replace("\u0152", "i");
            name = name.Replace("\u00A4", "n");
            name = name.Replace("\u00A5", "n");
            name = name.Replace("\u201D", "o");
            name = name.Replace("\u00A2", "o");
            name = name.Replace("\u2022", "o");
            name = name.Replace("\u201C", "o");
            name = name.Replace("\u2122", "o");
            name = name.Replace("\u2014", "u");
            name = name.Replace("\u2013", "u");
            name = name.Replace("\u00A3", "u");
            name = name.Replace("\u02DC", "y");
            return name;
        }

        public static Location ConvertToLocation(string coordinates)
        {
            int x = Convert.ToInt32(coordinates.Substring(0, coordinates.IndexOf(",")));
            int y = Convert.ToInt32(coordinates.Substring(coordinates.IndexOf(",") + 1, coordinates.Length - coordinates.IndexOf(",") - 1));
            return new Location(x, y);
        }

        public static void ResizeImage(Bitmap source, ref Bitmap dest, int height, int width, bool keepRatio, bool smooth = true)
        {
            double resizePercent = 1;
            Size imageSize = new Size();
            if (keepRatio)
            {
                if (source.Width > source.Height)
                    resizePercent = height / Convert.ToDouble(source.Width);
                else
                    resizePercent = height / Convert.ToDouble(source.Height);
                imageSize.Width = Convert.ToInt32(source.Width * resizePercent);
                imageSize.Height = Convert.ToInt32(source.Height * resizePercent);
            }
            else
            {
                imageSize.Width = width;
                imageSize.Height = height;
            }

            dest = new Bitmap(imageSize.Width, imageSize.Height);
            using (Graphics g = Graphics.FromImage(dest))
            {

                if (smooth)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                }
                else
                {
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                }
                using (SolidBrush fillBrush = new SolidBrush(Color.FromArgb(0, 0, 50)))
                    g.FillRectangle(fillBrush, 0, 0, height, height);
                g.DrawImage(source, new Rectangle(0, 0, imageSize.Width, imageSize.Height), new Rectangle(0, 0, source.Width, source.Height), GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        public static Color HsvToRgb(double h, double S, double V)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            int r = Clamp((int)(R * 255.0));
            int g = Clamp((int)(G * 255.0));
            int b = Clamp((int)(B * 255.0));
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static string TimeCountToSeason(int count)
        {
            string seasonString = string.Empty;
            int month = count % 100800;
            if (month <= 33600) seasonString += "early ";
            else if (month <= 67200) seasonString += "mid";
            else if (month <= 100800) seasonString += "late ";

            int season = count % 403200;
            if (season < 100800) seasonString += "spring";
            else if (season < 201600) seasonString += "summer";
            else if (season < 302400) seasonString += "autumn";
            else if (season < 403200) seasonString += "winter";

            return seasonString;
        }

        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }
    }
}

