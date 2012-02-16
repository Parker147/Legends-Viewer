using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Docuverse.Identicon
{
    /// <summary>identicon rendering class</summary>
    /// <author>Jeff Atwood http://www.codinghorror.com/, Jon Galloway http://weblogs.asp.net/jgalloway/</author>
    /// <remarks>
    /// Based on Don Park's Identicons 1.2 Java Code
    /// http://www.docuverse.com/blog/donpark/2007/01/19/identicon-updated-and-source-released
    /// </remarks>
    public class IdenticonRenderer
    {
        // Each "patch" in an identicon is a polygon created from a list of vertices on a 5 by 5 grid.
        // Vertices are numbered from 0 to 24, starting from top-left corner of
        // the grid, moving left to right and top to bottom.
        private const int PATCH_CELLS = 4;
        private const byte PATCH_SYMMETRIC = 1;
        private const byte PATCH_INVERTED = 2;
        private static readonly int PATCH_GRIDS = PATCH_CELLS + 1;
        private const int MAX_SIZE = 128;
        private const int MIN_SIZE = 16;

        private static readonly byte[] patch0 = new byte[] { 0, 4, 24, 20, 0 };
        private static readonly byte[] patch1 = new byte[] { 0, 4, 20, 0 };
        private static readonly byte[] patch2 = new byte[] { 2, 24, 20, 2 };
        private static readonly byte[] patch3 = new byte[] { 2, 10, 14, 22, 2 };
        private static readonly byte[] patch4 = new byte[] { 2, 14, 22, 10, 2 };
        private static readonly byte[] patch5 = new byte[] { 0, 14, 24, 22, 0 };
        private static readonly byte[] patch6 = new byte[] { 2, 24, 22, 13, 11, 22, 20, 2 };
        private static readonly byte[] patch7 = new byte[] { 0, 14, 22, 0 };
        private static readonly byte[] patch8 = new byte[] { 6, 8, 18, 16, 6 };
        private static readonly byte[] patch9 = new byte[] { 4, 20, 10, 12, 2, 4 };
        private static readonly byte[] patch10 = new byte[] { 0, 2, 12, 10, 0 };
        private static readonly byte[] patch11 = new byte[] { 10, 14, 22, 10 };
        private static readonly byte[] patch12 = new byte[] { 20, 12, 24, 20 };
        private static readonly byte[] patch13 = new byte[] { 10, 2, 12, 10 };
        private static readonly byte[] patch14 = new byte[] { 0, 2, 10, 0 };

        private static readonly byte[][] patchTypes =
            new byte[][]
				{
					patch0, patch1, patch2, patch3, patch4, patch5, patch6, patch7, patch8, patch9, patch10, patch11, patch12, patch13,
					patch14, patch0
				};

        private static readonly byte[] patchFlags =
            new byte[]
				{
					PATCH_SYMMETRIC, 0, 0, 0, PATCH_SYMMETRIC, 0, 0, 0, PATCH_SYMMETRIC, 0, 0, 0, 0, 0, 0,
					(PATCH_SYMMETRIC + PATCH_INVERTED)
				};

        private static int[] centerPatchTypes = new int[] { 0, 4, 8, 15 };

        private GraphicsPath[] _patchShapes;
        private int _patchSize;
        private int _patchOffset; // used to center patch shape at originOffset because shape rotation works correctly.

        /// <summary>
        /// The maxSize in pixels at which each patch will be rendered interally before they
        /// are scaled down to the requested identicon maxSize. Default maxSize is 20 pixels
        /// which means, for 9-block identicon, a 60x60 image will be rendered and
        /// scaled down.
        /// </summary>
        public int PatchSize
        {
            get { return _patchSize; }

            set
            {
                this._patchSize = value;
                this._patchOffset = _patchSize / 2; // used to center patch shape at originOffset.
                int scale = _patchSize / PATCH_CELLS;
                this._patchShapes = new GraphicsPath[patchTypes.Length];
                for (int i = 0; i < patchTypes.Length; i++)
                {
                    GraphicsPath patch = new GraphicsPath();
                    byte[] patchVertices = patchTypes[i];
                    for (int j = 0; j < patchVertices.Length; j++)
                    {
                        int v = patchVertices[j];
                        int vx = (v % PATCH_GRIDS * scale) - _patchOffset;
                        int vy = (v / PATCH_GRIDS * scale) - _patchOffset;
                        AddPointToGraphicsPath(patch, vx, vy);
                    }
                    this._patchShapes[i] = patch;
                    patch.CloseFigure();
                }

            }
        }

        /// <summary>
        /// Adds the X and Y coordinatesList to the current graphics path.
        /// </summary>
        /// <param name="path"> The current Graphics path</param>
        /// <param name="x">The x coordinate to be added</param>
        /// <param name="y">The y coordinate to be added</param>
        private static void AddPointToGraphicsPath(GraphicsPath path, int x, int y)
        {
            PointF[] tempPointArray = new PointF[path.PointCount + 1];
            byte[] tempPointTypeArray = new byte[path.PointCount + 1];

            if (path.PointCount == 0)
            {
                tempPointArray[0] = new PointF(x, y);
                GraphicsPath tempGraphicsPath = new GraphicsPath(tempPointArray, new byte[] { (byte)PathPointType.Start });
                path.AddPath(tempGraphicsPath, false);
            }
            else
            {
                path.PathPoints.CopyTo(tempPointArray, 0);
                tempPointArray[path.PointCount] = new Point(x, y);

                path.PathTypes.CopyTo(tempPointTypeArray, 0);
                tempPointTypeArray[path.PointCount] = (byte)PathPointType.Line;

                GraphicsPath tempGraphics = new GraphicsPath(tempPointArray, tempPointTypeArray);
                path.Reset();
                path.AddPath(tempGraphics, false);
                //path.CloseFigure();
            }
        }

        /// <summary>
        /// Returns rendered identicon bitmap for a given identicon code.
        /// </summary>
        /// <param name="code">identicon code</param>
        /// <param name="maxSize">desired image maxSize</param>
        public Bitmap Render(int code, int size, Color renderColor)
        {

            // enforce maxSize limits
            if (size > MAX_SIZE)
                size = MAX_SIZE;
            if (size < MIN_SIZE)
                size = MIN_SIZE;

            // set patch maxSize appropriately to avoid scaling artifacts
            if (size <= 24)
            {
                PatchSize = 16;
            }
            else if (size <= 40)
            {
                PatchSize = 20;
            }
            else if (size <= 64)
            {
                PatchSize = 32;
            }
            else if (size <= 128)
            {
                PatchSize = 48;
            }

            // decode the code into parts:            
            // bit 0-1: middle patch race
            int centerType = centerPatchTypes[code & 0x3];
            // bit 2: middle invert
            bool centerInvert = ((code >> 2) & 0x1) != 0;
            // bit 3-6: corner patch race
            int cornerType = (code >> 3) & 0x0f;
            // bit 7: corner invert
            bool cornerInvert = ((code >> 7) & 0x1) != 0;
            // bit 8-9: corner turns
            int cornerTurn = (code >> 8) & 0x3;
            // bit 10-13: side patch race
            int sideType = (code >> 10) & 0x0f;
            // bit 14: side invert
            bool sideInvert = ((code >> 14) & 0x1) != 0;
            // bit 15: corner turns
            int sideTurn = (code >> 15) & 0x3;
            // bit 16-20: blue color component
            //int blue = (code >> 16) & 0x01f;
            // bit 21-26: green color component
            //int green = (code >> 21) & 0x01f;
            // bit 27-31: red color component
            //int red = (code >> 27) & 0x01f;

            // color components are used at top of the range for color difference
            // use white background for now. TODO: support transparency.
            Color foreColor = Color.FromArgb(renderColor.A, renderColor);
            //Color foreColor = Color.FromArgb(200, red, green, blue);
            //Color foreColor = Color.FromArgb(red, green, blue);
            //Color foreColor = Color.FromArgb(red << 3, green << 3, blue << 3);
            //Color backColor = Color.DimGray;
            Color backColor;
            if (renderColor.GetBrightness() < 0.33)
                backColor = Color.FromArgb(200, 200, 200);
            else if (renderColor.GetBrightness() < 0.66)
                backColor = Color.FromArgb(127, 127, 127);
            else
                backColor = Color.FromArgb(80, 80, 80);
            //Color backColor = Color.Black;

            // outline shapes with a noticeable color (complementary will do) if
            // shape color and background color are too similar (measured by color
            // distance).
            Color strokeColor = Color.Empty;
            if (ColorDistance(ref foreColor, ref backColor) < 32f)
            {
                strokeColor = ComplementaryColor(ref foreColor);
            }

            // render at larger source maxSize (to be scaled down later)
            int sourceSize = _patchSize * 3;
            using (Bitmap sourceImage = new Bitmap(sourceSize, sourceSize, PixelFormat.Format32bppRgb))
            {
                using (Graphics graphics = Graphics.FromImage(sourceImage))
                {
                    //graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    // center patch
                    DrawPatch(graphics, _patchSize, _patchSize, centerType, 0, centerInvert, ref foreColor, ref backColor,
                              ref strokeColor);

                    // side patch (top)
                    DrawPatch(graphics, _patchSize, 0, sideType, sideTurn++, sideInvert, ref foreColor, ref backColor,
                              ref strokeColor);
                    // side patch (right)
                    DrawPatch(graphics, _patchSize * 2, _patchSize, sideType, sideTurn++, sideInvert, ref foreColor, ref backColor,
                              ref strokeColor);
                    // side patch (bottom)
                    DrawPatch(graphics, _patchSize, _patchSize * 2, sideType, sideTurn++, sideInvert, ref foreColor, ref backColor,
                              ref strokeColor);
                    // side patch (left)
                    DrawPatch(graphics, 0, _patchSize, sideType, sideTurn, sideInvert, ref foreColor, ref backColor, ref strokeColor);

                    // corner patch (top left)
                    DrawPatch(graphics, 0, 0, cornerType, cornerTurn++, cornerInvert, ref foreColor, ref backColor, ref strokeColor);
                    // corner patch (top right)
                    DrawPatch(graphics, _patchSize * 2, 0, cornerType, cornerTurn++, cornerInvert, ref foreColor, ref backColor,
                              ref strokeColor);
                    // corner patch (bottom right)
                    DrawPatch(graphics, _patchSize * 2, _patchSize * 2, cornerType, cornerTurn++, cornerInvert, ref foreColor,
                              ref backColor, ref strokeColor);
                    // corner patch (bottom left)
                    DrawPatch(graphics, 0, _patchSize * 2, cornerType, cornerTurn, cornerInvert, ref foreColor, ref backColor,
                              ref strokeColor);
                }
                // scale source image to target maxSize with bicubic smoothing
                Bitmap b = new Bitmap(size, size, PixelFormat.Format32bppRgb);
                using (Graphics g = Graphics.FromImage(b))
                {
                    //g.PixelOffsetMode = PixelOffsetMode.Half;
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    int fudge = (int)(size * 0.016 * 0); // this is necessary to prevent scaling artifacts at larger sizes
                    g.DrawImage(sourceImage, 0, 0, size + fudge, size + fudge);
                }
                return b;
            }
        }

        private void DrawPatch(Graphics g, int x, int y, int patch, int turn, bool invert, ref Color fore, ref Color back, ref Color stroke)
        {
            patch %= patchTypes.Length;
            turn %= 4;
            if ((patchFlags[patch] & PATCH_INVERTED) != 0)
                invert = !invert;

            // paint the background
            g.FillRegion(new SolidBrush(invert ? fore : back), new Region(new Rectangle(x, y, _patchSize, _patchSize)));

            // offset and rotate coordinate space by patch ScrollPosition (x, y) and
            // 'turn' before rendering patch shape
            Matrix m = g.Transform;
            g.TranslateTransform((x + _patchOffset), (y + _patchOffset));
            g.RotateTransform(turn * 90);

            // if stroke color was specified, apply stroke
            // stroke color should be specified if fore color is too close to the BackHistory color.
            if (!stroke.IsEmpty)
            {
                g.DrawPath(new Pen(stroke), _patchShapes[patch]);
            }

            // render rotated patch using fore color (BackHistory color if inverted)
            g.FillPath(new SolidBrush(invert ? back : fore), _patchShapes[patch]);

            // restore previous rotation
            g.Transform = m;
        }

        /// <summary>Returns distance between two colors</summary>		
        private static float ColorDistance(ref Color c1, ref Color c2)
        {
            float dx = c1.R - c2.R;
            float dy = c1.G - c2.G;
            float dz = c1.B - c2.B;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>Returns complementary color</summary>
        private static Color ComplementaryColor(ref Color c)
        {
            return Color.FromArgb(c.ToArgb() ^ 0x00FFFFFF);
        }
    }
}