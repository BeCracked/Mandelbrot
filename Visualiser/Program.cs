using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Visualiser
{
    static class Program
    {
        static void Main(string[] args)
        {
            // File to save to
            string file = "/home/be_cracked/dev/test/mandeln.png";

            // image parameters
            int width = (int) Math.Pow(2,12);
            int height = (int) Math.Pow(2,12);

            Console.WriteLine($"Generating {width}x{height} Mandelbrot canvas.");

            MandelbrotGenerator mandelbrotGenerator = new MandelbrotGenerator(width, height);

            Image img = mandelbrotGenerator.GetCanvas();
            Console.WriteLine("Rendered canvas!");

            img.Save(file, ImageFormat.Png);
            Console.WriteLine($"Saved image to '{file}'.");
        }
    }
}
