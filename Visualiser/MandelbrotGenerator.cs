using System;
using System.Drawing;
using System.Linq;

namespace Visualiser
{
    public class MandelbrotGenerator
    {
        private const int IterationLimit = 100;
        private const int InfinityThreshold = 1024;

        public MandelbrotGenerator(int canvasWidth, int canvasHeight)
        {
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;

            RangeWidthLower = -2;
            RangeWidthUpper = 2;
            RangeHeightLower = -2;
            RangeHeightUpper = 2;
        }

        public int CanvasWidth { get; }
        public int CanvasHeight { get; }

        #region Ranges

        private double RangeWidthLower { get; }
        private double RangeWidthUpper { get; }
        private double WidthRangeSize => RangeWidthUpper - RangeHeightLower;
        private double RangeHeightLower { get; }
        private double RangeHeightUpper { get; }
        private double HeightRangeSize => RangeHeightUpper - RangeHeightLower;

        #endregion

        /// <summary>
        ///     Maps the given value from within the canvas width range 0 to <see cref="CanvasWidth"/> to the set range.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns></returns>
        /// <remarks>NewValue = (((OldValue - OldMin) * NewRangeSize0) / OldRangeSize) + NewMin</remarks>
        private double MapWidth(int value)
        {
            return (double) value * WidthRangeSize / CanvasWidth + RangeWidthLower;
        }

        /// <summary>
        ///     Maps the given value from within the canvas height range 0 to <see cref="CanvasHeight"/> to the set range.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns></returns>
        /// <remarks>NewValue = (((OldValue - OldMin) * NewRangeSize0) / OldRangeSize) + NewMin</remarks>
        private double MapHeight(int value)
        {
            return (double) value * HeightRangeSize / CanvasHeight + RangeHeightLower;
        }

        public Bitmap GetCanvas()
        {
            var bmp = new Bitmap(CanvasWidth, CanvasHeight);

            for (var x = 0; x < CanvasWidth; x++)
            for (var y = 0; y < CanvasHeight; y++)
            {
                var a = MapWidth(x);
                var b = MapHeight(y);

                // Set color with brightness
                var brightness = (byte) Math.Floor(GetBrightness(a, b) * 255);
                var color = Color.FromArgb(brightness, Color.Black);
                bmp.SetPixel(x, y, color);
            }

            return bmp;
        }

        /// <summary>
        ///     Calculates the convergence for the given point and determines the brightness based on it.
        /// </summary>
        /// <param name="a">Real component of complex c.</param>
        /// <param name="b">Imaginary component of complex c.</param>
        /// <returns>The brightness of the given position as a value in the range of [0,1].</returns>
        private double GetBrightness(double a, double b)
        {
            // Store original c
            var ca = a;
            var cb = b;

            // Iteration counter
            int n;

            for (n = 0; n < IterationLimit; n++)
            {
                // Calculate z²
                var aa = a * a - b * b;
                var bb = 2 * a * b;

                // z² + c
                a = aa + ca;
                b = bb + cb;

                if (Math.Abs(a + b) > InfinityThreshold) break;
            }

            return (double) n / IterationLimit;
        }
    }
}