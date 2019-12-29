using System;
using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace Visualiser
{
    static class Program
    {
        public class Options
        {
            [Option('f', "filePath", Required = true, HelpText = "Set the file to save to.")]
            public string FilePath { get; set; }

            [Option('r', "resPow", Required = false, Default = 12,
                HelpText = "The power to raise 2 to to calculate the resolution.")]
            public int ResPow { get; set; }
        }

        static void Main(string[] args)
        {
            // File path to save image to
            string file;

            // image parameters
            int width = 0;
            int height = 0;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                file = o.FilePath;

                width = (int) Math.Pow(2, o.ResPow);
                height = (int) Math.Pow(2, o.ResPow);

            });

            Console.WriteLine($"Generating {width}x{height} Mandelbrot canvas.");

            MandelbrotGenerator mandelbrotGenerator = new MandelbrotGenerator(width, height);

            Image img = mandelbrotGenerator.GetCanvas();
            Console.WriteLine("Rendered canvas!");

            img.Save(file, ImageFormat.Png);
            Console.WriteLine($"Saved image to '{file}'.");
        }
    }
}
