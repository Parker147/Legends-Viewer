using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.Map
{
    public class HeatMapMaker : IDisposable
    {
        Bitmap HeatMap, HeatGradient, Occurence;
        int OccurenceIntensity = 25, OccurenceDiameter = 75, MaxOccurencesToDraw = 50;

        public static Bitmap Create(int width, int height, List<Location> occurences, List<int> occurencesCount = null)
        {
            HeatMapMaker heatmap = new HeatMapMaker(width, height, occurences, occurencesCount);
            return heatmap.HeatMap;
        }

        private HeatMapMaker(int width, int height, List<Location> occurences, List<int> occurencesCount = null)
        {

            MakeHeatGradient();
            MakeOccurence();
            Bitmap AlphaMap = new Bitmap(width, height);
            Graphics alphaMapGraphics = Graphics.FromImage(AlphaMap);
            alphaMapGraphics.SmoothingMode = SmoothingMode.None;
            alphaMapGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            if (occurencesCount == null)
            {
                var groupedOccurences = from occurence in occurences
                                        group occurence by occurence into occurenceGroup
                                        select occurenceGroup.Count();
                occurencesCount = groupedOccurences.ToList();
            }
            occurences = occurences.GroupBy(occurence => occurence).Select(occurence => occurence.Key).ToList();
            int maxOccurences;
            if (occurencesCount.Count > 0) maxOccurences = occurencesCount.Max();
            else maxOccurences = 0;
            double drawNumRatio = Convert.ToDouble(MaxOccurencesToDraw) / maxOccurences;
            for (int i = 0; i < occurences.Count; i++)
            {
                int drawNum = Convert.ToInt32(drawNumRatio * occurencesCount[i]);
                if (drawNum == 0 && occurencesCount[i] > 0) drawNum = 1;
                for (int h = 0; h < drawNum; h++)
                    DrawOccurence(occurences[i], alphaMapGraphics);
            }

            AlphaMap = Blur(AlphaMap);
            HeatMap = new Bitmap(width, height);
            ConvertAlphaMapToHeatMap(AlphaMap);
            AlphaMap.Dispose();

        }

        private void MakeHeatGradient()
        {
            HeatGradient = new Bitmap(256, 25);

            using (LinearGradientBrush heatGradient = new LinearGradientBrush(new Point(0, 0), new Point(HeatGradient.Width, HeatGradient.Height), Color.Red, Color.Yellow))
            {
                ColorBlend colorBlend = new ColorBlend();
                //colorBlend.Colors = new Color[] { Color.Red, Color.Yellow, Color.GreenYellow, Color.Transparent };
                //colorBlend.Colors = new Color[] { Color.FromArgb(200, Color.Red), Color.FromArgb(200, Color.Yellow), Color.FromArgb(200, Color.GreenYellow), Color.Transparent };
                //colorBlend.Positions = new float[] { 0.00f, 0.33f, 0.66f, 1.00f };
                colorBlend.Colors = new Color[] { Color.FromArgb(200, Color.Red), Color.FromArgb(200, Color.Yellow), Color.FromArgb(200, Color.Green), Color.FromArgb(200, Color.Cyan), Color.FromArgb(200, Color.Blue), Color.Transparent };
                colorBlend.Positions = new float[] { 0.00f, 0.20f, 0.40f, 0.60f, 0.80f, 1.00f };
                //colorBlend.Colors = new Color[] { Color.FromArgb(200, Color.White), Color.FromArgb(200, Color.Red), Color.FromArgb(200, Color.Yellow), Color.FromArgb(200, Color.Green), Color.FromArgb(200, Color.Cyan), Color.FromArgb(200, Color.Blue), Color.Transparent };
                //colorBlend.Positions = new float[] { 0.00f, 0.166f, 0.332f, 0.498f, 0.664f, .083f, 1f };
                //colorBlend.Colors = new Color[] { Color.FromArgb(200, Color.Red), Color.FromArgb(200, Color.Yellow), Color.FromArgb(200, Color.Green), Color.FromArgb(125, Color.Cyan), Color.FromArgb(50, Color.Blue)};
                //colorBlend.Positions = new float[] { 0.00f, 0.25f, 0.50f, 0.75f, 1.00f };
                heatGradient.InterpolationColors = colorBlend;
                Graphics fillGradient = Graphics.FromImage(HeatGradient);
                fillGradient.FillRectangle(heatGradient, 0, 0, HeatGradient.Width - 1, HeatGradient.Height);
                fillGradient.Dispose();
            }
        }

        private void MakeOccurence()
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                int x = OccurenceDiameter / 2;
                int y = OccurenceDiameter / 2;
                gp.AddEllipse(0, 0, OccurenceDiameter, OccurenceDiameter);
                using (PathGradientBrush gpBrush = new PathGradientBrush(gp))
                {
                    gpBrush.CenterPoint = new PointF(OccurenceDiameter / 2, OccurenceDiameter / 2);
                    gpBrush.CenterColor = Color.FromArgb(OccurenceIntensity, Color.Black);
                    gpBrush.SurroundColors = new Color[] { Color.Transparent };

                    Occurence = new Bitmap(OccurenceDiameter, OccurenceDiameter);
                    Graphics drawMap = Graphics.FromImage(Occurence);
                    drawMap.FillPath(gpBrush, gp);
                    drawMap.Dispose();
                }
            }

        }


        private void DrawOccurence(Location occurence, Graphics g)
        {
            int X = occurence.X - OccurenceDiameter / 2;
            int Y = occurence.Y - OccurenceDiameter / 2;
            g.DrawImage(Occurence, new Point(X, Y));
        }

        private void ConvertAlphaMapToHeatMap(Bitmap alphaMap)
        {
            using (ImageAttributes attributes = new ImageAttributes())
            {
                ColorMap[] newColorMap = new ColorMap[256];

                for (int x = 0; x < 256; x++)
                {
                    newColorMap[x] = new ColorMap();
                    newColorMap[x].OldColor = Color.FromArgb(x, Color.Black);
                    newColorMap[x].NewColor = HeatGradient.GetPixel(255 - x, 0);
                }

                Graphics remap = Graphics.FromImage(HeatMap);
                attributes.SetRemapTable(newColorMap);
                remap = Graphics.FromImage(HeatMap);
                remap.DrawImage(alphaMap, new Rectangle(0, 0, HeatMap.Width, HeatMap.Height), 0, 0, HeatMap.Width, HeatMap.Height, GraphicsUnit.Pixel, attributes);
                remap.Dispose();
            }
        }

        private static Bitmap Blur(Bitmap source)
        {
            Bitmap blurred = new Bitmap(source.Width, source.Height);
            BitmapData bmd = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData bmdBlurred = blurred.LockBits(new Rectangle(0, 0, blurred.Width, blurred.Height), ImageLockMode.ReadOnly, blurred.PixelFormat);
            int pixelSize = 4;
            int radius = 8;
            int totalMax = (radius * 2 + 1) * 256;
            long sourceStart = (long)bmd.Scan0;
            long blurredStart = (long)bmdBlurred.Scan0;
            int stride = bmd.Stride;
            int width = bmd.Width;
            int height = bmd.Height;
            int xOffset, yOffset;
            unsafe
            {
                yOffset = 0;
                for (int y = 0; y < height; y++)
                {
                    int total = 0;
                    byte* row = (byte*)sourceStart + yOffset;
                    for (int kx = -radius; kx <= radius; kx++)
                    {
                        if (kx >= 0 && kx < width)
                            total += row[(kx) * pixelSize + 3];
                    }

                    byte* rowSet = (byte*)blurredStart + yOffset;
                    rowSet[0] = (byte)(total / (radius * 2 + 1));

                    xOffset = 3;
                    int xOffsetRemove = (1 - radius - 1) * pixelSize + 3;
                    int xOffsetAdd = (1 + radius) * pixelSize + 3;
                    for (int x = 1; x < width; x++)
                    {
                        if (x - radius - 1 >= 0)
                            total -= row[xOffsetRemove];
                        if (x + radius < width)
                            total += row[xOffsetAdd];

                        rowSet = (byte*)blurredStart + yOffset;
                        if (total == 0) rowSet[xOffset] = 0;
                        else rowSet[xOffset] = (byte)(total / (radius * 2 + 1));

                        xOffset += pixelSize;
                        xOffsetRemove += pixelSize;
                        xOffsetAdd += pixelSize;
                    }
                    yOffset += stride;
                }

                source.UnlockBits(bmd);
                blurred.UnlockBits(bmdBlurred);
                source.Dispose();
                source = new Bitmap(blurred);
                bmd = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, source.PixelFormat);
                bmdBlurred = blurred.LockBits(new Rectangle(0, 0, blurred.Width, blurred.Height), ImageLockMode.ReadOnly, blurred.PixelFormat);
                sourceStart = (long)bmd.Scan0;

                xOffset = 3;
                for (int x = 0; x < width; x++)
                {
                    int total = 0;
                    byte* row;
                    for (int ky = -radius; ky <= radius; ky++)
                    {
                        row = (byte*)sourceStart + (ky * stride);
                        if (ky >= 0 && ky < height)
                            total += row[xOffset];
                    }

                    byte* rowSet = (byte*)blurredStart;
                    rowSet[xOffset] = (byte)(total / (radius * 2 + 1));

                    yOffset = 0;
                    int yOffsetRemove = (1 - radius - 1) * stride;
                    int yOffsetAdd = (1 + radius) * stride;
                    for (int y = 1; y < height; y++)
                    {
                        if (y - radius - 1 >= 0)
                        {
                            row = (byte*)sourceStart + yOffsetRemove;
                            total -= row[xOffset];
                        }
                        if (y + radius < height)
                        {
                            row = (byte*)sourceStart + yOffsetAdd;
                            total += row[xOffset];
                        }

                        rowSet = (byte*)blurredStart + yOffset;
                        if (total == 0) rowSet[xOffset] = 0;
                        rowSet[xOffset] = (byte)(total / (radius * 2 + 1));
                        yOffset += stride;
                        yOffsetRemove += stride;
                        yOffsetAdd += stride;
                    }
                    xOffset += pixelSize;
                }
            }
            source.UnlockBits(bmd);
            source.Dispose();
            blurred.UnlockBits(bmdBlurred);
            return blurred;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HeatMap.Dispose();
                HeatGradient.Dispose();
                Occurence.Dispose();
            }
        }
    }
}
